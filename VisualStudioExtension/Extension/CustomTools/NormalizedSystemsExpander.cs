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

using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using NormalizedSystems.Net.Templates;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using VSLangProj80;
using static NormalizedSystems.Net.Utils;

namespace NormalizedSystems.Net.CustomTools
{
    [ComVisible(true)]
    [Guid("5015988B-724B-4D6E-810C-941D23F1568C")]
    [CodeGeneratorRegistration(typeof(NormalizedSystemsExpander), "C# NormalizedSystems Expander", vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [ProvideObject(typeof(NormalizedSystemsExpander))]
    public class NormalizedSystemsExpander : BaseCustomTool
    {
        public override string generate(string input, string ns, string inputfile, IVsGeneratorProgress pGenerateProgress)
        {
            // Step 0: Dealing with local variables

            var filename = Path.GetFileName(inputfile);
            var name = filename.Substring(0, filename.IndexOf('.'));

            //Step 1: Xsd validation (syntactic check)

            try
            {
                Utils.ValidateXml(input, Utils.GetTextEmbeddedResource("NormalizedSystems.xsd", Assembly.GetAssembly(typeof(Definitions.Application))));
            }
            catch (SchemaValidationException svex)
            {
                pGenerateProgress.GeneratorError(0, 0, string.Format("Error validating file: {0} - {1}", filename, svex.Message), svex.LineNumber, 0xFFFFFFFF);
            }
            catch (Exception ex)
            {
                pGenerateProgress.GeneratorError(0, 0, string.Format("Error validating file: {0} - {1}", filename, ex.Message), 0xFFFFFFFF, 0xFFFFFFFF);

                return default(string);
            }

            //Step 2: Deserialize descriptor

            var serializer = new XmlSerializer(typeof(Definitions.Application));
            var application = default(Definitions.Application);

            try
            {
                using (TextReader tr = new StringReader(input))
                {
                    application = (Definitions.Application)serializer.Deserialize(tr);
                }
            }
            catch (Exception ex)
            {
                pGenerateProgress.GeneratorError(0, 0, string.Format("Error validating file: {0} - {1}\r\n{2}", ex.GetType().Name, ex.Message, ex.StackTrace), 0xFFFFFFFF, 0xFFFFFFFF);

                return default(string);
            }

            //Step 3: Semantic checks

            if (application.Name != name)
            {
                pGenerateProgress.GeneratorError(0, 0, string.Format("{0} doesn't match with filename {1}", application.Name, inputfile), 0xFFFFFFFF, 0xFFFFFFFF);

                return default(string);
            }

            ruleConditionsCheck(pGenerateProgress, application);

            //Step 4: Expand code

            try
            {
                return (new ApplicationTemplate(application, ns)).TransformText();
            }
            catch (Exception ex)
            {
                pGenerateProgress.GeneratorError(0, 0, string.Format("Error expanding file: {0} - {1}\r\n{2}", ex.GetType().Name, ex.Message, ex.StackTrace), 0xFFFFFFFF, 0xFFFFFFFF);

                return default(string);
            }
        }

        private static void ruleConditionsCheck(IVsGeneratorProgress pGenerateProgress, Definitions.Application application)
        {
            var query =
                (from rule in application.RuleElements
                 from ruleCondition in rule.Conditions
                 where
                    !application.ConditionElements.Any(
                        condition => 
                            condition.Name == ruleCondition.Name &&
                            condition.Version == ruleCondition.Version)
                select
                    new
                    {
                        Rule = rule,
                        Condition = ruleCondition
                    });

            foreach (var result in query)
            {
                pGenerateProgress.GeneratorError(
                    0, 0,
                    string.Format(
                        "Condition {0} version {1} on Rule {2} version {3} doesn't " +
                        "match with any existent condition on {4} application",
                        result.Condition.Name,
                        result.Condition.Version,
                        result.Rule.Name,
                        result.Rule.Version,
                        application.Name), 0xFFFFFFFF, 0xFFFFFFFF);
            }
        }
    }
}
