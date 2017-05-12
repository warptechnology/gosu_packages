using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Package
{
    public class Header : IHeader
    {
        public Header() { }
        public Header(byte[] PackageBytes)
        {

            int GlobalOffset = 28;
            int TagOffset = 0;
            int FileVersionOffset = 4;
            int LicenseeModeOffset = 6;
            int PackageFlagsOffset = 8;
            int NameCountOffset = 12;
            int NameOffsetOffset = 16;
            int ExportCountOffset = 20;
            int ExportOffsetOffset = 24;
            int ImportCountOffset = 28;
            int ImportOffsetOffset = 32;
            int GuidOffset = 36;
            int GenerationCountOffset = 52;
            int GenerationsOffset = 56;

            Signature = BitConverter.ToInt32(PackageBytes, TagOffset + GlobalOffset);
            PackageVersion = BitConverter.ToInt16(PackageBytes, FileVersionOffset + GlobalOffset);
            LicenseMode = BitConverter.ToInt16(PackageBytes, LicenseeModeOffset + GlobalOffset);
            PackageFlags = BitConverter.ToInt32(PackageBytes, PackageFlagsOffset + GlobalOffset);
            NameCount = BitConverter.ToInt32(PackageBytes, NameCountOffset + GlobalOffset);
            NameOffset = BitConverter.ToInt32(PackageBytes, NameOffsetOffset + GlobalOffset);
            ExportCount = BitConverter.ToInt32(PackageBytes, ExportCountOffset + GlobalOffset);
            ExportOffset = BitConverter.ToInt32(PackageBytes, ExportOffsetOffset + GlobalOffset);
            ImportCount = BitConverter.ToInt32(PackageBytes, ImportCountOffset + GlobalOffset);
            ImportOffset = BitConverter.ToInt32(PackageBytes, ImportOffsetOffset + GlobalOffset);


            Guid = new GUID()
            {
                A = BitConverter.ToInt32(PackageBytes, GuidOffset + GlobalOffset + 0),
                B = BitConverter.ToInt32(PackageBytes, GuidOffset + GlobalOffset + 4),
                C = BitConverter.ToInt32(PackageBytes, GuidOffset + GlobalOffset + 8),
                D = BitConverter.ToInt32(PackageBytes, GuidOffset + GlobalOffset + 12)
            };

            GenerationCount = BitConverter.ToInt32(PackageBytes, GenerationCountOffset + GlobalOffset);
            Generations = new List<GenerationInfo>();
            for (int i = 0; i < GenerationCount; i++)
            {
                int LocalOffset = 8 * i;
                GenerationInfo GI;
                GI.export_count = BitConverter.ToInt32(PackageBytes, GenerationsOffset + GlobalOffset + LocalOffset + 0);
                GI.name_count = BitConverter.ToInt32(PackageBytes, GenerationsOffset + GlobalOffset + LocalOffset + 4);
                Generations.Add(GI);
            }
        }

        private int signature;
        private short packageVersion;
        private short licenseMode;
        private int packageFlags;
        private int nameCount;
        private int nameOffset;
        private int exportCount;
        private int exportOffset;
        private int importCount;
        private int importOffset;
        private GUID guid;
        private int generationCount;
        private List<GenerationInfo> generations;

        public int Signature
        {
            get
            {
                return signature;
            }

            private set
            {
                signature = value;
            }
        }
        public short PackageVersion
        {
            get
            {
                return packageVersion;
            }

            private set
            {
                packageVersion = value;
            }
        }
        public short LicenseMode
        {
            get
            {
                return licenseMode;
            }

            private set
            {
                licenseMode = value;
            }
        }
        public int PackageFlags
        {
            get
            {
                return packageFlags;
            }

            private set
            {
                packageFlags = value;
            }
        }
        public int NameCount
        {
            get
            {
                return nameCount;
            }

            private set
            {
                nameCount = value;
            }
        }
        public int NameOffset
        {
            get
            {
                return nameOffset;
            }

            private set
            {
                nameOffset = value;
            }
        }
        public int ExportCount
        {
            get
            {
                return exportCount;
            }

            private set
            {
                exportCount = value;
            }
        }
        public int ExportOffset
        {
            get
            {
                return exportOffset;
            }

            private set
            {
                exportOffset = value;
            }
        }
        public int ImportCount
        {
            get
            {
                return importCount;
            }

            private set
            {
                importCount = value;
            }
        }
        public int ImportOffset
        {
            get
            {
                return importOffset;
            }

            private set
            {
                importOffset = value;
            }
        }
        public GUID Guid
        {
            get
            {
                return guid;
            }

            private set
            {
                guid = value;
            }
        }
        public int GenerationCount
        {
            get
            {
                return generationCount;
            }

            private set
            {
                generationCount = value;
            }
        }
        public List<GenerationInfo> Generations
        {
            get
            {
                return generations;
            }

            private set
            {
                generations = value;
            }
        }
        public struct GUID
        {
            public int A, B, C, D;
        };
        public struct GenerationInfo
        {
            public int export_count;
            public int name_count;
        };





    }
}
