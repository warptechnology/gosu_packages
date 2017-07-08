using Microsoft.VisualStudio.TestTools.UnitTesting;
using L2Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L2Package.DataStructures;
using L2Package.Body;
using Moq;

namespace L2Package.Tests
{

    [TestClass()]
    public class L2BasicSerializerTests
    {
        string Resolver(Index ind)
        {
            return NameTable[ind];
        }
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
                Property.Resolve = Resolver;
            }
            catch (Exception ex)
            {
                //Assert (no exceptions reading...)
                Assert.Fail(ex.ToString());
            }
        }

        //[TestMethod()]
        //public void InitializeTest()
        //{
        //    try
        //    {
        //        //Alloc
        //        IUnrealSerializer Ser = new L2BasicSerializer();
        //        //Act
        //        Ser.Initialize(header, NameTable, ExportTable, ImportTable, pf.Bytes);
        //        //Assert

        //    }
        //    catch (Exception ex)
        //    {
        //        //Assert
        //        Assert.Fail(ex.ToString());
        //    }
        //}

        [TestMethod()]
        public void DeserializeTest()
        {
            //try
            //{
            //Alloc
            IUnrealSerializer Ser = new L2BasicSerializer();
            Ser.Initialize(header, NameTable, ExportTable, ImportTable, pf.Bytes);
            string TestName = "StaticMeshActor1";
            //int Index = NameTable.IndexOf(TestName);
            //Export Exp = ExportTable.Find(Index);
            Export Exp = ExportTable[2112];//.FindAll(n => n.NameTableRef == NameTable.IndexOf(TestName))?.First();
                                           //Act
            UObject obj = Ser.Deserialize(Exp);
            Assert.IsInstanceOfType(obj, typeof(StaticMeshActor));
            StaticMeshActor sma = obj as StaticMeshActor;
            //Assert                
            Assert.IsTrue(sma.Name == TestName);
            Assert.AreEqual(sma.Location.X, -88879.6015625d);
            Assert.AreEqual(sma.Location.Y, 118961.0703125d);
            Assert.AreEqual(sma.Location.Z, -3343.093505859375d);
            Assert.AreEqual(sma.Rotation.Pitch, 1912);
            Assert.AreEqual(sma.Rotation.Yaw, 28112);
            Assert.AreEqual(sma.Rotation.Roll, 0);
            Assert.AreEqual(sma.StaticMesh, -110);
            //}
            //catch (Exception ex)
            //{
            //    //Assert
            //    Assert.Fail(ex.ToString());
            //}
        }

    }
}