using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class Box : IXmlSerializable, IUnrealExportable
    {
        [UEExport]
        public UVector min { set; get; }
        [UEExport]
        public UVector max { set; get; }
        [UEExport]
        public byte is_valid { set; get; }

        public Box()
        {
            min = new UVector();
            max = new UVector();
            is_valid = 0;
        }

        public string[] PropertiesList
        {
            get
            {
                return Utility.GetExportProperties(this.GetType());
            }
        }

        public string UnrealString
        {
            get
            {
                return Utility.GetExport(this);
            }
        }

        private static string Template = "";
        public string UnrealStringTemplate
        {
            set
            {
                Box.Template = value;
                Utility.SetTemplate(typeof(Box).Name, Box.Template);
            }
            get
            {
                if (!string.IsNullOrEmpty(Box.Template)) return Box.Template;
                string tmp = typeof(Box).Name;
                Box.Template = Utility.GetTemplate(tmp);
                return Box.Template;
            }
        }

        public XElement SerializeXML(string Name)
        {
            return new XElement(Name,
                 min.SerializeXML("min"),
                 min.SerializeXML("max"),
                 new XElement("is_valid", is_valid.ToString(NumberFormatInfo.InvariantInfo)),
                 new XAttribute("class", "Box"));
        }

        public void ResetTemplate()
        {
            Box.Template = "";
        }

        public void Deserialize(XElement element)
        {

            if (element.Attribute("class").Value != "Box")
                throw new Exception("Wrong class.");
            min.Deserialize(Utility.GetElement(element, "min"));
            max.Deserialize(Utility.GetElement(element, "max"));
            is_valid = Utility.Get<byte>("is_valid", element);
        }
    }
}
