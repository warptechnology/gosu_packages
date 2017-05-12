using System;
using System.Globalization;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class UVertex : IXmlSerializable, IUnrealExportable
    {
        [UEExport]
        public int vertex { set; get; }
        [UEExport]
        public int side { set; get; }

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
                UVertex.Template = value;
                Utility.SetTemplate(typeof(UVertex).Name, UVertex.Template);
            }
            get
            {
                if (!string.IsNullOrEmpty(UVertex.Template)) return UVertex.Template;
                string tmp = typeof(UVertex).Name;
                UVertex.Template = Utility.GetTemplate(tmp);
                return UVertex.Template;
            }
        }

        public XElement SerializeXML(string Name)
        {
            return new XElement(Name,
                new XAttribute("class", "Vertex"),
                new XElement("vertex", vertex.ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("side", side.ToString(NumberFormatInfo.InvariantInfo))
                );
        }

        public void ResetTemplate()
        {
            Template = "";
        }

        public void Deserialize(XElement element)
        {
            vertex = Utility.Get<int>("vertex", element);
            side = Utility.Get<int>("side", element);
        }
    }
}