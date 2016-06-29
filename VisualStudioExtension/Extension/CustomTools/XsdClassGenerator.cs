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

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using VSLangProj80;
using System.CodeDom.Compiler;
using System.Threading.Tasks;
using Microsoft.CSharp;
using NormalizedSystems.Net;
using System.Threading;

namespace NormalizedSystems.Net.CustomTools
{
    [ComVisible(true)]
    [Guid("5BCA81B5-7EEF-4310-A574-AE2A0FE3FED1")]
    [CodeGeneratorRegistration(typeof(XsdClassGenerator), "C# XSD File Expander", vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [ProvideObject(typeof(XsdClassGenerator))]
    public class XsdClassGenerator : BaseCustomTool
    {
        public override string generate(string input, string ns, string inputfile, IVsGeneratorProgress pGenerateProgress)
        {
            var output = default(string);
            var filename = Path.GetFileName(inputfile);
            var name = filename.Substring(0, filename.IndexOf('.'));

            using (var sr = new StringReader(input))
            {
                using (var xr = new XmlTextReader(sr))
                {
                    var xsd = XmlSchema.Read(xr, null);

                    var xsds = new XmlSchemas();
                    xsds.Add(xsd);
                    xsds.Compile(null, true);
                    XmlSchemaImporter schemaImporter = new XmlSchemaImporter(xsds);

                    // create the codedom
                    var cns = new CodeNamespace(ns + '.' + name);
                    var codeExporter = new XmlCodeExporter(cns, new CodeCompileUnit() { }, CodeGenerationOptions.EnableDataBinding | CodeGenerationOptions.GenerateProperties);

                    var maps = new List<XmlTypeMapping>();
                    foreach (XmlSchemaType schemaType in xsd.SchemaTypes.Values)
                    {
                        maps.Add(schemaImporter.ImportSchemaType(schemaType.QualifiedName));
                    }

                    foreach (XmlSchemaElement schemaElement in xsd.Elements.Values)
                    {
                        maps.Add(schemaImporter.ImportTypeMapping(schemaElement.QualifiedName));
                    }

                    foreach (XmlTypeMapping map in maps)
                    {
                        codeExporter.ExportTypeMapping(map);
                    }

                    // Check for invalid characters in identifiers
                    CodeGenerator.ValidateIdentifiers(cns);

                    // output the C# code
                    CSharpCodeProvider codeProvider = new CSharpCodeProvider();

                    using (StringWriter writer = new StringWriter())
                    {
                        codeProvider.GenerateCodeFromNamespace(cns, writer, new CodeGeneratorOptions() { });

                        output = writer.GetStringBuilder().ToString();
                    }
                }
            }

            return output;
        }
    }
}
