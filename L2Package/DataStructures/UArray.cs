using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace L2Package.DataStructures
{

    public class UArray<T> : List<T>, IUnrealExportable, IXmlSerializable where T : IUnrealExportable
    {
        public UArray() : base() { }
        public UArray(IEnumerable<T> collection) : base(collection) { }
        public UArray(int capacity) : base(capacity) { }

        [UEExport]
        public string Export
        {
            get
            {
                if (typeof(T) == typeof(UVector))
                    return "UVector";
                if (typeof(T) == typeof(BSPNode))
                    return "BSPNode";
                if (typeof(T) == typeof(BSPSurface))
                    return "BSPSurface";
                if (typeof(T) == typeof(UVertex))
                    return "UVertex";
                return "NotSupported";
            }
        }
        #region IUnrealExportable


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
                UArray<T>.Template = value;
                Utility.SetTemplate(typeof(UArray<T>).Name, UArray<T>.Template);
            }
            get
            {
                if (!string.IsNullOrEmpty(UArray<T>.Template)) return UArray<T>.Template;
                string tmp = typeof(UArray<T>).Name;
                UArray<T>.Template = Utility.GetTemplate(tmp);
                return UArray<T>.Template;
            }
        }
        #endregion
        #region IXmlSerializable
        public void Deserialize(XElement element)
        {
            throw new NotImplementedException();
        }
        
        public void ResetTemplate()
        {
            throw new NotImplementedException();
        }

        public XElement SerializeXML(string Name)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
