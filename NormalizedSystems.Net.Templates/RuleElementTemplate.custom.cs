using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormalizedSystems.Net.Definitions;

namespace NormalizedSystems.Net.Templates
{
    public partial class RuleElementTemplate
    {
        private Definitions.RuleElement model;
        private Definitions.Application app;

        public RuleElementTemplate(Definitions.RuleElement model, Definitions.Application app)
        {
            this.model = model;
            this.app = app;
        }
    }
}
