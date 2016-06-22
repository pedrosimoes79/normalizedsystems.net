using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormalizedSystems.Net
{
    public abstract class ConditionElement : Element
    {
        public Dictionary<string, EventElement> Events { get; }
                    = new Dictionary<string, EventElement>();

        public virtual bool Check() { return true; }
    }
}
