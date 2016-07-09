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
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NormalizedSystems.Net
{
    public abstract class DataElement : Element
    {
        [JsonIgnore]
        [XmlIgnore]
        public Dictionary<string, FieldElement> Fields { get; }
            = new Dictionary<string, FieldElement>();

        protected void Convert(DataElement data)
        {
            (from forig in Fields.Values
             join fdest in data.Fields.Values
             on forig.ElementInfo.Name equals fdest.ElementInfo.Name
             where forig.ElementInfo.Version >= fdest.ElementInfo.Version
             select forig.ElementInfo.Name).ToList().ForEach(
               result => data.Fields[result] = Fields[result]);
        }
    }
}
