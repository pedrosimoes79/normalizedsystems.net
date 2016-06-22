using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NormalizedSystems.Net
{
    public abstract class Application
    {
        private readonly Dictionary<ElementInfo, Func<RuleElement>> rules
            = new Dictionary<ElementInfo, Func<RuleElement>>();

        private readonly Dictionary<ElementInfo, HashSet<ElementInfo>> eventRules
            = new Dictionary<ElementInfo, HashSet<ElementInfo>>();

        private readonly Dictionary<Guid, Dictionary<ElementInfo, List<RuleElement>>> waitingRules
            = new Dictionary<Guid, Dictionary<ElementInfo, List<RuleElement>>>();

        private readonly Dictionary<Guid, Dictionary<ElementInfo, Action<EventElement>>> listeners
            = new Dictionary<Guid, Dictionary<ElementInfo, Action<EventElement>>>();

        protected void AddRule<T>()
            where T : RuleElement, new()
        {
            var rule = new T();
            var ruleinfo = rule.ElementInfo;

            if (!rules.ContainsKey(ruleinfo))
            {
                rules[ruleinfo] = () => new T();

                foreach (var e in rule.Events.Values)
                {
                    if (!eventRules.ContainsKey(e.ElementInfo))
                        eventRules[e.ElementInfo] = new HashSet<ElementInfo>();
                    if (!eventRules[e.ElementInfo].Contains(ruleinfo))
                        eventRules[e.ElementInfo].Add(ruleinfo);
                }
            }
        }

        public void Raise(EventElement e)
        {
            e.Handled = true;

            var eventinfo = e.ElementInfo;

            if (!waitingRules.ContainsKey(e.CorrelationId))
                waitingRules[e.CorrelationId] = new Dictionary<ElementInfo, List<RuleElement>>();

            lock (waitingRules[e.CorrelationId])
            {
                if (eventinfo.Version > 1)
                {
                    var assembly = e.GetType().Assembly;

                    var type = assembly.GetType(e.GetType().Namespace + "." + eventinfo.Name + (eventinfo.Version - 1 > 1 ? "Version" + (eventinfo.Version - 1) : ""));

                    Raise((EventElement)type.Cast(e));
                }

                if (listeners.ContainsKey(e.CorrelationId) && listeners[e.CorrelationId].ContainsKey(eventinfo))
                {
                    listeners[e.CorrelationId][eventinfo](e);
                }

                if (eventRules.ContainsKey(eventinfo))
                {
                    var rulesinfo = eventRules[eventinfo];

                    foreach (var ruleinfo in rulesinfo)
                    {
                        var handled = false;

                        if (waitingRules[e.CorrelationId].ContainsKey(ruleinfo))
                        {
                            foreach (var rule in waitingRules[e.CorrelationId][ruleinfo])
                            {
                                var v = rule.Events[eventinfo.Name];

                                if (v == null || !v.Handled)
                                {
                                    rule.Events[eventinfo.Name] = e.Clone();
                                    handled = true;

                                    if (rule.Evaluate())
                                        waitingRules[e.CorrelationId][ruleinfo].Remove(rule);

                                    break;
                                }
                            }
                        }
                        else
                        {
                            waitingRules[e.CorrelationId][ruleinfo] = new List<RuleElement>();
                        }

                        if (!handled)
                        {
                            var rule = rules[ruleinfo]();

                            rule.CorrelationId = e.CorrelationId;
                            rule.Application = this;

                            rule.Events[eventinfo.Name] = e.Clone();

                            if (!rule.Evaluate()) waitingRules[e.CorrelationId][ruleinfo].Add(rule);
                        }
                    }
                }
            }
        }

        public async Task<T> Raise<T>(EventElement e, int timeout = Timeout.Infinite)
            where T : EventElement, new()
        {
            var ret = default(T);
            var cts = new CancellationTokenSource();

            try
            {
                listeners[e.CorrelationId] = new Dictionary<ElementInfo, Action<EventElement>>();

                listeners[e.CorrelationId][(new T()).ElementInfo] =
                    new Action<EventElement>(
                        evt =>
                        {
                            ret = (T)evt;
                            cts.Cancel();
                        });

                this.Raise(e);

                await Task.Delay(timeout, cts.Token).ContinueWith(t => { });
            }
            finally
            {
                listeners.Remove(e.CorrelationId);
            }

            return ret;
        }

        public async Task<IEnumerable<T>> Raise<T, TEOF>(EventElement e, int timeout = Timeout.Infinite)
            where T : EventElement, new()
            where TEOF : EventElement, new()
        {
            var ret = new List<T>();

            try
            {
                var cts = new CancellationTokenSource();

                listeners[e.CorrelationId] = new Dictionary<ElementInfo, Action<EventElement>>();
                listeners[e.CorrelationId][(new T()).ElementInfo] = new Action<EventElement>(evt => { ret.Add((T)evt); });
                listeners[e.CorrelationId][(new TEOF()).ElementInfo] = new Action<EventElement>(evt => { cts.Cancel(); });

                this.Raise(e);

                await Task.Delay(timeout, cts.Token).ContinueWith(t => { });
            }
            finally
            {
                listeners.Remove(e.CorrelationId);
            }

            return ret;
        }
    }
}
