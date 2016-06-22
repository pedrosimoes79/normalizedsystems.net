using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormalizedSystems.Net;

namespace NormalizedSystems.Net.Templates
{
    public partial class ActionElementTemplate
    {
        private Definitions.ActionElement model;
        private Definitions.Application app;

        public ActionElementTemplate(Definitions.ActionElement model, Definitions.Application app)
        {
            this.model = model;
            this.app = app;
        }
    }
}
