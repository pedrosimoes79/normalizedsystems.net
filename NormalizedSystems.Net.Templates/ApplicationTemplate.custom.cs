using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NormalizedSystems.Net;

namespace NormalizedSystems.Net.Templates
{
    public partial class ApplicationTemplate
    {
        private Definitions.Application model;
        private string codenamespace;

        public ApplicationTemplate(Definitions.Application model, string codenamespace)
        {
            this.model = model;
            this.codenamespace = codenamespace;
        }
    }
}
