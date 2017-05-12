using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    internal class NotSupported : IUnrealExportable, IXmlSerializable
    {
        public string[] PropertiesList
        {
            get
            {
                return new string[]{ "" };
            }
        }

        public string UnrealString
        {
            get
            {
                return "";
            }
        }

        public void ResetTemplate()
        {
        }
        public string UnrealStringTemplate
        {
            get
            {
                return "";
            }

            set
            {
                return;
            }
        }

        public XElement SerializeXML(string Name)
        {
            return new XElement("NotSupported");
        }

        public void Deserialize(XElement element)
        {
            return;
        }
    }
}
