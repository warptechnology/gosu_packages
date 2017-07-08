using Microsoft.VisualStudio.TestTools.UnitTesting;
using L2Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L2Package.DataStructures;
using L2Package.Body;

namespace L2Package.Tests
{
    [TestClass()]
    public class PropertyTests
    {
        IPackageReader pf { set; get; }
        IHeader header { set; get; }
        IExportTable ExportTable { set; get; }
        IImportTable ImportTable { set; get; }
        INameTable NameTable { set; get; }
        string Resolver(Index ind)
        {
            return NameTable[ind];
        }
        [TestInitialize]
        public void Initialize()
        {
            try
            {
                //Alloc
                pf = new PackageReader();
                pf.Read("D:\\la2\\maps\\17_21.unr");
                header = new Header(pf.Bytes);
                ExportTable = new ExportTable(header, pf.Bytes);
                ImportTable = new ImportTable(header, pf.Bytes);
                NameTable = new NameTable(header, pf.Bytes);
                Property.Resolve = Resolver;
            }
            catch (Exception ex)
            {
                //Assert (no exceptions reading...)
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        public void PropertyCanReadByteValueTest()
        {
            //0x017F
            string name = "SpriteEmitter544";
            Export Exp = ExportTable.FindAll(n => n.NameTableRef == NameTable.IndexOf(name))?.First();
            int PropertyOffset = 0x017F + Exp.SerialOffset + 28;
            //Act
            Property Prop = new Property(pf.Bytes, PropertyOffset);
            //Assert
            string Str = NameTable[Prop.NameTableRef];
            Assert.AreEqual(Str, "DrawStyle");
            Assert.IsTrue(Prop.Value is byte);
            Assert.AreEqual(Prop.Size, 4);
            Assert.AreEqual(Prop.Value, (byte)0x06);
        }
        [TestMethod]
        public void PropertyCanReadIntegerValueTest()
        {
            string name = "SpriteEmitter544";
            //Export Exp = ExportTable.Find(NameTable.IndexOf(name));
            Export Exp = ExportTable.FindAll(n => n.NameTableRef == NameTable.IndexOf(name))?.First();
            int PropertyOffset = 0x77 + Exp.SerialOffset + 28;
            //Act
            Property Prop = new Property(pf.Bytes, PropertyOffset);
            //Assert
            string Str = NameTable[Prop.NameTableRef];
            Assert.AreEqual(Str, "MaxParticles");
            Assert.IsTrue(Prop.Value is int);
            Assert.AreEqual(Prop.Size, 7);
            Assert.AreEqual(Prop.Value, 0x02);
        }
        [TestMethod]
        public void PropertyCanReadBoolValueTest()
        {
            //Alloc
            string name = "StaticMeshActor0";
            Export Exp = ExportTable.FindAll(n => n.NameTableRef == NameTable.IndexOf(name))?.First();
            int PropertyOffset = 19 + Exp.SerialOffset + 28;
            //Act
            Property Prop = new Property(pf.Bytes, PropertyOffset);
            //Assert
            string Str = NameTable[Prop.NameTableRef];
            Assert.AreEqual(Str, "bDynamicActorFilterState");
            Assert.IsTrue((bool)Prop.Value);
            Assert.AreEqual(Prop.Size, 3);
        }
        [TestMethod]
        public void PropertyCanReadFloatValueTest()
        {
            string name = "SpriteEmitter544";
            Export Exp = ExportTable.FindAll(n => n.NameTableRef == NameTable.IndexOf(name))?.First();
            int PropertyOffset = 0x5A + Exp.SerialOffset + 28;
            //Act
            Property Prop = new Property(pf.Bytes, PropertyOffset);
            //Assert
            string Str = NameTable[Prop.NameTableRef];
            Assert.AreEqual(Str, "Opacity");
            Assert.IsTrue(Prop.Value is float);
            Assert.AreEqual(Prop.Size, 7);
            Assert.AreEqual(Prop.Value, 0.34f);
        }
        [TestMethod]
        public void PropertyCanReadObjectIndexValueTest()
        {
            string name = "SpriteEmitter544";
            Export Exp = ExportTable.FindAll(n => n.NameTableRef == NameTable.IndexOf(name))?.First();
            int PropertyOffset = 0x183 + Exp.SerialOffset + 28;
            //Act
            Property Prop = new Property(pf.Bytes, PropertyOffset);
            //Assert
            string Str = NameTable[Prop.NameTableRef];
            Assert.AreEqual(Str, "Texture");
            Assert.IsTrue(Prop.Value is Index);
            Assert.AreEqual(Prop.Size, 5);
            Index val = Prop.Value as Index;
            Assert.AreEqual(val.Value, -105);
        }
        [TestMethod]
        public void PropertyCanReadStringValueTest()
        {
            //No StringProperty found in Packages (only StrProperty)
            //Cannot implement.
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void PropertyCanReadClassValueTest()
        {
            //No ClassProperty found in Packages
            //Cannot implement.
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void PropertyCanReadArrayValueTest()
        {
            string name = "SpriteEmitter544";
            Export Exp = ExportTable.FindAll(n => n.NameTableRef == NameTable.IndexOf(name))?.First();
            int PropertyOffset = 0 + Exp.SerialOffset + 28;
            List<Property> Val;
            //Act
            Property Prop = new Property(pf.Bytes, PropertyOffset);
            //Assert
            string Str = NameTable[Prop.NameTableRef];
            Assert.AreEqual(Str, "ColorScale");
            Assert.IsTrue(Prop.Value is List<Property>);
            Assert.AreEqual(Prop.Size, 33);
            Assert.AreEqual(Prop.Type, PropertyType.ArrayProperty);

            //Act more
            Val = Prop.Value as List<Property>;
            //Assert more
            Assert.AreEqual(6, Val.Count());
            
        }
        [TestMethod]
        public void PropertyCanReadStructValueTest()
        {
            string name = "SpriteEmitter544";
            Export Exp = ExportTable.FindAll(n => n.NameTableRef == NameTable.IndexOf(name))?.First();
            int PropertyOffset = 0x97 + Exp.SerialOffset + 28;
            //Act
            Property Prop = new Property(pf.Bytes, PropertyOffset);
            //Assert
            string Str = NameTable[Prop.NameTableRef];
            Assert.AreEqual(Str, "SpinCCWorCW");
            Assert.IsTrue(Prop.Value is byte[]);
            Assert.AreEqual(Prop.Size, 16);
            Assert.AreEqual(Prop.Type, PropertyType.StructProperty);
        }
        [TestMethod]
        public void PropertyCanReadStructVectorValueTest()
        {
            string name = "SpriteEmitter544";
            Export Exp = ExportTable.FindAll(n => n.NameTableRef == NameTable.IndexOf(name))?.First();
            int PropertyOffset = 0x97 + Exp.SerialOffset + 28;
            //Act
            Property Prop = new Property(pf.Bytes, PropertyOffset);
            Prop.SetStructType(StructType.Vector);
            //Assert
            string Str = NameTable[Prop.NameTableRef];
            Assert.AreEqual(Str, "SpinCCWorCW");
            Assert.IsTrue(Prop.Value is DataStructures.UVector);
            Assert.AreEqual(Prop.Size, 16);
            DataStructures.UVector uv = Prop.Value as DataStructures.UVector;
            Assert.AreEqual(Prop.Type, PropertyType.StructProperty);
            Assert.AreEqual(uv.X, 1.00f);
            Assert.AreEqual(uv.Y, 0.50f);
            Assert.AreEqual(uv.Z, 0.50f);
        }

        [TestMethod]
        public void PropertyCanReadStructVectorValueTest2()
        {
            var pf1 = new PackageReader();
            pf1.Read("D:\\la2\\system\\lineageeffect.u");
            var header1 = new Header(pf1.Bytes);
            var ExportTable1 = new ExportTable(header1, pf1.Bytes);
            var ImportTable1 = new ImportTable(header1, pf1.Bytes);
            var NameTable1 = new NameTable(header1, pf1.Bytes);
            string name = "MeshEmitter0";
            Export Exp = ExportTable1[1939];
            int PropertyOffset = 0xA + Exp.SerialOffset + 28;
            //Act
            Property Prop = new Property(pf1.Bytes, PropertyOffset);
            Prop.SetStructType(StructType.Vector);
            //Assert
            string Str = NameTable1[Prop.NameTableRef];
            Assert.AreEqual(Str, "Acceleration");
            Assert.IsTrue(Prop.Value is DataStructures.UVector);
            Assert.AreEqual(Prop.Size, 15);
            DataStructures.UVector uv = Prop.Value as DataStructures.UVector;
            Assert.AreEqual(Prop.Type, PropertyType.StructProperty);
            Assert.AreEqual(uv.X, -13.00f);
            Assert.AreEqual(uv.Y, 0.0f);
            Assert.AreEqual(uv.Z, -50.0f);
        }
        [TestMethod]
        public void PropertyCanReadStructRotatorValueTest()
        {

            string name = "StaticMeshActor2";
            Export Exp = ExportTable.FindAll(n => n.NameTableRef == NameTable.IndexOf(name))?.First();
            int PropertyOffset = 0x42 + Exp.SerialOffset + 28;
            //Act
            Property Prop = new Property(pf.Bytes, PropertyOffset);
            Prop.SetStructType(StructType.Rotator);
            //Assert
            string Str = NameTable[Prop.NameTableRef];
            Assert.AreEqual(Str, "Rotation");
            Assert.IsTrue(Prop.Value is DataStructures.URotator);
            Assert.AreEqual(Prop.Size, 15);
            DataStructures.URotator ur = Prop.Value as DataStructures.URotator;
            Assert.AreEqual(ur.Pitch, 0);
            Assert.AreEqual(ur.Yaw, 2048);
            Assert.AreEqual(ur.Roll, 0);
        }

        [TestMethod]
        public void PropertyCanReadStructColorValueTest()
        {
            string name = "SpriteEmitter544";
            Export Exp = ExportTable.FindAll(n => n.NameTableRef == NameTable.IndexOf(name))?.First();
            int PropertyOffset = 0x0B + Exp.SerialOffset + 28;
            //Act
            Property Prop = new Property(pf.Bytes, PropertyOffset);
            Prop.SetStructType(StructType.Color);
            UColor Col = Prop.Value as UColor;
            //Assert
            Assert.IsTrue(Prop.Value is UColor);
            Assert.AreEqual(Prop.Size, 7);
            Assert.AreEqual(Prop.NameTableRef.Value, NameTable.IndexOf("Color"));
            Assert.AreEqual((byte)255, Col.a);
            Assert.AreEqual((byte)255, Col.b);
            Assert.AreEqual((byte)255, Col.g);
            Assert.AreEqual((byte)255, Col.r);
        }
        [TestMethod]
        public void PropertyCanReadStructScaleValueTest()
        {
            string name = "Brush192";
            Export Exp = ExportTable.FindAll(n => n.NameTableRef == NameTable.IndexOf(name))?.First();
            int PropertyOffset = 0x13 + Exp.SerialOffset + 28;
            //Act
            Property Prop = new Property(pf.Bytes, PropertyOffset);
            Prop.SetStructType(StructType.Scale);
            //Assert
            string Str = NameTable[Prop.NameTableRef];
            Assert.AreEqual(Str, "MainScale");
            Assert.IsTrue(Prop.Value is DataStructures.UScale);
            Assert.AreEqual(Prop.Size, 30);
            DataStructures.UScale us = Prop.Value as DataStructures.UScale;
            Assert.AreEqual(us.x, 1.0f);
            Assert.AreEqual(us.y, 1.0f);
            Assert.AreEqual(us.z, 1.0f);
            Assert.AreEqual(us.sheerrate, 0.0f);
            Assert.AreEqual(us.sheeraxis, 5);
        }
        [TestMethod]
        public void PropertyCanReadStructPointRegionValueTest()
        {
            string name = "StaticMeshActor1";
            Export Exp = ExportTable.FindAll(n => n.NameTableRef == NameTable.IndexOf(name))?.First();
            int PropertyOffset = 0x19 + Exp.SerialOffset + 28;
            //Act
            Property Prop = new Property(pf.Bytes, PropertyOffset);
            Prop.SetStructType(StructType.PointRegion);
            //Assert
            string Str = NameTable[Prop.NameTableRef];
            Assert.AreEqual(Str, "Region");
            Assert.IsTrue(Prop.Value is DataStructures.UPointRegion);
            Assert.AreEqual(Prop.Size, 17);
            DataStructures.UPointRegion ur = Prop.Value as DataStructures.UPointRegion;
            Assert.AreEqual(ur.iLeaf, 725);
            Assert.AreEqual(ur.ZoneNumber, (byte)1);
            Assert.AreEqual(ur.Zone.Value, 0x04);
        }
        [TestMethod]
        public void PropertyCanReadStrValueTest()
        {
            string name = "SpriteEmitter544";
            Export Exp = ExportTable.FindAll(n => n.NameTableRef == NameTable.IndexOf(name))?.First();
            int PropertyOffset = 0x7E + Exp.SerialOffset + 28;
            //Act
            Property Prop = new Property(pf.Bytes, PropertyOffset);
            //Assert
            string Str = NameTable[Prop.NameTableRef];
            Assert.AreEqual(Str, "Name");
            Assert.IsTrue(Prop.Value is string);
            Assert.AreEqual(Prop.Size, 21);
            Assert.AreEqual(Prop.Value, "SpriteEmitter29");
        }

        [TestMethod]
        public void PropertyCanReadMapValueTest()
        {
            //No MapProperty found in Packages
            //Cannot implement.
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void PropertyCanReadFixedArrayValueTest()
        {
            //No FixedArrayProperty found in Packages
            //Cannot implement.
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void PropertyCanReadNoneValueTest()
        {
            string name = "SpriteEmitter544";
            Export Exp = ExportTable.FindAll(n => n.NameTableRef == NameTable.IndexOf(name))?.First();
            int PropertyOffset = 0x1E1 + Exp.SerialOffset + 28;
            //Act
            Property Prop = new Property(pf.Bytes, PropertyOffset);
            //Assert
            string Str = NameTable[Prop.NameTableRef];
            Assert.AreEqual(Str, "None");
            Assert.AreEqual(Prop.Value, null);
            Assert.AreEqual(Prop.Size, 1);
            Assert.AreEqual(Prop.Type, PropertyType.None);
        }
        [TestMethod]
        public void PropertyCanReadEntireObject()
        {
            var pf1 = new PackageReader();
            pf1.Read("D:\\la2\\system\\lineageeffect.u");
            var header1 = new Header(pf1.Bytes);
            var ExportTable1 = new ExportTable(header1, pf1.Bytes);
            var ImportTable1 = new ImportTable(header1, pf1.Bytes);
            var NameTable1 = new NameTable(header1, pf1.Bytes);
            //Property.Resolve = Resolver;
            var Exp = ExportTable1[0x793];
            Assert.AreEqual("MeshEmitter0", NameTable1[Exp.NameTableRef]);

            var list = Property.ReadProperties(pf1.Bytes, Exp.SerialOffset + 28);
            Assert.IsNotNull(list);
            Assert.AreEqual(31, list.Count);
        }
    }
}