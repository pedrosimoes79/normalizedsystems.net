using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NormalizedSystems.Net
{
    public static class Utils
    {
        /// <summary>
        /// Method that returns all the duplicates (distinct) in the collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="source">The source collection to detect for duplicates</param>
        /// <param name="distinct">Specify <b>true</b> to only return distinct elements.</param>
        /// <returns>A distinct list of duplicates found in the source collection.</returns>
        /// <remarks>This is an extension method to IEnumerable&lt;T&gt;</remarks>
        public static IEnumerable<T> Duplicates<T>
                 (this IEnumerable<T> source, bool distinct = true)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            // select the elements that are repeated
            IEnumerable<T> result = source.GroupBy(a => a).SelectMany(a => a.Skip(1));

            // distinct?
            if (distinct == true)
            {
                // deferred execution helps us here
                result = result.Distinct();
            }

            return result;
        }

        public static string ToLowerCamelCase(string stringValue)
        {
            string result = String.Empty;

            if (!String.IsNullOrEmpty(stringValue))
            {
                result += Char.ToLower(stringValue[0]) + stringValue.Substring(1);
            }

            return result;
        }

        private static Dictionary<string, string> textResources =
            new Dictionary<string, string>();

        public static string GetTextEmbeddedResource(string name, Assembly assembly)
        {
            if (!textResources.ContainsKey(name))
            {
                using (var stream = assembly.GetManifestResourceStream(assembly.GetManifestResourceNames().Where(r => r.ToUpperInvariant().Contains(name.ToUpperInvariant())).FirstOrDefault()))
                {
                    if (stream == default(Stream))
                        throw new FileNotFoundException(string.Format("Resource not found: {0}", name));

                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            else
            {
                return textResources[name];
            }
        }

        public class SchemaValidationException : Exception
        {
            public uint LineNumber { get; }

            public SchemaValidationException(string message, uint linenumber) : base(message)
            {
                this.LineNumber = linenumber;
            }
        }

        public static void ValidateXml(string xmlString, string schemaString)
        {
            try
            {
                using (var sr = new StringReader(schemaString))
                {
                    using (var xr = new XmlTextReader(sr))
                    {
                        var settings = new XmlReaderSettings();
                        settings.Schemas.Add(null, xr);
                        settings.ValidationType = ValidationType.Schema;

                        using (var reader = XmlReader.Create(new StringReader(xmlString), settings))
                        {
                            try
                            {
                                while (reader.Read()) { }
                            }
                            catch(Exception ex)
                            {
                                var line = (int)reader.GetType().GetProperty("LineNumber", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(reader);

                                throw new SchemaValidationException(ex.Message, Convert.ToUInt32(line));
                            }
                        }
                    }
                }
            }
            catch(SchemaValidationException svex)
            {
                throw svex;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException(string.Format("Schema validation error: {0}", ex.Message), ex);
            }
        }

        private readonly static Dictionary<string, string> mappingTable = new Dictionary<string, string>();

        public static string TypeMapper(string input)
        {
            if (mappingTable.Count == 0)
            {
                mappingTable["Boolean"] = "bool";
                mappingTable["Integer"] = "int";
                mappingTable["Long"] = "long";
                mappingTable["Int8"] = "sbyte";
                mappingTable["Int16"] = "short";
                mappingTable["Int32"] = "int";
                mappingTable["Int64"] = "long";
                mappingTable["UInteger"] = "uint";
                mappingTable["ULong"] = "ulong";
                mappingTable["UInt8"] = "byte";
                mappingTable["UInt16"] = "ushort";
                mappingTable["UInt32"] = "uint";
                mappingTable["UInt64"] = "ulong";
                mappingTable["Float"] = "float";
                mappingTable["Double"] = "double";
                mappingTable["Decimal"] = "decimal";
                mappingTable["String"] = "string";
                mappingTable["DateTime"] = "DateTime";
                mappingTable["TimeSpan"] = "TimeSpan";
            }

            if (mappingTable.ContainsKey(input))
                return mappingTable[input];
            else  //TODO find DataElements & Support arrays/collections
                return input;
        }
    }
}
