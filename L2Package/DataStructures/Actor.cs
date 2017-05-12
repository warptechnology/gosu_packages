using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class AActor : UObject, IXmlSerializable, IUnrealExportable
    {
        [UEExport]
        public int bBlockActors { set; get; }
        [UEExport]
        public int bBlockPlayers { set; get; }
        [UEExport]
        public int bCollideActors { set; get; }
        [UEExport]
        public int bDeleteMe { set; get; }
        [UEExport]
        public int bHidden { set; get; }
        [UEExport]
        public double draw_scale { set; get; }
        [UEExport]
        public UVector location { get; set; }
        [UEExport]
        public URotator rotation { get; set; }
        [UEExport]
        public UVector draw_scale_3d { get; set; }
        [UEExport]
        public UVector pre_pivot { get; set; }
        [UEExport]
        public ObjectRef static_mesh { get; set; }
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

        public static bool operator ==(AActor left, AActor right)
        {
            return left.bBlockActors == right.bBlockActors &&
                left.bBlockPlayers == right.bBlockPlayers &&
                left.bCollideActors == right.bCollideActors &&
                left.bDeleteMe == right.bDeleteMe &&
                left.bHidden == right.bHidden &&
                left.draw_scale == right.draw_scale &&
                left.draw_scale_3d == right.draw_scale_3d &&
                left.flags == right.flags &&
                left.location == right.location &&
                left.name == right.name &&
                left.pre_pivot == right.pre_pivot &&
                left.rotation == right.rotation &&
                left.static_mesh == right.static_mesh;
        }
        public static bool operator !=(AActor left, AActor right) { return !(left == right); }
        

        [UEExport]
        public UVector UU4_Draw_Scale_3D
        {
            get
            {
                return new UVector()
                {
                    X = draw_scale_3d.X * draw_scale,
                    Y = draw_scale_3d.Y * draw_scale,
                    Z = draw_scale_3d.Z * draw_scale
                };
            }
        }

        public AActor() : base()
        {
            location = new UVector();
            rotation = new URotator();
            draw_scale_3d = new UVector();
            pre_pivot = new UVector();
            static_mesh = new ObjectRef();
        }

        public new void Deserialize(XElement element)
        {
            if (element.Attribute("class").Value != "AActor")            
                return;            
            base.Deserialize(Utility.GetElement(element, element.Name.ToString()));
            bBlockActors = Utility.Get<int>("bBlockActors", element);
            bBlockPlayers = Utility.Get<int>("bBlockPlayers", element);
            bCollideActors = Utility.Get<int>("bCollideActors", element);
            bDeleteMe = Utility.Get<int>("bDeleteMe", element);
            bHidden = Utility.Get<int>("bHidden", element);
            location.Deserialize(Utility.GetElement(element, "location"));
            rotation.Deserialize(Utility.GetElement(element, "rotation"));
            draw_scale = Utility.Get<double>("draw_scale", element);
            draw_scale_3d.Deserialize(Utility.GetElement(element, "draw_scale_3d"));
            pre_pivot.Deserialize(Utility.GetElement(element, "pre_pivot"));
            static_mesh.Deserialize(Utility.GetElement(element, "static_mesh"));
        }

        public new XElement SerializeXML(string Name)
        {
            return new XElement(base.name,
                new XAttribute("class", "AActor"),
                base.SerializeXML(name),
                new XElement("bBlockActors", bBlockActors.ToString()),
                new XElement("bBlockPlayers", bBlockPlayers.ToString()),
                new XElement("bCollideActors", bCollideActors.ToString()),
                new XElement("bDeleteMe", bDeleteMe.ToString()),
                new XElement("bHidden", bHidden.ToString()),
                new XElement("draw_scale", draw_scale.ToString(CultureInfo.InvariantCulture)),
                location.SerializeXML("location"),
                rotation.SerializeXML("rotation"),
                draw_scale_3d.SerializeXML("draw_scale_3d"),
                pre_pivot.SerializeXML("pre_pivot"),
                static_mesh.SerializeXML("static_mesh")
            );
        }

        public new string UnrealString
        {
            get
            {
                return Utility.GetExport(this);
            }
        }

        private static string Template = null;


        public new string UnrealStringTemplate
        {
            set
            {
                AActor.Template = value;
                Utility.SetTemplate(typeof(AActor).Name, AActor.Template);
            }
            get
            {
                if (!string.IsNullOrEmpty(AActor.Template)) return AActor.Template;
                string tmp = typeof(AActor).Name;
                AActor.Template = Utility.GetTemplate(tmp);
                return AActor.Template;
            }
        }

        public new string[] PropertiesList
        {             
            get
            {
                return Utility.GetExportProperties(this.GetType());
            }
        }

        private string[] test
        {
            get
            {
                return this.GetType().GetProperties().Where(
                    prop => Attribute.IsDefined(prop, typeof(UEExportAttribute)))
                    .Select(n => n.Name)
                    .ToArray();
            }
        }
        public new void ResetTemplate()
        {
            AActor.Template = null;
        }
    }
}
