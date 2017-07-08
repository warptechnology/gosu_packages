using L2Package.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Package.Body
{
    /// <summary>
    /// Property of an object.
    /// </summary>
    public class Property
    {
        public delegate string NameResolver(Index Ind);
        private static NameResolver nameResolver;


        public static NameResolver Resolve
        {
            get { return nameResolver; }
            set { nameResolver = value; }
        }


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
        public Index StructNameRef { get; set; }

        /// <summary>
        /// If PropertyType is PropertyType.ArrayProperty, Then it has size;
        /// </summary>
        public int ArraySize { set; get; }
        
        /// <summary>
        /// Deserializes a Property from bytes of package
        /// </summary>        
        /// <param name="cache">Decrypted bytes of a package. Use PackageReader to read and decrypt it.</param>
        /// <param name="Offset">Offset in bytes within a package file</param>
        public Property(byte[] Cache, int Offset)
        {
            /*var qwe = new byte[25];
            Array.Copy(Cache, Offset, qwe, 0, 25);*/
            if (Resolve == null) throw new NullReferenceException("Set NameResolver first");
            int Position = Offset;
            NameTableRef = new Index(Cache, Position);
            if (Resolve(NameTableRef) == "None")
            {
                Type = PropertyType.None;
                Size = 1;
                return;
            }
            Position += NameTableRef.Size;
            InfoByte ib = new InfoByte() { Value = Cache[Position] };
            Position++;
            Type = ib.Type;
            if (ib.Type == PropertyType.StructProperty)
            {
                StructNameRef = new Index(Cache, Position);
                Position += StructNameRef.Size;
            }
            int ValueSize = 0;
            if (ib.Size > 0)
            {
                ValueSize = ib.Size;
            }
            else
            {
                if (ib.ByteSizeFollows)
                {
                    ValueSize = (int)Cache[Position];
                    Position++;
                }
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
                Value = BitConverter.ToInt32(Cache, Position);
                Position += 4;
            }
            if (ib.Type == PropertyType.BooleanProperty)
            {
                Value = ib[7];
            }
            if (ib.Type == PropertyType.StrProperty)
            {
                byte Length = Cache[Position];
                Position++;
                unsafe
                {
                    fixed (byte* pAscii = Cache)
                    {
                        Value = new string((sbyte*)pAscii, Position, Length - 1);
                    }
                }
                Position += Length;
            }
            if (ib.Type == PropertyType.FloatProperty)
            {
                Value = BitConverter.ToSingle(Cache, Position);
                Position += 4;
            }
            if (ib.Type == PropertyType.ObjectProperty)
            {
                Value = new Index(Cache, Position);
                Position += (Value as Index).Size;
            }
            if (ib.Type == PropertyType.VectorProperty)
            {
                Value = new UVector()
                {
                    X = BitConverter.ToSingle(Cache, Position),
                    Y = BitConverter.ToSingle(Cache, Position + 4),
                    Z = BitConverter.ToSingle(Cache, Position + 8)
                };
                Position += 12;
            }
            if (ib.Type == PropertyType.StructProperty)
            {
                byte[] Arr = new byte[ValueSize];
                Array.Copy(Cache, Position, Arr, 0, Arr.Length);
                Value = Arr;
                Position += Arr.Length;
            }
            if (ib.Type == PropertyType.ArrayProperty)
            {
                byte[] Arr = new byte[ValueSize];
                Array.Copy(Cache, Position, Arr, 0, Arr.Length);
                ArraySize = Arr[0]; 
                List<Property> Val = new List<Property>();
                int LocalOffset = 1; //because 1 byte for arr. sz
                
                while (LocalOffset < Arr.Length)
                {
                    Val.Add(new Property(Arr, LocalOffset));
                    LocalOffset += Val.Last().Size;
                }
                Value = Val;
                Position += Arr.Length;
            }
            if(ib.Type == PropertyType.NameProperty)
            {
                Index Ref = new Index(Cache, Position);
                Value = Resolve(Ref);
                Position += Ref.Size;
            }
            if (ib.Type == PropertyType.ClassProperty)
                throw new NotSupportedException("Never found in packages => never implemented");
            if(ib.Type == PropertyType.FixedArrayProperty)
                throw new NotSupportedException("Never found in packages => never implemented");
            if(ib.Type == PropertyType.MapProperty)
                throw new NotSupportedException("Never found in packages => never implemented");


            this.Size = Position - Offset;
            ;
        }

        public void SetStructType(StructType SType)
        {
            //valid only for structs with value set to byte[]
            if (!(Value is byte[]) || Type != PropertyType.StructProperty)
                throw new InvalidOperationException();
            byte[] Val = Value as byte[];
            switch (SType)
            {
                case StructType.Color:
                    ReadColor(Val);
                    break;
                case StructType.Vector:
                    ReadVector(Val);
                    break;
                case StructType.Rotator:
                    ReadRotator(Val);
                    break;
                case StructType.Scale:
                    ReadScale(Val);
                    break;
                case StructType.PointRegion:
                    ReadPointRegion(Val);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private void ReadColor(byte[] Val)
        {
            //RGBA bytes (4)
            if (Val.Length != 4) throw new InvalidOperationException();
            Value = new UColor
            {
                r = Val[0],
                g = Val[1],
                b = Val[2],
                a = Val[3]
            };
        }
        private void ReadVector(byte[] Val)
        {
            //XYZ 3 floats values
            if (Val.Length != 12) throw new InvalidOperationException();
            UVector vec = new UVector()
            {
                X = BitConverter.ToSingle(Val, 0),
                Y = BitConverter.ToSingle(Val, 4),
                Z = BitConverter.ToSingle(Val, 8)
            };
            Value = vec;
        }
        private void ReadRotator(byte[] Val)
        {
            //XYZ 3 int values
            if (Val.Length != 12) throw new InvalidOperationException();
            URotator rot = new URotator()
            {
                Pitch = BitConverter.ToInt32(Val, 0),
                Yaw = BitConverter.ToInt32(Val, 4),
                Roll = BitConverter.ToInt32(Val, 8)
            };
            Value = rot;
        }
        private void ReadPointRegion(byte[] Val)
        {
            var PointRegionProps = ReadProperties(Val, 0);
            UPointRegion upr = new UPointRegion();
            foreach (var item in PointRegionProps)
            {
                switch (Resolve(item.NameTableRef))
                {
                    case "Zone":
                        upr.Zone = item.Value as Index;
                        break;
                    case "iLeaf":
                        upr.iLeaf = (int)item.Value;
                        break;
                    case "ZoneNumber":
                        upr.ZoneNumber = (byte)item.Value;
                        break;
                    default:
                        break;
                }
            }
            Value = upr;
        }
        private void ReadScale(byte[] Val)
        {
            UScale us = new UScale();
            var ScaleProps = ReadProperties(Val, 0);
            foreach (var item in ScaleProps)
            {
                switch (Resolve(item.NameTableRef))
                {
                    case "SheerAxis":
                        us.sheeraxis = (byte)item.Value;
                        break;
                    case "SheerRate":
                        us.sheerrate = (float)item.Value;
                        break;
                    case "Scale":
                        item.SetStructType(StructType.Vector);
                        UVector Vec = item.Value as UVector;
                        us.x = Vec.X;
                        us.y = Vec.Y;
                        us.z = Vec.Z;
                        break;
                    default:
                        break;
                }
            }
            Value = us;
        }
        public static List<Property> ReadProperties(byte[] Bytes, int pos)
        {
            List<Property> Props = new List<Property>();
            Property prop = null;
            int offset = 0;
            do
            {
                prop = new Property(Bytes, pos + offset);
                Props.Add(prop);
                string qwe = Resolve(prop.NameTableRef);
                offset += prop.Size;
                
            } while (prop.Type != PropertyType.None);
            return Props;
        }
    }
    public enum StructType
    {
        Color, Vector, Rotator, Scale, PointRegion
    }
}
