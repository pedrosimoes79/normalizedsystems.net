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
    public abstract class FieldElement<T> : FieldElement
    {
        public new T Value
        {
            get
            {
                return (T)(base.Value ?? default(T));
            }
            set
            {
                base.Value = value;
            }
        }

        public void Convert(FieldElement dest)
        {
            dest.Value = Value;
        }

        public static implicit operator T(FieldElement<T> field)
        {
            return field.Value;
        }
    }

    public abstract class FieldElement : Element
    {
        public object Value { get; set; }

        public override string ToString()
        {
            return (Value ?? string.Empty).ToString();
        }

        public override int GetHashCode()
        {
            if (Value == null) return 0;
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            FieldElement other = obj as FieldElement;

            return this == other;
        }

        public static bool operator ==(FieldElement a, FieldElement b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (((object)a == null) || ((object)b == null)) return false;
            if (ReferenceEquals(a.Value, b.Value)) return true;
            if ((a.Value == null) || (b.Value == null)) return false;
            
            return a.Value == b.Value;
        }

        public static bool operator !=(FieldElement a, FieldElement b)
        {
            return !(a == b);
        }
    }
}
