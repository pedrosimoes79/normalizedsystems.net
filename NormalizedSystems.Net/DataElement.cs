using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace NormalizedSystems.Net
{
    public abstract class DataElement : Element
    {
        public Dictionary<string, FieldElement> Fields { get; } 
            = new Dictionary<string, FieldElement>();

        protected void Convert(DataElement data)
        {
            (from forig in Fields.Values
             join fdest in data.Fields.Values
             on forig.ElementInfo.Name equals fdest.ElementInfo.Name
             where forig.ElementInfo.Version >= fdest.ElementInfo.Version
             select forig.ElementInfo.Name).AsParallel().ForAll(result => data.Fields[result] = Fields[result]);
        }
    }
}
