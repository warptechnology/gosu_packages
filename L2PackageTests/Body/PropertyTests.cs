using Microsoft.VisualStudio.TestTools.UnitTesting;
using L2Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Export Exp = ExportTable.Find(NameTable.IndexOf(name));
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
            Export Exp = ExportTable.Find(NameTable.IndexOf(name));
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
            Export Exp = ExportTable.Find(NameTable.IndexOf(name));
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
            Export Exp = ExportTable.Find(NameTable.IndexOf(name));
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
            Export Exp = ExportTable.Find(NameTable.IndexOf(name));
            int PropertyOffset = 0x183 + Exp.SerialOffset + 28;
            //Act
            Property Prop = new Property(pf.Bytes, PropertyOffset);
            //Assert
            string Str = NameTable[Prop.NameTableRef];
            Assert.AreEqual(Str, "Texture");
            Assert.IsTrue(Prop.Value is Index);
            Assert.AreEqual(Prop.Size, 7);
            Index val = Prop.Value as Index;
            Assert.AreEqual(val.Value, -105);
        }
        [TestMethod]
        public void PropertyCanReadStrValueTest()
        {
            //have no idea how should str properties work... (it is not a sting property oO)
            Assert.Fail("Needs further research for str properties");
        }
        [TestMethod]
        public void PropertyCanReadClassValueTest()
        {
            //have no idea how should class properties work...
            Assert.Fail("Needs further research for class properties");
        }
        [TestMethod]
        public void PropertyCanReadArrayValueTest()
        {
            //have no idea how should array properties work...
            Assert.Fail("Needs further research for array properties");
        }
        [TestMethod]
        public void PropertyCanReadStructValueTest()
        {
            //have no idea how should Struct properties work...
            Assert.Fail("Needs further research for Struct properties");
        }
        [TestMethod]
        public void PropertyCanReadVectorValueTest()
        {

            string name = "SpriteEmitter544";
            Export Exp = ExportTable.Find(NameTable.IndexOf(name));
            int PropertyOffset = 0x97 + Exp.SerialOffset + 28;
            //Act
            Property Prop = new Property(pf.Bytes, PropertyOffset);
            //Assert
            string Str = NameTable[Prop.NameTableRef];
            Assert.AreEqual(Str, "SpinCCWorCW");
            Assert.IsTrue(Prop.Value is DataStructures.UVector);
            Assert.AreEqual(Prop.Size, 16);
            DataStructures.UVector uv = Prop.Value as DataStructures.UVector;
            Assert.AreEqual(uv.X, 1.00f);
            Assert.AreEqual(uv.Y, 0.50f);
            Assert.AreEqual(uv.Z, 0.50f);
        }
        [TestMethod]
        public void PropertyCanReadRotatorValueTest()
        {

            string name = "StaticMeshActor2";
            Export Exp = ExportTable.Find(NameTable.IndexOf(name));
            int PropertyOffset = 0x42 + Exp.SerialOffset + 28;
            //Act
            Property Prop = new Property(pf.Bytes, PropertyOffset);
            //Assert
            string Str = NameTable[Prop.NameTableRef];
            Assert.AreEqual(Str, "Rotation");
            Assert.IsTrue(Prop.Value is DataStructures.URotator);
            Assert.AreEqual(Prop.Size, 16);
            DataStructures.URotator ur = Prop.Value as DataStructures.URotator;
            Assert.AreEqual(ur.Pitch, 0);
            Assert.AreEqual(ur.Yaw, 2048);
            Assert.AreEqual(ur.Roll, 0);
        }

        [TestMethod]
        public void PropertyCanReadStringValueTest()
        {
            string name = "SpriteEmitter544";
            Export Exp = ExportTable.Find(NameTable.IndexOf(name));
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
            //have no idea how should map properties work...
            Assert.Fail("Needs further research for map properties");
        }
        [TestMethod]
        public void PropertyCanReadFixedArrayValueTest()
        {
            //have no idea how should FixedArray properties work...
            Assert.Fail("Needs further research for FixedArray properties");
        }
        [TestMethod]
        public void PropertyCanReadNoneValueTest()
        {
            string name = "SpriteEmitter544";
            Export Exp = ExportTable.Find(NameTable.IndexOf(name));
            int PropertyOffset = 0x1E1 + Exp.SerialOffset + 28;
            //Act
            Property Prop = new Property(pf.Bytes, PropertyOffset);
            //Assert
            string Str = NameTable[Prop.NameTableRef];
            Assert.AreEqual(Str, "None");
            Assert.IsTrue(Prop.Value is object);
            Assert.AreEqual(Prop.Size, 1);
            Assert.IsNull(Prop.Value);
        }
    }
}