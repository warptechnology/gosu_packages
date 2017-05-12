using System;

namespace L2Package
{
    public struct ExportTableEntry
    {
        public int Class;
        public int Super;
        public int Outer;
        public int Name;
        public int ObjectFlags;
    }
    
    public class OldExportTable
    {
        
        public ExportTableEntry[] EntryTable { set; get; }

        public OldExportTable()
        {
            EntryTable = new ExportTableEntry[1];
        }

        
        int ReadIndex(byte[] buff, int pos, out int ReadSize)
        {
            const byte isIndiced = 0x40; // 7th bit
            const byte isNegative = 0x80; // 8th bit
            const byte value = 0xFF - isIndiced - isNegative; // 3F
            const byte isProceeded = 0x80; // 8th bit
            const byte proceededValue = 0xFF - isProceeded; // 7F

            
            int index = 0;
            ReadSize = 1;
            byte b0 = buff[pos];
            if ((b0 & isIndiced) != 0)
            {
                byte b1 = buff[pos + 1];
                if ((b1 & isProceeded) != 0)
                {
                    byte b2 = buff[pos + 2];
                    if ((b2 & isProceeded) != 0)
                    {
                        byte b3 = buff[pos + 3];
                        if ((b3 & isProceeded) != 0)
                        {
                            byte b4 = buff[pos + 4];
                            index = b4;
                            ReadSize = 5;
                        }
                        index = (index << 7) + (b3 & proceededValue);
                        ReadSize = 4;
                    }
                    index = (index << 7) + (b2 & proceededValue);
                    ReadSize = 3;
                }
                index = (index << 7) + (b1 & proceededValue);
                ReadSize = 2;
            }
            return (b0 & isNegative) != 0 // The value is negative or positive?.
                ? -((index << 6) + (b0 & value))
                : ((index << 6) + (b0 & value));
        }
        /*
        public OldExportTable(Header header, byte[] cache)
        {
            EntryTable = new ExportTableEntry[header.ExportCount];
            int LastOffset = 0;
            for(int i = 0; i < header.ExportCount; i++)
            {
                int ReadSize = 0;
                int ByteOffset = 0;
                EntryTable[i].Class = ReadIndex(cache, header.ExportOffset + LastOffset, out ReadSize);
                ByteOffset += ReadSize;
                EntryTable[i].Super = ReadIndex(cache, header.ExportOffset + ByteOffset + LastOffset, out ReadSize);
                ByteOffset += ReadSize;
                EntryTable[i].Outer = BitConverter.ToInt32(cache, header.ExportOffset + ByteOffset + LastOffset);
                ByteOffset += 4;
                EntryTable[i].Name = ReadIndex(cache, header.ExportOffset + ByteOffset + LastOffset, out ReadSize);
                ByteOffset += ReadSize;
                EntryTable[i].ObjectFlags = BitConverter.ToInt32(cache, header.ExportOffset + ByteOffset + LastOffset);
                ByteOffset += 4;
                LastOffset += ByteOffset;
            }
        }*/
        
    }
}