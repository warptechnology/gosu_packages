using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class UPrimitive : UObject, IXmlSerializable, IUnrealExportable
    {
        [UEExport]
        public Box bounding_box { set; get; }

        [UEExport]
        public Sphere bounding_sphere { set; get; }

        [UEExport]
        public UObject UObject
        {
            get
            {
                UObject obj = new UObject();
                obj.flags = this.flags;
                obj.name = this.name;
                return obj;
            }
        }

        public UPrimitive()
        {
            bounding_box = new Box();
            bounding_sphere = new Sphere();
        }

        public new string[] PropertiesList
        {
            get
            {
                return Utility.GetExportProperties(this.GetType());
            }
        }

        public new string UnrealString
        {
            get
            {
                return Utility.GetExport(this);
            }
        }

        private static string Template = "";
        public new string UnrealStringTemplate
        {
            set
            {
                UPrimitive.Template = value;
                Utility.SetTemplate(typeof(UPrimitive).Name, UPrimitive.Template);
            }
            get
            {
                if (!string.IsNullOrEmpty(UPrimitive.Template)) return UPrimitive.Template;
                string tmp = typeof(UPrimitive).Name;
                UPrimitive.Template = Utility.GetTemplate(tmp);
                return UPrimitive.Template;
            }
        }


        public new XElement SerializeXML(string Name)
        {
            return new XElement(base.name,
                bounding_sphere.SerializeXML("bounding_sphere"),
                bounding_box.SerializeXML("bounding_box"),
                new XAttribute("class", "UPrimative")
            );
        }

        public new void ResetTemplate()
        {
            Template = "";
        }

        public new void Deserialize(XElement element)
        {
            if (element.Attribute("class").Value != "UPrimative")
                return;
            base.Deserialize(Utility.GetElement(element, element.Name.ToString()));
            bounding_sphere.Deserialize(Utility.GetElement(element, "bounding_sphere"));
            bounding_box.Deserialize(Utility.GetElement(element, "bounding_box"));
        }
    }
}
