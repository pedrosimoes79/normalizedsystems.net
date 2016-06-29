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
