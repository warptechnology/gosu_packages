using System;
using System.Globalization;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class BSPNode : IXmlSerializable, IUnrealExportable
    {
        [UEExport]
        public Plane plane { set; get; }
        [UEExport]
        public ulong zone_mask { set; get; }
        [UEExport]
        public byte node_flags { set; get; }
        [UEExport]
        public int vert_pool { set; get; }
        [UEExport]
        public int surface { set; get; }
        [UEExport]
        public int back { set; get; }
        [UEExport]
        public int front { set; get; }
        [UEExport]
        public int i_plane { set; get; }
        [UEExport]
        public int collision_bound { set; get; }
        [UEExport]
        public int render_bound { set; get; }
        [UEExport]
        public UVector unknown_point { set; get; }
        [UEExport]
        public uint unknown_id { set; get; }
        [UEExport]
        public ulong conn_zones { set; get; }
        [UEExport]
        public ulong vis_zones { set; get; }
        [UEExport]
        public int[] zone { set; get; }
        [UEExport]
        public byte num_verticies { set; get; }
        [UEExport]
        public int[] leaf { set; get; }

        public BSPNode()
        {
            plane = new Plane();
            unknown_point = new UVector();
            zone = new int[2];
            leaf = new int[2];
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
                BSPNode.Template = value;
                Utility.SetTemplate(typeof(BSPNode).Name, BSPNode.Template);
            }
            get
            {
                if (!string.IsNullOrEmpty(BSPNode.Template)) return BSPNode.Template;
                string tmp = typeof(BSPNode).Name;
                BSPNode.Template = Utility.GetTemplate(tmp);
                return BSPNode.Template;
            }
        }

        public XElement SerializeXML(string Name)
        {
            return new XElement(Name,
                plane.SerializeXML("location"),
                new XElement("zone_mask_index", zone_mask.ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("node_flags_index", node_flags.ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("vert_pool_index", vert_pool.ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("surface_index", surface.ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("back_index", back.ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("front_index", front.ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("i_plane_index", i_plane.ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("collision_bound_index", collision_bound.ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("render_bound_index", render_bound.ToString(NumberFormatInfo.InvariantInfo)),
                unknown_point.SerializeXML("unknown_point"),
                new XElement("unknown_id", unknown_id.ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("conn_zones", conn_zones.ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("vis_zones", vis_zones.ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("zone_0", zone[0].ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("zone_1", zone[1].ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("num_verticies", num_verticies.ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("leaf_0", leaf[0].ToString(NumberFormatInfo.InvariantInfo)),
                new XElement("leaf_1", leaf[1].ToString(NumberFormatInfo.InvariantInfo)),
                new XAttribute("class", "BSPNode"));
        }

        public void ResetTemplate()
        {
            Template = "";
        }

        public void Deserialize(XElement element)
        {
            if (element.Attribute("class").Value != "BSPNode")
                throw new Exception("Wrong class.");

            plane.Deserialize(Utility.GetElement(element, "location"));
            zone_mask = Utility.Get<ulong>("zone_mask_index", element);
            node_flags = Utility.Get<byte>("node_flags_index", element);
            vert_pool = Utility.Get<int>("vert_pool_index", element);
            surface = Utility.Get<int>("surface_index", element);
            back = Utility.Get<int>("back_index", element);
            front = Utility.Get<int>("front_index", element);
            i_plane = Utility.Get<int>("i_plane_index", element);
            collision_bound = Utility.Get<int>("collision_bound_index", element);
            render_bound = Utility.Get<int>("render_bound_index", element);
            unknown_point.Deserialize(Utility.GetElement(element, "unknown_point"));
            unknown_id = Utility.Get<uint>("unknown_id", element);
            conn_zones = Utility.Get<ulong>("conn_zones", element);
            vis_zones = Utility.Get<ulong>("vis_zones", element);
            zone[0] = Utility.Get<int>("zone_0", element);
            zone[1] = Utility.Get<int>("zone_1", element);
            num_verticies = Utility.Get<byte>("num_verticies", element);
            leaf[0] = Utility.Get<int>("leaf_0", element);
            leaf[1] = Utility.Get<int>("leaf_1", element);
        }
    }
}