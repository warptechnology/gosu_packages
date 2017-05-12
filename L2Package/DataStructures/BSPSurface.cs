using System;
using System.Globalization;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class BSPSurface : IXmlSerializable, IUnrealExportable
    {
        [UEExport]
        public ObjectRef material { set; get; }
        [UEExport]
        public uint flags { set; get; }
        [UEExport]
        public int Base { set; get; }
        [UEExport]
        public int normal { set; get; }
        [UEExport]
        public int U { set; get; }
        [UEExport]
        public int V { set; get; }
        [UEExport]
        public ObjectRef brush_poly { set; get; }
        [UEExport]
        public ObjectRef actor { set; get; }
        [UEExport]
        public Plane plane { set; get; }
        [UEExport]
        public uint[] unk { set; get; }

        public BSPSurface()
        {
            material = new ObjectRef();
            brush_poly = new ObjectRef();
            actor = new ObjectRef();
            plane = new Plane();
            unk = new uint[2];
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
                BSPSurface.Template = value;
                Utility.SetTemplate(typeof(BSPSurface).Name, BSPSurface.Template);
            }
            get
            {
                if (!string.IsNullOrEmpty(BSPSurface.Template)) return BSPSurface.Template;
                string tmp = typeof(BSPSurface).Name;
                BSPSurface.Template = Utility.GetTemplate(tmp);
                return BSPSurface.Template;
            }
        }

        public XElement SerializeXML(string Name)
        {
            return new XElement(Name,
                material.SerializeXML("UMaterial"),
                new XElement("flags", flags.ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("base", Base.ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("normal", normal.ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("U", U.ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("V", V.ToString(NumberFormatInfo.InvariantInfo)),
                brush_poly.SerializeXML("brush_poly"),
                actor.SerializeXML("actor"),
                plane.SerializeXML("plane"),
                new XElement("ubk_0", unk[0].ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("unk_0", unk[1].ToString(NumberFormatInfo.InvariantInfo))
                );
        }

        public void ResetTemplate()
        {
            BSPSurface.Template = "";
        }

        public void Deserialize(XElement element)
        {
            flags = Utility.Get<uint>("flags", element);
            Base = Utility.Get<int>("base", element);
            normal = Utility.Get<int>("normal", element);
            U = Utility.Get<int>("U", element);
            V = Utility.Get<int>("V", element);
            brush_poly.Deserialize(Utility.GetElement(element, "brush_poly"));
            plane.Deserialize(Utility.GetElement(element, "plane"));
            unk[0] = Utility.Get<uint>("unk_0", element);
            unk[1] = Utility.Get<uint>("unk_1", element);
            

        }
    }
}