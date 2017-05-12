using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Package
{
    public class PackageReader : IPackageReader
    {
        public string Path { private set; get; }
        public byte[] Bytes
        {
            get
            {
                return bytes;
            }

            set
            {
                bytes = value;
            }
        }
        public void Read(string FileName)
        {
            ReadFrom(FileName);
        }

        private void ReadFrom(string FileName)
        {
            Path = FileName;
            if (!File.Exists(Path))
                throw new FileNotFoundException();
            byte[] OriginalBytes = File.ReadAllBytes(Path);

            byte[] target_header = {
                    0x4C, 0x00, 0x69, 0x00, 0x6E, 0x00, 0x65, 0x00,
                    0x61, 0x00, 0x67, 0x00, 0x65, 0x00, 0x32, 0x00,
                    0x56, 0x00, 0x65, 0x00, 0x72, 0x00
                }; // Lineage2Ver

            byte[] HeaderBuffer = new byte[22];
            byte[] VersionBuffer = new byte[6];

            Array.Copy(OriginalBytes, 0, HeaderBuffer, 0, 22);
            Array.Copy(OriginalBytes, 22, VersionBuffer, 0, 6);

            if (!target_header.SequenceEqual(HeaderBuffer))
                throw new InvalidDataException();

            byte[] Vers111 = { 49, 49, 49 };
            byte[] Vers121 = { 49, 50, 49 };
            byte[] MyVers = { VersionBuffer[0], VersionBuffer[2], VersionBuffer[4] };

            byte key = 0;
            if (Vers111.SequenceEqual(MyVers))
            {
                key = 0xAC;
            }
            else if (Vers111.SequenceEqual(MyVers))
            {
                byte val = OriginalBytes[28];
                key = (byte)(val ^ 0xC1);
            }
            else
                throw new FormatException("Unknown version");

            Bytes = new byte[OriginalBytes.Count()];
            Array.Copy(OriginalBytes, 0, Bytes, 0, 28);
            for (int i = 28; i < OriginalBytes.Count(); i++)
            {
                Bytes[i] = (byte)(OriginalBytes[i] ^ key);
            }
        }
        public PackageReader() { }
        //public PackageReader(string FileName)
        //{
        //    ReadFrom(FileName);
        //}
        public void Save(string FileName = "")
        {   
            throw new NotImplementedException();
        }
        private byte[] bytes;
    }
}
