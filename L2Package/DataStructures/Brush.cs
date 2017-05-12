using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class Brush: AActor, IXmlSerializable, IUnrealExportable
    {
        [UEExport]
        public ECSGOperation csg_operation { set; get; }
        [UEExport]
        public UColor Color { get; set; }
        [UEExport]
        public long poly_flags { set; get; } 
        [UEExport]
        public int bColored { get; set; }
        [UEExport]
        public ObjectRef brush { get; set; }
        [UEExport]
        public Brush()
        {
            Color = new UColor();
            brush = new ObjectRef();
        }

        [UEExport]
        public UModel model
        {
            get
            {
                if (OriginalElement.Parent != null)
                {
                    XElement ParentElement = OriginalElement.Ancestors("root").First();
                    if(ParentElement != null)
                    {
                        var AllElemnts = ParentElement.Elements().ToList();
                        foreach(XElement Element in AllElemnts)
                        {
                            if (Element.Parent.Name.ToString() != ParentElement.Name.ToString()) continue;
                            if (Element.HasAttributes &&
                                !string.IsNullOrEmpty(Element.Attribute("class").Value) &&
                                Element.Attribute("class").Value == "UModel")
                            {
                                UModel Model = new UModel();
                                Model.Deserialize(Element);
                                if(Model.name == this.brush.Path.Substring(this.brush.Path.LastIndexOf(".") + 1))
                                {
                                    return Model;
                                }
                            }
                        }

                    }
                }
                return new UModel();
            }
        }

        [UEExport]
        public AActor Actor
        {
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
                A.flags = this.flags;
                A.name = this.name;
                return A;
            }
        }

        private XElement OriginalElement { set; get; }

        public new void ResetTemplate()
        {
            Brush.Template = null;
        }
        public new void Deserialize(XElement element)
        {
            if (element.Attribute("class").Value != "Brush")
                throw new Exception("Wrong class.");
            OriginalElement = element;
            base.Deserialize(Utility.GetElement(element, element.Name.ToString()));
            try
            {
                csg_operation = (ECSGOperation)Enum.Parse(typeof(ECSGOperation),Utility.GetElement(element, "csg_operation").Value.ToString());
            }
            catch (Exception Ex)
            {
                ;
            }
            Color.Deserialize(Utility.GetElement(element, "Color"));
            poly_flags = Utility.Get<long>("poly_flags", element);
            bColored = Utility.Get<int>("bColored", element);
            brush.Deserialize(Utility.GetElement(element, "brush"));
        }

        public new XElement SerializeXML(string Name)
        {
            return new XElement(
                Name,
                new XAttribute("class","Brush"),
                base.SerializeXML(name),
                Color.SerializeXML("Name"),
                new XElement("poly_flags", poly_flags),
                new XElement("bColored", bColored),
                brush.SerializeXML("brush")
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


        public new string[] PropertiesList
        {
            get
            {
                return Utility.GetExportProperties(this.GetType());
            }
        }
    }
}
