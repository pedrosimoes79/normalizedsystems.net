using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormalizedSystems.Net;

namespace NormalizedSystems.Net.Templates
{
    public partial class ConditionElementTemplate
    {
        private Definitions.ConditionElement model;
        private Definitions.Application app;

        public ConditionElementTemplate(Definitions.ConditionElement model, Definitions.Application app)
        {
            this.model = model;
            this.app = app;
        }
    }
}
