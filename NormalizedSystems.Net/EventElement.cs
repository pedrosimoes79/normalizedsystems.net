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
