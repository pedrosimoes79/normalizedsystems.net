using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormalizedSystems.Net;

namespace NormalizedSystems.Net.Templates
{
    public partial class EventElementTemplate
    {
        private Definitions.EventElement model;
        private Definitions.Application app;

        public EventElementTemplate(Definitions.EventElement model, Definitions.Application app)
        {
            this.model = model;
            this.app = app;
        }
    }
}
