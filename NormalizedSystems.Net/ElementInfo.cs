using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NormalizedSystems.Net
{
    public sealed class ElementInfo
    {
        public string Name { get; set; }
        public uint Version { get; set; }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Version.GetHashCode(); 
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ElementInfo)) return false;
            var other = (ElementInfo)obj;
            return Name == other.Name && Version == other.Version; 
        }
    }
}
