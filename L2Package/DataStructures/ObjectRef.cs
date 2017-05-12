using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class ObjectRef : IXmlSerializable, IUnrealExportable
    {
        // <brush class="ObjectRef">
        //   <Path>17_21.Model283</Path>
        public ObjectRef()
        {
            Path = "";
            index = 0;
        }
        [UEExport]
        public string Path { set; get; }
        //   <index>2243</index>
        [UEExport]
        public int index { set; get; }
        // </brush>


        public static bool operator ==(ObjectRef left, ObjectRef right)
        {
            return left.Path == right.Path &&
                left.index == right.index;
        }
        public static bool operator !=(ObjectRef left, ObjectRef right)
        {
            return !(left == right);
        }

        public void Deserialize(System.Xml.Linq.XElement element)
        {
            if (element == null ||
                string.IsNullOrWhiteSpace(element.Attribute("class").Value))            
                return;
            
            if (element.Attribute("class").Value != "ObjectRef")
                throw new Exception("Wrong class.");
            index = Utility.Get<int>("index", element);
            Path = Utility.GetElement(element, "Path").Value.ToString();
        }

        [UEExport]
        public string Mesh
        {
            get { return Path.Substring(Path.LastIndexOf(".") + 1); }
        }

        public System.Xml.Linq.XElement SerializeXML(string Name)
        {
            return new XElement(
                Name,
                new XAttribute("class", "ObjectRef"),
                new XElement("Path", Path),
                new XElement("index", index)
                );
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
            ObjectRef.Template = null;
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
