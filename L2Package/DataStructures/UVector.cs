using System;
using System.Reflection;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class UVector : IXmlSerializable, IUnrealExportable
    {
        [UEExport]
        public double X { set; get; }
        [UEExport]
        public double Y { set; get; }
        [UEExport]
        public double Z { set; get; }

        public void Deserialize(XElement element)
        {
            if (element.Attribute("class").Value != "Vector")
                throw new Exception("Wrong class.");
            X = Convert.ToDouble(Utility.GetElement(element, "X").Value.ToString(), NumberFormatInfo.InvariantInfo);
            Y = Convert.ToDouble(Utility.GetElement(element, "Y").Value.ToString(), NumberFormatInfo.InvariantInfo);
            Z = Convert.ToDouble(Utility.GetElement(element, "Z").Value.ToString(), NumberFormatInfo.InvariantInfo);
        }

        public static bool operator== (UVector left, UVector right)
        {
            return left.X == right.X &&
                left.Y == right.Y &&
                left.Z == right.Z;
        }
        public static bool operator != (UVector left, UVector right)
        {
            return !(left == right);
        }
        public XElement SerializeXML(string Name)
        {
            return new XElement(Name,
                 new XElement("X", X.ToString(NumberFormatInfo.InvariantInfo)),
                 new XElement("Y", Y.ToString(NumberFormatInfo.InvariantInfo)),
                 new XElement("Z", Z.ToString(NumberFormatInfo.InvariantInfo)),
                 new XAttribute("class", "Vector"));
        }
        
        public string UnrealString
        {
            get
            {
                return Utility.GetExport(this);          
            }
        }

        private static string Template = null;


        public string UnrealStringTemplate
        {
            set
            {
                Template = value;
                Utility.SetTemplate(this.GetType().Name, Template);
            }
            get
            {
                if (!string.IsNullOrEmpty(Template)) return Template;
                Template = Utility.GetTemplate(this.GetType().Name);
                return Template;
            }
        }

        public void ResetTemplate()
        {
            UVector.Template = null;
        }
        public string[] PropertiesList
        {
            get
            {
                return Utility.GetExportProperties(this.GetType());
            }
        }
    }
}
