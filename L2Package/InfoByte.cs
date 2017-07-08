using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L2Package.Body;

namespace L2Package
{
    /// <summary>
    /// Contains information about property
    /// Bits 0 to 3 is the type
    /// bits 4 to 6 is the size
    /// and bit 7 is the array flag
    /// </summary>
    class InfoByte
    {
        private byte value;
        private int[] SizeValues = new int[]
        {
            1 , // byte
            2 , // bytes
            4 , // bytes
            12, // bytes
            16, // bytes
            -1, //   a byte follows with real size
            -2, //   a word follows with real size
            -3  //   an integer follows with real size
        };
        /// <summary>
        /// Serialized to a single byte value
        /// </summary>
        public byte Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Returns atype of property (such as Byte or Vector)
        /// </summary>
        public PropertyType Type
        {
            get
            {
                int val = 0;
                val = Value & 0x0F; //00 00 11 11
                return (PropertyType)val;
            }
        }

        /// <summary>
        /// Size of a folowing property
        /// </summary>
        public int Size
        {
            get
            {
                int val = 0;
                val = (Value & 0x70) >> 4; // 0 111 0000

                return val < 5 ? SizeValues[val]:0;
            }
        }
        /// <summary>
        /// for properties bigger then 16 bytes Next byte will contain size of property.
        /// </summary>
        public bool ByteSizeFollows
        {
            get
            {
                return (Value & 0x70) >> 4 == 5;
                
            }
        }
        /// <summary>
        /// Returns the value of the specified bit
        /// </summary>
        /// <param name="i">Bit index</param>
        /// <returns>Bit value as boolean</returns>
        public bool this[int i]
        {
            get
            {
                if (i < 0 || i > 7)
                    throw new IndexOutOfRangeException("Bits in byte can only be indexed 0-7!");
                return (Value & (0x01 << i)) > 0;
            }
        }

        /// <summary>
        /// for properties bigger then 16 bytes Next 2 bytes will contain WORD size of property.
        /// </summary>
        public bool WordSizeFollows
        {
            get
            {
                return (Value & 0x70) >> 4 == 6;
            }
        }

        /// <summary>
        /// for properties bigger then 16 bytes Next 4 byte will contain DWORD size of property.
        /// </summary>
        public bool DwordSizeFollows
        {
            get
            {
                return (Value & 0x70) >> 4 == 7;
            }
        }
        /// <summary>
        /// If is true byte property folows this infobyte.
        /// </summary>
        public bool IsArray
        {
            get
            {
                return (Value & 0x80) > 0 && PropertyType.BooleanProperty != Type;
            }
        }

    }
}
