using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class StaticMeshActor : AActor, IXmlSerializable, IUnrealExportable
    {
        [UEExport]
        public AActor Actor {
            get
            {
                AActor A = new AActor();
                A.bBlockActors = this.bBlockActors;
                A.bBlockPlayers = this.bBlockPlayers;
                A.bCollideActors = this.bCollideActors;
                A.bDeleteMe = this.bDeleteMe;
                A.bHidden = this.bHidden;
                A.draw_scale = this.draw_scale;
                A.draw_scale_3d = this.draw_scale_3d;
                A.flags = this.flags;
                A.location = this.location;
                A.name = this.name;
                A.pre_pivot = this.pre_pivot;
                A.rotation = this.rotation;
                A.static_mesh = this.static_mesh;
                A.name = this.name;
                return A;
            }
        }
        
        public static bool operator ==(StaticMeshActor left, StaticMeshActor right)
        {
            return left.Actor == right.Actor;
        }
        public static bool operator !=(StaticMeshActor left, StaticMeshActor right) { return !(left == right); }

        public StaticMeshActor() :base()
        {
            
        }

        public new void Deserialize(XElement element)
        {
            if (element.Attribute("class").Value != "StaticMeshActor")
                throw new Exception("Wrong class.");
            base.Deserialize(Utility.GetElement(element, element.Name.ToString()));
        }

        public new void ResetTemplate()
        {
            StaticMeshActor.Template = null;
        }
        public new XElement SerializeXML(string Name)
        {
            return new XElement(base.name,
                new XAttribute("class", "StaticMeshActor"),
                base.SerializeXML(Name)
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
                StaticMeshActor.Template = value;
                Utility.SetTemplate(typeof(StaticMeshActor).Name, StaticMeshActor.Template);
            }
            get
            {
                if (!string.IsNullOrEmpty(StaticMeshActor.Template)) return StaticMeshActor.Template;
                StaticMeshActor.Template = Utility.GetTemplate(typeof(StaticMeshActor).Name);
                return StaticMeshActor.Template;
            }
        }



        public new string[] PropertiesList
        {
            get
            {
                return Utility.GetExportProperties(this.GetType());
            }
        }
    }
}
