using System;
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
        public string StructNameIfStruct { get; set; }

        /// <summary>
        /// Deserializes a Property from bytes of package
        /// </summary>        
        /// <param name="cache">Decrypted bytes of a package. Use PackageReader to read and decrypt it.</param>
        /// <param name="Offset">Offset in bytes within a package file</param>
        public Property(byte[] Cache, int Offset)
        {
            int Position = Offset;
            NameTableRef = new Index(Cache, Position);
            if (NameTableRef == 0) //none
            {
                Type = PropertyType.None;
                return;
            }
            Position += NameTableRef.Size;
            InfoByte ib = new InfoByte() { Value = Cache[Position] };
            Position++;
            Type = ib.Type;

            int ValueSize = 0;
            if (ib.Size > 0)
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
            if (ib.Type == PropertyType.ByteProperty)
            {
                Value = Cache[Position];
                Position++;
            }
            if (ib.Type == PropertyType.IntegerProperty)
            {

            }
            if (ib.Type == PropertyType.BooleanProperty)
            {
                Value = ib[7];
            }

            if (ib.Type == PropertyType.StructProperty) { }
            if (ib.IsArray) { }


            this.Size = Position - Offset;
        }

    }
}
