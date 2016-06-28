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
        public Guid CorrelationId { get; set; } = Guid.NewGuid();

        internal Application Application { get; set; }

        [JsonProperty()]
        internal bool Handled { get; set; }

        [JsonIgnore()]
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
             select orig.ElementInfo.Name).ToList().ForEach(
                result => e.ContentData[result] = ContentData[result]);
        }
    }
}
