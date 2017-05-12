namespace L2Package
{
    public class FPoly
    {
        private const int MAX_VERTICES = 16;
        public FVector Base;                   // Base point of polygon.
        public FVector Normal;                 // Normal of polygon.
        public FVector TextureU;               // Texture U vector.
        public FVector TextureV;               // Texture V vector.
        public FVector[] Vertex;               // Actual vertices.
        public int PolyFlags;                  // FPoly & Bsp poly bit flags (PF_).
        public Index Actor;                    // Brush where this originated, or NULL.
        public Index Texture;                  // Texture map.
        public FName ItemName;                 // Item name.
        public int NumVertices;                // Number of vertices.
        public int iLink;                      // iBspSurf, or brush fpoly index of first identical polygon, or MAXWORD.
        public int iBrushPoly;                 // Index of editor solid's polygon this originated from.
        public short PanU;                     // Texture panning values U.
        public short PanV;                     // Texture panning values V.
        public int SavePolyIndex;              // Used by multiple vertex editing to keep track of original PolyIndex into owner brush
        public bool bFaceDragSel;              //

        public FPoly()
        {
            Vertex = new FVector[MAX_VERTICES];
        }
    }
}