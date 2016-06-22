using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NormalizedSystems.Net
{
    public abstract class EventElement : Element
    {
        [JsonProperty()]
        public Guid CorrelationId { get; internal set; } = Guid.NewGuid();

        internal Application Application { get; set; }

        internal bool Handled { get; set; }

        public Dictionary<string, DataElement> ContentData { get; } 
            = new Dictionary<string, DataElement>();

        public void Raise()
        {
            Application?.Raise(this);
        }

        protected void Convert(EventElement e)
        {
            e.CorrelationId = CorrelationId;
            e.Application = Application;
            e.Handled = Handled;

            (from orig in ContentData.Values
             join dest in e.ContentData.Values
             on orig.ElementInfo.Name equals dest.ElementInfo.Name
             where orig.ElementInfo.Version >= dest.ElementInfo.Version
             select orig.ElementInfo.Name).AsParallel().ForAll(
                result => e.ContentData[result] = ContentData[result]);
        }
    }
}
