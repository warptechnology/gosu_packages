using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class UModel : UPrimitive
    {
        public UArray<UVector> vectors { set; get; }
        public UArray<UVector> points { set; get; }
        public UArray<BSPNode> nodes { set; get; }
        public UArray<BSPSurface> surfaces { set; get; }
        public UArray<UVertex> vertexes { set; get; }

        
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

        
        public UPrimitive UPrimitive
        {
            get
            {
                UPrimitive obj = new UPrimitive();
                obj.bounding_box = this.bounding_box;
                obj.bounding_sphere = this.bounding_sphere;
                obj.Flags = this.Flags;
                obj.Name = this.Name;
                return obj;
            }
        }        
    }
}
