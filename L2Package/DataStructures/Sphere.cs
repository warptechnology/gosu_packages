using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class Sphere : IXmlSerializable, IUnrealExportable
    {
        [UEExport]
        public UVector location { set; get; }

        [UEExport]
        public float radius { set; get; }

        public Sphere()
        {
            location = new UVector();
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
                Sphere.Template = value;
                Utility.SetTemplate(typeof(Sphere).Name, Sphere.Template);
            }
            get
            {
                if (!string.IsNullOrEmpty(Sphere.Template)) return Sphere.Template;
                string tmp = typeof(Sphere).Name;
                Sphere.Template = Utility.GetTemplate(tmp);
                return Sphere.Template;
            }
        }


        public XElement SerializeXML(string Name)
        {
            return new XElement(Name,
                 location.SerializeXML("location"),
                 new XElement("radius", radius.ToString(NumberFormatInfo.InvariantInfo)),
                 new XAttribute("class", "Sphere"));
        }

        public void ResetTemplate()
        {
            Sphere.Template = "";
        }

        public void Deserialize(XElement element)
        {
            if (element.Attribute("class").Value != "Sphere")
                throw new Exception("Wrong class.");
            location.Deserialize(Utility.GetElement(element, "location"));
            radius = Utility.Get<float>("radius", element);
        }
    }
}
