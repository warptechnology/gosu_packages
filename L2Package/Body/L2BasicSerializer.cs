using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L2Package.DataStructures;
using System.Collections;
using System.IO;
using System.Reflection;

namespace L2Package.Body
{

    /// <summary>
    /// Serializer for Lineage 2 packages
    /// </summary>
    internal class L2BasicSerializer : IUnrealSerializer
    {
        IHeader Header { set; get; }
        INameTable NameTable { set; get; }
        IExportTable ExportTable { set; get; }
        IImportTable ImportTable { set; get; }
        byte[] Bytes { set; get; }

        /// <summary>
        /// Initializing func for Serializer. 
        /// </summary>
        /// <param name="H">Header of the package</param>
        /// <param name="NT">NameTable of package.</param>
        /// <param name="ET">ExportTable of package.</param>
        /// <param name="IT">ImportTable of Package</param>
        /// <param name="body">Readed and decrypted bytes of package body. </param>
        /// <seealso cref="L2Package.PackageReader"/>
        public void Initialize(IHeader H, INameTable NT, IExportTable ET, IImportTable IT, byte[] body)
        {
            Header = H;
            NameTable = NT;
            ExportTable = ET;
            ImportTable = IT;
            Bytes = body;
            Initialized = true;
        }
        public bool Initialized { get; private set; }

        public UObject Deserialize(Export Exp)
        {
            int HeaderSize = 0;
            string Class = NameTable[ImportTable[(Exp.Class + 1) * -1].ObjectName];
            if (Class == "StaticMeshActor")
                HeaderSize = 0xF;
            List<Property> Properties = Property.ReadProperties(Bytes, Exp.SerialOffset + 28 + HeaderSize);
            UObject obj = null;
            if (Factories.ContainsKey(Class))
                obj = Factories[Class](Properties);
            else
                obj = new UObject();

            obj.Flags = Exp.ObjectFlags;
            obj.Group = NameTable[Exp.Group];
            obj.Name = NameTable[Exp.NameTableRef];

            return obj;
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }

        public L2BasicSerializer()
        {
            Factories = new Dictionary<string, UObjectFactory>();
            Factories.Add("StaticMeshActor", StaticMeshActorFactory);
        }

        private delegate UObject UObjectFactory(List<Property> Properties);

        private Dictionary<string, UObjectFactory> Factories;

        object GetValueByPropertyName(string Name, List<Property> Props, StructType? t = null)
        {
            int NameRef = NameTable.IndexOf(Name);

            foreach (Property item in Props)
            {
                if (item.NameTableRef.Value == NameRef)
                {
                    if (t != null) item.SetStructType((StructType)t);
                    return item.Value;
                }
            }
            return null;
        }

        private UObject AActorFactory(List<Property> Properties)
        {
            AActor Actor = new AActor();
            object bBlockActors = GetValueByPropertyName("bBlockActors", Properties);
            Actor.bBlockActors = bBlockActors == null ? false : (bool)bBlockActors;

            object bBlockPlayers = GetValueByPropertyName("bBlockPlayers", Properties);
            Actor.bBlockPlayers = bBlockPlayers == null ? false : (bool)bBlockPlayers;

            object bCollideActors = GetValueByPropertyName("bCollideActors", Properties);
            Actor.bCollideActors = bCollideActors == null ? false : (bool)bCollideActors;

            object bDeleteMe = GetValueByPropertyName("bDeleteMe", Properties);
            Actor.bDeleteMe = bDeleteMe == null ? false : (bool)bDeleteMe;

            object bHidden = GetValueByPropertyName("bHidden", Properties);
            Actor.bHidden = bHidden == null ? false : (bool)bHidden;

            object draw_scale = GetValueByPropertyName("DrawScale", Properties);
            Actor.DrawScale = draw_scale == null ? 1.0f : (float)draw_scale;

            object location = GetValueByPropertyName("Location", Properties, StructType.Vector);
            //if (location == null)
            //    Actor.location = new UVector { X = 1.0f, Y = 1.0f, Z = 1.0f };
            //else
            //Actor.location = (UVector)location;
            Actor.Location = location == null ? new UVector { X = 1.0f, Y = 1.0f, Z = 1.0f } :
                                             (UVector)location;

            object rotation = GetValueByPropertyName("Rotation", Properties, StructType.Rotator);
            Actor.Rotation = rotation == null ? new URotator { Pitch = 0, Roll = 0, Yaw = 0 } :
                                             (URotator)rotation;

            object draw_scale_3d = GetValueByPropertyName("DrawScale3D", Properties, StructType.Vector);
            Actor.DrawScale3D = draw_scale_3d == null ? new UVector { X = 1.0f, Y = 1.0f, Z = 1.0f } :
                                             (UVector)draw_scale_3d;

            object pre_pivot = GetValueByPropertyName("PrePivot", Properties, StructType.Vector);
            Actor.PrePivot = pre_pivot == null ? new UVector { X = 0.0f, Y = 0.0f, Z = 0.0f } :
                                             (UVector)pre_pivot;

            object static_mesh = GetValueByPropertyName("StaticMesh", Properties);
            Actor.StaticMesh = static_mesh == null ? 0 : ((Index)static_mesh).Value;
            return Actor;
        }

        private UObject StaticMeshActorFactory(List<Property> Properties)
        {
            var Aactor = AActorFactory(Properties);
            StaticMeshActor Actor = new StaticMeshActor(Aactor as AActor);

            object bExactProjectileCollision = GetValueByPropertyName("bExactProjectileCollision", Properties);
            Actor.bExactProjectileCollision = bExactProjectileCollision == null ? false : (bool)bExactProjectileCollision;

            object bWorldGeometry = GetValueByPropertyName("bWorldGeometry", Properties);
            Actor.bWorldGeometry = bWorldGeometry == null ? false : (bool)bWorldGeometry;

            object bShadowCast = GetValueByPropertyName("bShadowCast", Properties);
            Actor.bShadowCast = bShadowCast == null ? false : (bool)bShadowCast;

            object bStaticLighting = GetValueByPropertyName("bStaticLighting", Properties);
            Actor.bStaticLighting = bStaticLighting == null ? false : (bool)bStaticLighting;

            object CollisionRadius = GetValueByPropertyName("bCollideActors", Properties);
            Actor.CollisionRadius = CollisionRadius == null ? 0.0f : (float)CollisionRadius;

            object CollisionHeight = GetValueByPropertyName("bCollideActors", Properties);
            Actor.CollisionHeight = CollisionHeight == null ? 0.0f : (float)CollisionHeight;

            object bBlockKarma = GetValueByPropertyName("bBlockKarma", Properties);
            Actor.bBlockKarma = bBlockKarma == null ? false : (bool)bBlockKarma;

            object bEdShouldSnap = GetValueByPropertyName("bEdShouldSnap", Properties);
            Actor.bEdShouldSnap = bEdShouldSnap == null ? false : (bool)bEdShouldSnap;

            return Actor;
        }

        private void SetClassProperties(List<Property> Properties, UObject Actor)
        {
            // Получаем все свойства десериализуемого объекта, тип которых является базовым. (float, byte...)
            // Ищем среди прочитаных свойств из файла свойства с такими же именами
            // и по именам записываем свойства в объект
            
        }
    }


    internal enum PropertyTypes
    {
        otNone = 0,
        otByte = 1,
        otInt = 2,
        otBool = 3,
        otFloat = 4,
        otObject = 5,
        otName = 6,
        otString = 7,
        otClass = 8,
        otArray = 9,
        otStruct = 10,
        otVector = 11,
        otRotator = 12,
        otStr = 13,
        otMap = 14,
        otFixedArray = 15,
        otExtendedValue = 0x00000100,
        otBuffer = otExtendedValue | 0,
        otWord = otExtendedValue | 1,
    }
}
