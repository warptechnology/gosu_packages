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
                string TestName = "StaticMeshActor1";
                int Index = NameTable.IndexOf(TestName);
                Export Exp = ExportTable.Find(Index);
                //Act
                UObject obj = Ser.Deserialize(Exp);
                //Assert                
                Assert.IsTrue(obj.name == TestName);
                

            }
            catch (Exception ex)
            {
                //Assert
                Assert.Fail(ex.ToString());
            }
        }
        
    }
}