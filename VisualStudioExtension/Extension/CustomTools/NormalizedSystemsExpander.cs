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
using NormalizedSystems.Net.Definitions;

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

            var ext = string.Empty;
            DefaultExtension(out ext);
            var outfile = Path.GetFileNameWithoutExtension(inputfile) + ext;
            var previous = string.Empty;

            if (File.Exists(outfile))
                previous = File.ReadAllText(outfile);

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

                return previous;
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

                return previous;
            }

            //Step 3: Semantic checks

            if (application.Name != name)
            {
                pGenerateProgress.GeneratorError(0, 0, string.Format("{0} doesn't match with filename {1}", application.Name, inputfile), 0xFFFFFFFF, 0xFFFFFFFF);

                return previous;
            }

            if(dataFieldsCheck(pGenerateProgress, application) |
               eventContentDataCheck(pGenerateProgress, application) |
               actionInputDataCheck(pGenerateProgress, application) |
               actionOutputEventsCheck(pGenerateProgress, application) |
               conditionEventsCheck(pGenerateProgress, application) |
               ruleEventsCheck(pGenerateProgress, application) |
               ruleConditionsCheck(pGenerateProgress, application) |
               ruleActionsCheck(pGenerateProgress, application))
            {
                return previous;
            }

            //Step 4: Expand code

            try
            {
                return (new ApplicationTemplate(application, ns)).TransformText();
            }
            catch (Exception ex)
            {
                pGenerateProgress.GeneratorError(0, 0, string.Format("Error expanding file: {0} - {1}\r\n{2}", ex.GetType().Name, ex.Message, ex.StackTrace), 0xFFFFFFFF, 0xFFFFFFFF);

                return previous;
            }
        }

        private bool ruleActionsCheck(IVsGeneratorProgress pGenerateProgress, Application application)
        {
            var query =
                (from rule in application.RuleElements
                 from ruleAction in rule.Actions
                 where 
                    !application.ActionElements.Any(
                        action =>
                            action.Name == ruleAction.Name &&
                            action.Version == ruleAction.Version)
                 select
                     new
                     {
                         Rule = rule,
                         Action = ruleAction
                     });

            foreach (var result in query)
            {
                pGenerateProgress.GeneratorError(
                    0, 0,
                    string.Format(
                        "Action {0} version {1} on Rule {2} version {3} doesn't " +
                        "match with any existent event on {4} application",
                        result.Action.Name,
                        result.Action.Version,
                        result.Rule.Name,
                        result.Rule.Version,
                        application.Name), 0xFFFFFFFF, 0xFFFFFFFF);
            }

            return query.Count() > 0;
        }

        private bool ruleEventsCheck(IVsGeneratorProgress pGenerateProgress, Application application)
        {
            var query =
                (from rule in application.RuleElements
                 from ruleEvent in rule.Events
                 where
                    !application.EventElements.Any(
                        evt =>
                            evt.Name == ruleEvent.Name &&
                            evt.Version == ruleEvent.Version)
                 select
                     new
                     {
                         Rule = rule,
                         Event = ruleEvent
                     });

            foreach (var result in query)
            {
                pGenerateProgress.GeneratorError(
                    0, 0,
                    string.Format(
                        "Event {0} version {1} on Rule {2} version {3} doesn't " +
                        "match with any existent event on {4} application",
                        result.Event.Name,
                        result.Event.Version,
                        result.Rule.Name,
                        result.Rule.Version,
                        application.Name), 0xFFFFFFFF, 0xFFFFFFFF);
            }

            return query.Count() > 0;
        }

        private bool conditionEventsCheck(IVsGeneratorProgress pGenerateProgress, Application application)
        {
            var query =
                (from condition in application.ConditionElements
                 from conditionEvent in condition.Events
                 where
                    !application.EventElements.Any(
                        evt =>
                            evt.Name == conditionEvent.Name &&
                            evt.Version == conditionEvent.Version)
                 select
                     new
                     {
                         Condition = condition,
                         Event = conditionEvent
                     });

            foreach (var result in query)
            {
                pGenerateProgress.GeneratorError(
                    0, 0,
                    string.Format(
                        "Event {0} version {1} on Condition {2} version {3} doesn't " +
                        "match with any existent event on {4} application",
                        result.Event.Name,
                        result.Event.Version,
                        result.Condition.Name,
                        result.Condition.Version,
                        application.Name), 0xFFFFFFFF, 0xFFFFFFFF);
            }

            return query.Count() > 0;
        }

        private bool actionOutputEventsCheck(IVsGeneratorProgress pGenerateProgress, Application application)
        {
            var query =
                (from action in application.ActionElements
                 from outputEvent in action.OutputEvents
                 where
                    !application.EventElements.Any(
                        evt =>
                            evt.Name == outputEvent.Name &&
                            evt.Version == outputEvent.Version)
                 select
                     new
                     {
                         Action = action,
                         Event = outputEvent
                     });

            foreach (var result in query)
            {
                pGenerateProgress.GeneratorError(
                    0, 0,
                    string.Format(
                        "Data {0} version {1} on Event {2} version {3} doesn't " +
                        "match with any existent data on {4} application",
                        result.Event.Name,
                        result.Event.Version,
                        result.Action.Name,
                        result.Action.Version,
                        application.Name), 0xFFFFFFFF, 0xFFFFFFFF);
            }

            return query.Count() > 0;
        }

        private bool actionInputDataCheck(IVsGeneratorProgress pGenerateProgress, Application application)
        {
            var query =
                (from action in application.ActionElements
                 from inputData in action.InputData
                 where
                    !application.DataElements.Any(
                        data =>
                            data.Name == inputData.Name &&
                            data.Version == inputData.Version)
                 select
                     new
                     {
                         Action = action,
                         Data = inputData
                     });

            foreach (var result in query)
            {
                pGenerateProgress.GeneratorError(
                    0, 0,
                    string.Format(
                        "Data {0} version {1} referenced in Action {2} version {3} doesn't " +
                        "match with any existent {4} application data",
                        result.Data.Name,
                        result.Data.Version,
                        result.Action.Name,
                        result.Action.Version,
                        application.Name), 0xFFFFFFFF, 0xFFFFFFFF);
            }

            return query.Count() > 0;
        }

        private bool eventContentDataCheck(IVsGeneratorProgress pGenerateProgress, Application application)
        {
            var query =
                (from evt in application.EventElements
                 from eventData in evt.ContentData
                 where
                    !application.DataElements.Any(
                        data =>
                            data.Name == eventData.Name &&
                            data.Version == eventData.Version)
                 select
                     new
                     {
                         Event = evt,
                         Data = eventData
                     });

            foreach (var result in query)
            {
                pGenerateProgress.GeneratorError(
                    0, 0,
                    string.Format(
                        "Data {0} version {1} referenced in Event {2} version {3} doesn't " +
                        "match with any existent {4} application data",
                        result.Data.Name,
                        result.Data.Version,
                        result.Event.Name,
                        result.Event.Version,
                        application.Name), 0xFFFFFFFF, 0xFFFFFFFF);
            }

            return query.Count() > 0;
        }

        private bool dataFieldsCheck(IVsGeneratorProgress pGenerateProgress, Application application)
        {
            var query =
                (from data in application.DataElements
                 from dataField in data.Fields
                 where
                    !application.FieldElements.Any(
                        field =>
                            field.Name == dataField.Name &&
                            field.Version == dataField.Version)
                 select
                     new
                     {
                         Data = data,
                         Field = dataField
                     });

            foreach (var result in query)
            {
                pGenerateProgress.GeneratorError(
                    0, 0,
                    string.Format(
                        "Field {0} version {1} referenced in Data {2} version {3} doesn't " +
                        "match with any existent {4} application field",
                        result.Field.Name,
                        result.Field.Version,
                        result.Data.Name,
                        result.Data.Version,
                        application.Name), 0xFFFFFFFF, 0xFFFFFFFF);
            }

            return query.Count() > 0;
        }

        private bool ruleConditionsCheck(IVsGeneratorProgress pGenerateProgress, Definitions.Application application)
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
                        "Condition {0} version {1} referenced in Rule {2} version {3} doesn't " +
                        "match with any existent {4} application condition",
                        result.Condition.Name,
                        result.Condition.Version,
                        result.Rule.Name,
                        result.Rule.Version,
                        application.Name), 0xFFFFFFFF, 0xFFFFFFFF);
            }

            return query.Count() > 0;
        }
    }
}
