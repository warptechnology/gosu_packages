using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Drawing;

namespace L2Package.DataStructures
{
    public class UColor : IXmlSerializable, IUnrealExportable
    {
        [UEExport]
        public byte r { set; get; }
        [UEExport]
        public byte g { set; get; }
        [UEExport]
        public byte b { set; get; }
        [UEExport]
        public byte a { set; get; }

        public void Deserialize(System.Xml.Linq.XElement element)
        {

            if (element.Attribute("class").Value != "Color")
                throw new Exception("Wrong class.");
            a = Convert.ToByte(Utility.GetElement(element, "a").Value.ToString());
            r = Convert.ToByte(Utility.GetElement(element, "r").Value.ToString());
            g = Convert.ToByte(Utility.GetElement(element, "g").Value.ToString());
            b = Convert.ToByte(Utility.GetElement(element, "b").Value.ToString());
        }

        public System.Xml.Linq.XElement SerializeXML(string Name)
        {
            return new XElement(Name,
                new XAttribute("class", "Color"),
                new XElement("b",b),
                new XElement("g",g),
                new XElement("r",r),
                new XElement("a",a)                
            );
        }

        public Color ToColor()
        {
            return Color.FromArgb(a, r, g, b);
            
        }

        public string UnrealString 
        {
            get
            {
                return Utility.GetExport(this);
            }
        }


        public void ResetTemplate()
        {
            UColor.Template = null;
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

        public string[] PropertiesList
        {
            get
            {
                return Utility.GetExportProperties(this.GetType());
            }
        }
    }
}
