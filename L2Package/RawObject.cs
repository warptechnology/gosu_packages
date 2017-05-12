using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Package
{
    public class Property
    {
        public Index NameTableRef { set; get; }
        public PropertyType Type { set; get; }
        public object Value { set; get; }
        public int Size { get; internal set; }
        public string StructNameIfAStruct { get; set; }

        public Property(byte[] Cache, int Offset)
        {
            int Position = Offset;
            NameTableRef = new Index(Cache, Position);
            Position += NameTableRef.Size;            
            InfoByte ib = new InfoByte() { Value = Cache[Position] };
            Position++;
            Type = ib.Type;
            
            int ValueSize = 0;
            if(ib.Size > 0)
            {
                ValueSize = ib.Size;
            }
            else
            {
                if (ib.ByteSizeFollows) { ValueSize = (int)Cache[Position]; Position++; }
                if (ib.WordSizeFollows)
                {
                    ValueSize = (int)BitConverter.ToUInt16(Cache, Position);
                    Position += 2;
                }
                if (ib.DwordSizeFollows)
                {
                    ValueSize = (int)BitConverter.ToUInt32(Cache, Position);
                    Position += 4;
                }
            }
            if (ib.Type == PropertyType.StructProperty) throw new NotImplementedException();
            if (ib.IsArray) throw new NotImplementedException();
             
        }

    }
    public class RawObject : ICollection, IEnumerable<Property>
    {
        private List<Property> Properties { set; get; }
        public string Name { set; get; }
        public int Flags;

        public int Count
        {
            get
            {
                return Properties.Count();
            }
        }

        public object SyncRoot
        {
            get
            {
                return this;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public byte[] Header { get; internal set; }

        public void CopyTo(Array array, int index)
        {
            foreach (Property item in Properties)
                array.SetValue(item, index++);
        }

        public IEnumerator GetEnumerator()
        {
            return new PropertiesEnumerator<Property>(Properties);
        }

        IEnumerator<Property> IEnumerable<Property>.GetEnumerator()
        {
            return new PropertiesEnumerator<Property>(Properties);
        }
        public void Add(Property prop)
        {
            Properties.Add(prop);
        }
        public void AddRange(IEnumerable<Property> props)
        {
            Properties.AddRange(props);
        }
    }
    public enum PropertyType
    {
        ByteProperty = 0x01, // BYTE
        IntegerProperty = 0x02, // DWORD
        BooleanProperty = 0x03, // The real value is in bit 7 of the info byte.
        FloatProperty = 0x04, // DWORD A 4-byte float.
        ObjectProperty = 0x05, // INDEX Object Reference value. See “Object References”.
        NameProperty = 0x06, // INDEX Name Reference value. Index in to the Name Table.
        StringProperty = 0x07, // Unknown
        ClassProperty = 0x08, // See below for some known classes.
        ArrayProperty = 0x09, // Unknown
        StructProperty = 0x0A, // See below for some known structs.
        VectorProperty = 0x0B, // Unknown
        RotatorProperty = 0x0C, // Unknown
        StrProperty = 0x0D, // INDEX length ASCIIZ text Length field includes null terminator.
        MapProperty = 0x0E, // Unknown
        FixedArrayProperty = 0x0F // Unknown
    }
}
