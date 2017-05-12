using System;
using System.Globalization;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class Plane : IXmlSerializable, IUnrealExportable
    {
        [UEExport]
        public float X { set; get; }

        [UEExport]
        public float Y { set; get; }

        [UEExport]
        public float Z { set; get; }

        [UEExport]
        public float W { set; get; }

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
                Plane.Template = value;
                Utility.SetTemplate(typeof(Plane).Name, Plane.Template);
            }
            get
            {
                if (!string.IsNullOrEmpty(Plane.Template)) return Plane.Template;
                string tmp = typeof(Plane).Name;
                Plane.Template = Utility.GetTemplate(tmp);
                return Plane.Template;
            }
        }

        public XElement SerializeXML(string Name)
        {
            return new XElement(Name,
                                new XElement("X", X.ToString(NumberFormatInfo.InvariantInfo)),
                                new XElement("Y", Y.ToString(NumberFormatInfo.InvariantInfo)),
                                new XElement("Z", Z.ToString(NumberFormatInfo.InvariantInfo)),
                                new XElement("W", W.ToString(NumberFormatInfo.InvariantInfo)),
                                new XAttribute("class", "Plane"));
        }

        public void ResetTemplate()
        {
            Template = "";
        }

        public void Deserialize(XElement element)
        {
            if (element.Attribute("class").Value != "Plane")
                throw new Exception("Wrong class.");
            X = Utility.Get<float>("X", element);
            Y = Utility.Get<float>("Y", element);
            Z = Utility.Get<float>("Z", element);
            W = Utility.Get<float>("W", element);
        }
    }
}