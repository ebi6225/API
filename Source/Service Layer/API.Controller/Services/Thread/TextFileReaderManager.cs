using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace API.Controller.Services.Thread{
    public class TextFileReaderManager
    {
        #region Properties

        public List<string> FileLines { get; set; }

        public enumBulkSchemaType SchemaType { get; set; }
        public string SchemaName { get; set; }

        public string ItemListJSONstring { get; set; }

        public string HeaderJSONstring { get; set; }

        public string FooterJSONstring { get; set; }

        public string ItemType { get; set; }

        public string HeaderType { get; set; }

        public string FooterType { get; set; }

        public string AssemblyName { get; set; }

        public string HeaderString { get; set; }

        public string FooterString { get; set; }

        public List<SectionInfo> SectionInfoList { get; set; }

        #endregion

        private SectionCollection sections = new SectionCollection();
        JsonSerializer serializer = new JsonSerializer();
        private XmlDocument fileSchema { get; set; }

        public async Task ExecuteAsync(CancellationToken token = default)
        {
            if (FileLines == null)
                return;

            fileSchema = await GetSchemaByTranCodeAsync(SchemaType, token);
            if (fileSchema == null)
                return;

            ParseSchema(fileSchema);

            if (SectionInfoList == null)
                return;

            foreach (var sInfo in SectionInfoList)
            {
                ReadSection(FileLines, sInfo);
            }
        }

        private void ReadSection(List<string> lines, SectionInfo sectionInfo)
        {
            Section currSection = null;

            foreach (Section s in sections)
            {
                if (s.Name == sectionInfo.Name)
                {
                    currSection = s;
                    break;
                }
            }

            if (sectionInfo.Name.ToLower() == "header")
            {
                HeaderString = lines.FirstOrDefault(c =>
                    !string.IsNullOrEmpty(c) &&
                    c.Substring(0, sectionInfo.IndicatorLength).Trim() == sectionInfo.Indicator);
                if (currSection != null && !string.IsNullOrEmpty(HeaderType))
                {
                    HeaderJSONstring = GetSectionJSONstring(HeaderString, currSection, sectionInfo, HeaderType);
                }
                else
                {
                    HeaderJSONstring = GetSectionJSONstring(lines.FirstOrDefault(), currSection, sectionInfo, HeaderType);
                }
            }

            else if (sectionInfo.Name.ToLower() == "footer")
            {
                FooterString = lines.FirstOrDefault(c =>
                    !string.IsNullOrEmpty(c) &&
                    c.Substring(0, sectionInfo.IndicatorLength).Trim() == sectionInfo.Indicator);

                if (currSection != null && !string.IsNullOrEmpty(FooterType))
                    FooterJSONstring = GetSectionJSONstring(FooterString, currSection, sectionInfo, FooterType);
            }
            else //Body
            {
                if (currSection != null)
                    ItemListJSONstring = GetSectionJSONstringAsListByJObject(lines, currSection, sectionInfo, ItemType);
            }
        }

        private string GetSectionJSONstringAsListByJObject(List<string> lines, Section section, SectionInfo sectionInfo, string SectionType)
        {
            Type type = Type.GetType(SectionType + ", " + AssemblyName);
            var listType = typeof(List<>);

            var instanceListType = listType.MakeGenericType(type);
            var instanceList = (IList)Activator.CreateInstance(instanceListType);

            object instance = null;
            var filteredLines = lines.Where(c => !String.IsNullOrEmpty(c) && c.Substring(0, sectionInfo.IndicatorLength).Trim() == sectionInfo.Indicator);

            foreach (string line in lines.Where(c => !string.IsNullOrEmpty(c) && c.Substring(0, sectionInfo.IndicatorLength).Trim() == sectionInfo.Indicator))
            {
                JObject obj = new JObject();

                foreach (TextField field in section.TextFields)
                {
                    try
                    {
                        AssignPropValue(line, obj, field);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                instance = serializer.Deserialize(new JTokenReader(obj), type);
                instanceList.Add(instance);
            }
            return null; //JSONSerializer.SimpleSerialize(instanceList)
        }

        private static void AssignPropValue(string line, JObject instance, TextField field)
        {
            string value = line.Substring(field.StartIndex, field.Length);

            object typeValue = null;

            switch (field.DataType)
            {
                case TypeCode.String:
                    typeValue = value;
                    break;
                case TypeCode.Boolean:
                    typeValue = Boolean.Parse(value);
                    break;

                case TypeCode.SByte:
                case TypeCode.Byte:
                    typeValue = Byte.Parse(value);
                    break;
                case TypeCode.Char:
                    typeValue = Char.Parse(value);
                    break;

                case TypeCode.DBNull:
                    break;

                case TypeCode.DateTime:
                    if (!string.IsNullOrEmpty(field.Format))
                    {
                        typeValue = DateTime.ParseExact(value, field.Format, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        typeValue = DateTime.Parse(value);
                    }
                    break;

                case TypeCode.Decimal:
                    if (!string.IsNullOrEmpty(field.Format))
                    {
                        typeValue = (Decimal.Parse(value) / 100).ToString(field.Format);
                    }
                    else
                    {
                        typeValue = Decimal.Parse(value);
                    }
                    break;

                case TypeCode.Double:
                    typeValue = Double.Parse(value);
                    break;

                case TypeCode.UInt16:
                case TypeCode.Int16:
                    typeValue = Int16.Parse(value);
                    break;
                case TypeCode.UInt32:
                case TypeCode.Int32:
                    typeValue = Int32.Parse(value);
                    break;
                case TypeCode.UInt64:
                case TypeCode.Int64:
                    typeValue = Int64.Parse(value);
                    break;

                case TypeCode.Object:
                    break;

                case TypeCode.Single:
                    typeValue = Single.Parse(value);
                    break;

                default:
                    break;
            }
            instance[field.Name] = JToken.FromObject(typeValue);
        }

        private string GetSectionJSONstring(string line, Section section, SectionInfo sectionInfo, string SectionType)
        {
            Type type = Type.GetType(SectionType + ", " + AssemblyName);

            JObject obj = new JObject();

            foreach (TextField field in section.TextFields)
            {
                try
                {
                    AssignPropValue(line, obj, field);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            object instance = serializer.Deserialize(new JTokenReader(obj), type);
            return /*JSONSerializerHelper.SimpleSerialize(instance)*/null;
        }

        private async Task<XmlDocument> GetSchemaByTranCodeAsync(enumBulkSchemaType SchemaType, CancellationToken token = default)
        {
            // There will be a table as follow
   //         BULK_SCHEMA and follow columns
   //             "ID",
			//"SCHEMA_TYPE",
			//"SCHEMA_NAME",
			//"BULK_FILE_SCHEMA"

            //BULK_SCHEMA schema = await new TextReaderManager().GetBulkSchemaAsync(SchemaType, token);
            //if (schema != null)
            //{
            //    XmlDocument doc = new XmlDocument();
            //    doc.LoadXml(schema.BULK_FILE_SCHEMA);
            //    return doc;
            //}
            return null;
        }

        private void ParseSchema(XmlDocument doc)
        {

            XmlNodeList lst = doc.GetElementsByTagName("SECTION");

            if (lst.Count == 0)
                throw new XmlException("Could not locate the 'TABLE' node." + Environment.NewLine +
                    "The Schema is case sensitive.");

            // Process Each Section added to Sections Collection
            foreach (XmlNode tableNode in lst)
            {
                string Name = "";
                SectionFormat fileformat = SectionFormat.FixedWidth;

                foreach (XmlAttribute attribute in tableNode.Attributes)
                {
                    switch (attribute.Name.ToLower())
                    {
                        case "name": Name = attribute.Value; break;
                        case "sectionformat": fileformat = (SectionFormat)Enum.Parse(typeof(SectionFormat), attribute.Value); break;
                    }
                }
                Section section = new Section(Name, fileformat);
                TextFieldCollection m_TextFields = ParseSchemaSection(tableNode);
                section.TextFields = m_TextFields;
                sections.Add(section);
            }
        }

        private TextFieldCollection ParseSchemaSection(XmlNode tableNode)
        {
            TextFieldCollection m_TextFields = new TextFieldCollection();
            XmlNodeList lst = tableNode.ChildNodes; // GetGetElementsByTagName("FIELD");
            TextField field;
            string name = "";
            TypeCode datatype;
            int length = 0;
            int startindex = 0;
            string format = "";

            foreach (XmlNode node in lst)
            {
                name = "";
                datatype = TypeCode.String;
                length = 0;
                startindex = -1;
                format = "";
                foreach (XmlAttribute fattribute in node.Attributes)
                {
                    switch (fattribute.Name.ToLower())
                    {
                        case "name": name = fattribute.Value; break;
                        case "datatype": datatype = (TypeCode)Enum.Parse(typeof(TypeCode), fattribute.Value); break;
                        case "length": length = Int32.Parse(fattribute.Value); break;
                        case "startindex": startindex = int.Parse(fattribute.Value); break;
                        case "format": format = fattribute.Value; break;

                    }
                }
                field = new TextField(name, datatype, length, startindex, format);
                m_TextFields.Add(field);
            }
            return m_TextFields;
        }
    }

    public enum enumBulkSchemaType
    {
        [Description("Description")]
        Description = 1,        
    }
}
public class SectionInfo
{
    public string Name { get; set; }
    public string Indicator { get; set; }
    public int IndicatorLength { get; set; }
}