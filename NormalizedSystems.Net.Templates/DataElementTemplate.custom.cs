using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormalizedSystems.Net;

namespace NormalizedSystems.Net.Templates
{
    public partial class DataElementTemplate
    {
        private Definitions.DataElement model;
        private Definitions.Application app;

        public DataElementTemplate(Definitions.DataElement model, Definitions.Application app)
        {
            this.model = model;
            this.app = app;
        }
    }
}
