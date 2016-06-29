// This file is part of NormalizedSystems.Net
// 
// NormalizedSystems.Net is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// NormalizedSystems.Net is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Foobar.  If not, see <http://www.gnu.org/licenses/>.

using Newtonsoft.Json;
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

        internal Guid CorrelationId { get; set; }

        internal Application Application { get; set; }

        [JsonIgnore()]
        public Dictionary<string, EventElement> Events { get; }
                    = new Dictionary<string, EventElement>();

        [JsonIgnore()]
        public Dictionary<string, ConditionElement> Conditions { get; }
                    = new Dictionary<string, ConditionElement>();

        [JsonIgnore()]
        public Dictionary<string, ActionElement> Actions { get; }
                    = new Dictionary<string, ActionElement>();

        private bool checkEvents()
        {
            if (LogicType == LogicType.And)
                return Events.Values.All(e => e.Handled);
            else
                return Events.Values.Any(e => e.Handled);
        }

        private bool checkConditions()
        {
            if (Conditions.Count() == 0) return true;

            return Conditions.Values.All(
                c =>
                {
                    (from ce in c.Events.Values
                     from e in Events.Values
                     where
                        e.Handled &&
                        ce.ElementInfo.Name == e.ElementInfo.Name &&
                        e.ElementInfo.Version >= ce.ElementInfo.Version
                     select e.ElementInfo.Name).ToList().ForEach(
                        result =>
                        {
                            c.Events[result] = Events[result].Clone();
                        });

                    return c.Events.Values.Any(e => !e.Handled) || c.Check();
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
                     select ed).ToList().ForEach(
                        result =>
                        {
                            a.InputData[result.ElementInfo.Name] = result.Clone();
                        });

                    a.OutputEvents.Values.ToList().ForEach(
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
