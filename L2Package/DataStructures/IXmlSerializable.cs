using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    interface IXmlSerializable
    {
        void Deserialize(XElement element);
        XElement SerializeXML(string Name);
        
    }

    public class Utility
    {
        public static XElement GetElement(XElement Element, string Search)
        {
            List<XElement> Elements = Element.Elements().Where(E => E.Name.ToString() == Search).ToList();
            return Elements.Count() > 0 ? Elements.First() : null;
        }

        public static T Get<T>(string Name, XElement Element)
        {
            object parsedValue = default(T);
            string Value = Utility.GetElement(Element, Name).Value.ToString();
            try
            {
                parsedValue = Convert.ChangeType(Value, typeof(T), CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (Exception Ex)
            {
                if (typeof(T).IsValueType)
                    return default(T);
                parsedValue = null;
            }
            return (T)parsedValue;

        }
        public static int GetDecimalPlaces(decimal value)
        {
            return BitConverter.GetBytes(decimal.GetBits(value)[3])[2];
        }

        internal static string GetTemplate(string v)
        {
            string FileName = string.Format("Templates\\{0}.txt",v);
            FileName = FileName.Replace("<", "_");
            FileName = FileName.Replace("<", "-");
            if (!File.Exists(FileName))
                return string.Format("No template defined for {0}. You can do it in Properties dialog", v);
            return File.ReadAllText(FileName);
            
        }

        internal static void SetTemplate(string Name, string Templ)
        {
            string FileName = string.Format("Templates\\{0}.txt", Name);
            FileName = FileName.Replace("<", "_");
            FileName = FileName.Replace("<", "-");
            if (!Directory.Exists("Templates")) Directory.CreateDirectory("Templates");
            File.WriteAllText(FileName, Templ);
            Type cls = Type.GetType("L2Package.DataStructures." + Name);
            //if (cls is DataStructures.IUnrealExportable)
            bool IsAssignable = typeof(DataStructures.IUnrealExportable).IsAssignableFrom(cls);
            if (IsAssignable)
            {
                DataStructures.IUnrealExportable obj =
                    (DataStructures.IUnrealExportable)Activator.CreateInstance(cls);
                obj.ResetTemplate();
            }
        }
        public static string[] GetExportProperties(Type cls)
        {
            return cls.GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(UEExportAttribute)))
                .Select(n => n.Name)
                .ToArray();            
        }

        internal static string GetExport(IUnrealExportable Item)
        {
            StringBuilder Builder = new StringBuilder(Item.UnrealStringTemplate);
            
            foreach (string Prop in Item.PropertiesList)
            {
                PropertyInfo Info = Item.GetType().GetProperty(Prop);
                Type cls = Info.PropertyType;
                if (typeof(DataStructures.IUnrealExportable).IsAssignableFrom(cls))
                {
                    IUnrealExportable Ex = (IUnrealExportable)Info.GetValue(Item, null);
                    Builder.Replace(string.Format("%{0}%", Prop), Ex.UnrealString);
                }
                else
                {
                    if (cls == typeof(Single) || cls == typeof(Double))
                    {
                        double val = 0.0;
                        if (cls == typeof(Single))
                            val = (Single)Info.GetValue(Item, null);
                        if (cls == typeof(Double))
                            val = (Double)Info.GetValue(Item, null);
                        //string str = string.Format("{0:C2}",);
                        Builder.Replace(string.Format("%{0}%", Prop), val.ToString("0.0##############", CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        Builder.Replace(string.Format("%{0}%", Prop), Info.GetValue(Item, null).ToString());
                    }
                }
            }
            return Builder.ToString();
        }
    }
}


