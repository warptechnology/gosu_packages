using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Package
{
    /// <summary>
    /// Property of an object.
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Property name. (Location, rotation, size etc.) 
        /// Index referance to a NameTable
        /// </summary>
        public Index NameTableRef { set; get; }
        /// <summary>
        /// Type of a property. (Array, struct, class, vector, etc.)
        /// </summary>
        public PropertyType Type { set; get; }
        /// <summary>
        /// Deserialized value of a property
        /// </summary>
        public object Value { set; get; }
        /// <summary>
        /// Serial size of a property.
        /// </summary>
        public int Size { get; internal set; }
        /// <summary>
        /// if PropertyType is PropertyType.StructProperty, then it must have a name
        /// </summary>
        public string StructNameIfAStruct { get; set; }

        /// <summary>
        /// Deserializes a Property from bytes of package
        /// </summary>        
        /// <param name="cache">Decrypted bytes of a package. Use PackageReader to read and decrypt it.</param>
        /// <param name="Offset">Offset in bytes within a package file</param>
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
    /// <summary>
    /// A collection of properties of an object, including Name, flags, etc.
    /// </summary>
    public class RawObject : ICollection, IEnumerable<Property>
    {
        /// <summary>
        /// List of all properties of an object
        /// </summary>
        private List<Property> Properties { set; get; }
        /// <summary>
        /// Object name
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// object fkags
        /// </summary>
        public int Flags;

        /// <summary>
        /// number of properties of an object
        /// </summary>
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

        /// <summary>
        /// Header of a single property
        /// </summary>
        public byte[] Header { get; internal set; }

        /// <summary>
        /// Copies all the Properties of the current RawObject to the 
        /// specified one-dimensional array starting at the specified destination array index. 
        /// The index is specified as a 32-bit integer.
        /// </summary>
        /// <param name="array">Destination array</param>
        /// <param name="index">Zero-based starting index in destination array</param>
        public void CopyTo(Array array, int index)
        {
            foreach (Property item in Properties)
                array.SetValue(item, index++);
        }

        /// <summary>
        /// Returns an enumerator that iterates through an properties.
        /// </summary>
        /// <returns>PropertiesEnumerator<Property></returns>
        public IEnumerator GetEnumerator()
        {
            return new PropertiesEnumerator<Property>(Properties);
        }

        /// <summary>
        /// Returns an enumerator that iterates through properties.
        /// </summary>
        /// <returns>PropertiesEnumerator<Property></returns>
        IEnumerator<Property> IEnumerable<Property>.GetEnumerator()
        {
            return new PropertiesEnumerator<Property>(Properties);
        }
        /// <summary>
        /// Adds a property to an object
        /// </summary>
        /// <param name="prop"></param>
        public void Add(Property prop)
        {
            Properties.Add(prop);
        }
        /// <summary>
        /// Adds a collection of properties to an object
        /// </summary>
        /// <param name="prop"></param>
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
