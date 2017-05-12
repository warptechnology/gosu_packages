using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class UObject: IXmlSerializable, IUnrealExportable
    {
        [UEExport]
        public string name { set; get; }
        [UEExport]
        public int flags { set; get; }

        public UObject()
        {
            name = "None";
            flags = 0;
        }

        public void Deserialize(XElement element)
        {
            if (element.Attribute("class").Value != "UObject")
                throw new Exception("Wrong class.");
            name = element.Name.ToString();
            flags = Utility.Get<int>("flags", element);
        }

        public XElement SerializeXML(string Name)
        {
            return new XElement(name,
                new XAttribute("class", "UObject"),
                new XElement("flags", flags)
                );
        }

        public string UnrealString
        {
            get
            {
                StringBuilder Builder = new StringBuilder(UnrealStringTemplate);
                Builder.Replace("%name%", name);
                Builder.Replace("%flags%", flags.ToString());
                return Builder.ToString();
            }
        }


        public void ResetTemplate()
        {
            UObject.Template = null;
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
