using Microsoft.VisualStudio.TestTools.UnitTesting;
using L2Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L2Package.DataStructures;

namespace L2Package.Tests
{
    [TestClass()]
    public class L2BasicSerializerTests
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

        [TestMethod()]
        public void InitializeTest()
        {
            try
            {
                //Alloc
                IUnrealSerializer Ser = new L2BasicSerializer();
                //Act
                Ser.Initialize(header, NameTable, ExportTable, ImportTable, pf.Bytes);
                //Assert
                
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void DeserializeTest()
        {
            try
            {
                //Alloc
                IUnrealSerializer Ser = new L2BasicSerializer();
                Ser.Initialize(header, NameTable, ExportTable, ImportTable, pf.Bytes);
                string TestName = "StaticMeshActor0";
                int Index = NameTable.IndexOf(TestName);
                Export Exp = ExportTable.Find(Index);
                //Act
                UObject obj = Ser.Deserialize(Exp);
                StaticMeshActor sma = obj as StaticMeshActor;
                //Assert                
                Assert.IsTrue(sma.name == TestName);
                Assert.AreEqual(sma.location.X, -83112.00d);
                Assert.AreEqual(sma.location.Y, 111360.00d);
                Assert.AreEqual(sma.location.Z, -5154.00d);
                Assert.AreEqual(sma.static_mesh.index, -459);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Fail(ex.ToString());
            }
        }
        
    }
}