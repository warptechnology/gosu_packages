using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class UModel : UPrimitive, IXmlSerializable, IUnrealExportable
    {
        public UArray<UVector> vectors { set; get; }
        public UArray<UVector> points { set; get; }
        public UArray<BSPNode> nodes { set; get; }
        public UArray<BSPSurface> surfaces { set; get; }
        public UArray<UVertex> vertexes { set; get; }

        [UEExport]
        public string UEVectors
        {
            get
            {
                string huing = "";
                foreach (UVector vec in vectors)
                    huing += vec.UnrealString;
                return huing;
            }
        }
        [UEExport]
        public string Points
        {
            get
            {
                string huing = "";
                foreach (UVector vec in points)
                    huing += vec.UnrealString;
                return huing;
            }
        }
        [UEExport]
        public string Nodes
        {
            get
            {
                string huing = "";
                foreach (BSPNode vec in nodes)
                    huing += vec.UnrealString;
                return huing;
            }
        }
        [UEExport]
        public string Surfaces
        {
            get
            {
                string huing = "";
                foreach (BSPSurface vec in surfaces)
                    huing += vec.UnrealString;
                return huing;
            }
        }
        [UEExport]
        public string Vertexes
        {
            get
            {
                string huing = "";
                foreach (UVertex vec in vertexes)
                    huing += vec.UnrealString;
                return huing;
            }
        }

        public UModel()
        {
            vectors = new UArray<UVector>();
            points = new UArray<UVector>();
            nodes = new UArray<BSPNode>();
            surfaces = new UArray<BSPSurface>();
            vertexes = new UArray<UVertex>();
        }

        [UEExport]
        public UPrimitive UPrimitive
        {
            get
            {
                UPrimitive obj = new UPrimitive();
                obj.bounding_box = this.bounding_box;
                obj.bounding_sphere = this.bounding_sphere;
                obj.flags = this.flags;
                obj.name = this.name;
                return obj;
            }
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
                UModel.Template = value;
                Utility.SetTemplate(typeof(UModel).Name, UModel.Template);
            }
            get
            {
                if (!string.IsNullOrEmpty(UModel.Template)) return UModel.Template;
                string tmp = typeof(UModel).Name;
                UModel.Template = Utility.GetTemplate(tmp);
                return UModel.Template;
            }
        }


        public new XElement SerializeXML(string Name)
        {
            throw new NotImplementedException();
            //foreach всех векторов и т.д.
            XElement vectors_node = new XElement("vectors");            
            XElement points_node = new XElement("points");
            XElement nodes_node = new XElement("nodes");
            XElement surfaces_node = new XElement("surfaces");
            XElement vertexes_node = new XElement("vertexes");
            //throw new NotImplementedException();
            return new XElement(base.name,
                new XAttribute("class", "UModel"),
                vectors_node,
                points_node,
                nodes_node,
                surfaces_node,
                vertexes_node,
                base.SerializeXML(Name)
            );
        }

        public new void ResetTemplate()
        {
            Template = "";
        }

        public new void Deserialize(XElement element)
        {
            if (element.Attribute("class").Value != "UModel")
                throw new Exception("Wrong class.");
            base.Deserialize(Utility.GetElement(element, element.Name.ToString()));
            XElement vectors_node = Utility.GetElement(element, "vectors");            
            if (vectors.Count > 0) vectors.Clear();
            if (vectors_node != null)
            {
                var AllElemnts = vectors_node.Elements().ToList();
                foreach (XElement child in AllElemnts)
                {
                    if (!child.HasAttributes) continue;
                    UVector Vec = new UVector();
                    Vec.Deserialize(child);
                    vectors.Add(Vec);
                }
            }

            XElement points_node = Utility.GetElement(element, "points");
            if (points.Count > 0) points.Clear();
            if (points_node != null)
            {
                var AllElemnts = points_node.Elements().ToList();
                foreach (XElement child in AllElemnts)
                {
                    if (!child.HasAttributes) continue;
                    UVector Vec = new UVector();
                    Vec.Deserialize(child);
                    points.Add(Vec);
                }
            }

            XElement nodes_node = Utility.GetElement(element, "nodes");
            if (nodes.Count > 0) nodes.Clear();
            if (nodes_node != null)
            {
                var AllElemnts = nodes_node.Elements().ToList();
                foreach (XElement child in AllElemnts)
                {
                    if (!child.HasAttributes) continue;
                    BSPNode Node = new BSPNode();
                    Node.Deserialize(child);
                    nodes.Add(Node);
                }
            }

            XElement surfaces_node = Utility.GetElement(element, "surfaces");
            if (surfaces.Count > 0) surfaces.Clear();
            if (surfaces_node != null)
            {
                var AllElemnts = surfaces_node.Elements().ToList();
                foreach (XElement child in AllElemnts)
                {
                    if (!child.HasAttributes) continue;
                    BSPSurface Surf = new BSPSurface();
                    Surf.Deserialize(child);
                    surfaces.Add(Surf);
                }
            }

            XElement vertexes_node = Utility.GetElement(element, "vertexes");
            if (vertexes.Count > 0) vertexes.Clear();
            if (vertexes_node != null)
            {
                var AllElemnts = vertexes_node.Elements().ToList();
                foreach (XElement child in AllElemnts)
                {
                    if (!child.HasAttributes) continue;
                    UVertex Vec = new UVertex();
                    Vec.Deserialize(child);
                    vertexes.Add(Vec);
                }
            }
        }
    }
}
