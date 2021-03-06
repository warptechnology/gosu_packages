﻿
namespace L2Package
{
    /// <summary>
    /// Compact integer. 1-5 bytes.
    /// </summary>
    public class Index
    {
        public static implicit operator int(Index i)
        {
            return i.Value;
        }
        public static implicit operator Index(int i)
        {
            return new Index() { Value = i };
        }
        public static bool operator ==(Index R, Index L)
        {
            return R.Value == L.Value;
        }
        public static bool operator !=(Index R, Index L)
        {
            return R.Value != L.Value;
        }
        public static bool operator <(Index R, Index L)
        {
            return R.Value < L.Value;
        }
        public static bool operator >(Index R, Index L)
        {
            return R.Value > L.Value;
        }
        public static bool operator <=(Index R, Index L)
        {
            return R.Value <= L.Value;
        }
        public static bool operator >=(Index R, Index L)
        {
            return R.Value >= L.Value;
        }
        /// <summary>
        /// Int32 representation of a compact int
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// Serial size of an object
        /// </summary>
        public int Size { get; set; }

        public Index() { }

        /// <summary>
        /// Ctor. Reads bytes. Get number of bytes with Size property
        /// </summary>
        /// <param name="buff">Bytes to read</param>
        /// <param name="pos">offset in Buff</param>
        public Index(byte[] buff, int pos)
        {
            this.ReadIndex(buff, pos);
        }
        /// <summary>
        /// Reads bytes. Get number of bytes with Size property
        /// </summary>
        /// <param name="buff">Bytes to read</param>
        /// <param name="pos">offset in Buff</param>
        public void ReadIndex(byte[] buff, int pos)
        {
            byte isIndiced = 0x40; // 7th bit
            byte isNegative = 0x80; // 8th bit
            byte value = (byte)(0xFF - isIndiced - isNegative); // 3F
            byte isProceeded = 0x80; // 8th bit
            byte proceededValue = (byte)(0xFF - isProceeded); // 7F

            int index = 0;
            Size = 1;
            byte b0 = buff[pos];
            if ((b0 & isIndiced) != 0)
            {
                Size = 2;
                byte b1 = buff[pos + 1];
                if ((b1 & isProceeded) != 0)
                {
                    Size = 3;
                    byte b2 = buff[pos + 2];
                    if ((b2 & isProceeded) != 0)
                    {
                        Size = 4;
                        byte b3 = buff[pos + 3];                        
                        if ((b3 & isProceeded) != 0)
                        {
                            Size = 5;
                            byte b4 = buff[pos + 4];
                            index = b4;                            
                        }
                        index = (index << 7) + (b3 & proceededValue);
                    }
                    index = (index << 7) + (b2 & proceededValue);
                }
                index = (index << 7) + (b1 & proceededValue);
            }
            Value = (b0 & isNegative) != 0 // The value is negative or positive?.
                ? -((index << 6) + (b0 & value))
                : ((index << 6) + (b0 & value));        
        }
    }
}
