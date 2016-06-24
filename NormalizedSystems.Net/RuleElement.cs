using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NormalizedSystems.Net
{
    public abstract class RuleElement : Element
    {
        public LogicType LogicType { get; protected set; } = LogicType.And;

        public Guid CorrelationId { get; internal set; }

        public Application Application { get; internal set; }

        public Dictionary<string, EventElement> Events { get; }
                    = new Dictionary<string, EventElement>();

        public Dictionary<string, ConditionElement> Conditions { get; }
                    = new Dictionary<string, ConditionElement>();

        public Dictionary<string, ActionElement> Actions { get; }
                    = new Dictionary<string, ActionElement>();

        private bool checkEvents()
        {
            if(LogicType == LogicType.And)
                return Events.Values.AsParallel().All(e => e.Handled);
            else
                return Events.Values.AsParallel().Any(e => e.Handled);
        }

        private bool checkConditions()
        {
            if (Conditions.Count() == 0) return true;

            return Conditions.Values.AsParallel().All(
                c =>
                {
                    (from ce in c.Events.Values
                     join e in Events.Values
                     on ce.ElementInfo.Name equals e.ElementInfo.Name
                     where e.ElementInfo.Version >= ce.ElementInfo.Version
                     select e.ElementInfo.Name).AsParallel().ForAll(result => c.Events[result] = Events[result].Clone());

                    return c.Check();
                });
        }

        private void invokeActions()
        {
            Actions.Values.AsParallel().ForAll(
                a =>
                {
                    (from e in Events.Values
                     from ed in e.ContentData.Values
                     from ad in a.InputData.Values
                     where
                        e.Handled &&
                        ed.ElementInfo.Name == ad.ElementInfo.Name &&
                        ed.ElementInfo.Version >= ad.ElementInfo.Version
                     select ed).AsParallel().ForAll(result => a.InputData[result.ElementInfo.Name] = result.Clone());

                    a.OutputEvents.Values.AsParallel().ForAll(
                        e =>
                        {
                            e.CorrelationId = CorrelationId;
                            e.Application = Application;
                        });

                    Task.Run(() => a.Execute());
                });
        }
        
        public bool Evaluate()
        {
            var ret = false;

            var chkEvents = checkEvents();
            var chkConditions = (chkEvents ? checkConditions() : false);

            if (chkEvents)
            {
                ret = true;

                if (chkConditions)
                {
                    invokeActions();
                }
            }

            return ret;
        }
    }
}
