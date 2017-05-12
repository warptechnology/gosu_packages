using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Package
{
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

        public PropertyType Type
        {
            get
            {
                int val = 0;
                val = Value & 0x0F; //00 00 11 11
                return (PropertyType)val;
            }
        }
        public int Size
        {
            get
            {
                int val = 0;
                val = (Value & 0x70) >> 3; // 0 11 00 00

                return val < 5 ? SizeValues[val]:0;
            }
        }
        public bool ByteSizeFollows
        {
            get
            {
                return (Value & 0x70) >> 3 == 5;
                
            }
        }
        public bool WordSizeFollows
        {
            get
            {
                return (Value & 0x70) >> 3 == 6;
            }
        }
        public bool DwordSizeFollows
        {
            get
            {
                return (Value & 0x70) >> 3 == 7;
            }
        }
        public bool IsArray
        {
            get
            {
                return (Value & 0x80) > 0;
            }
        }

    }
}
