using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormalizedSystems.Net
{
    public abstract class ActionElement : Element
    {
        public Dictionary<string, DataElement> InputData { get; }
            = new Dictionary<string, DataElement>();

        public Dictionary<string, EventElement> OutputEvents { get; }
                    = new Dictionary<string, EventElement>();

        public virtual void Execute() { }
    }
}
