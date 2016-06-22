using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormalizedSystems.Net;

namespace NormalizedSystems.Net.Templates
{
    public partial class FieldElementTemplate
    {
        private Definitions.FieldElement model;
        private Definitions.Application app;

        public FieldElementTemplate(Definitions.FieldElement model, Definitions.Application app)
        {
            this.model = model;
            this.app = app;
        }
    }
}
