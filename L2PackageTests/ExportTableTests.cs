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
    public class ExportTableTests
    {

        PackageReader pf { set; get; }
        Header header { set; get; }

        [TestInitialize]
        public void Initialize()
        {
            try
            {
                pf = new PackageReader();
                pf.Read("D:\\la2\\maps\\17_21.unr");
                header = new Header(pf.Bytes);
            }
            catch (Exception ex)
            {
                //Assert (no exceptions reading...)
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void ExportTableTest()
        {
            try
            {
                //alloc
                //act (Alloc is Act here, for ctor)
                ExportTable et = new ExportTable(header, pf.Bytes);
            }
            catch (Exception)
            {
                //assert
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void ExportTableCopyToTest()
        {
            //Alloc
            ExportTable et = new ExportTable(header, pf.Bytes);
            Export[] ea = new Export[et.Count];
            try
            {
                //Act
                et.CopyTo(ea, 0);
            }
            catch (Exception)
            {
                //Assert
                Assert.Fail();
            }

        }
        [TestMethod()]
        public void ExportTableIndexOperatorTest()
        {
            //Alloc
            Export str;
            ExportTable nt = new ExportTable(header, pf.Bytes);
            //Act 
            try
            {
                for (int i = 0; i < nt.Count; i++)
                    str = nt[i];
            }
            //Assert
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
        [TestMethod()]
        public void ExportTableGetEnumeratorTest()
        {

            //Alloc
            ExportTable et = new ExportTable(header, pf.Bytes);
            try
            {
                //Act
                ExportTableEnumerator<Export> ete = (ExportTableEnumerator<Export>)et.GetEnumerator();
                Assert.IsNotNull(ete);
            }
            catch (Exception)
            {
                //Assert
                Assert.Fail();
            }
        }
        [TestMethod()]
        public void MoveExportTableEnumeratorTest()
        {
            //Alloc
            Export exp;
            ExportTable et = new ExportTable(header, pf.Bytes);
            try
            {
                //Act
                
                ExportTableEnumerator<Export> ete = (ExportTableEnumerator<Export>)et.GetEnumerator();
                while (ete.MoveNext())
                {
                    exp = ((IEnumerator<Export>)ete).Current;
                }
            }
            catch (Exception)
            {
                //Assert
                Assert.Fail();
            }
        }
        [TestMethod()]
        public void ResetExportTableEnumeratorTest()
        {
            //Alloc
            
            ExportTable et = new ExportTable(header, pf.Bytes);
            try
            {
                //Act

                ExportTableEnumerator<Export> ete = (ExportTableEnumerator<Export>)et.GetEnumerator();
                ete.MoveNext(); ;
                Export First = (Export)ete.Current;
                ete.MoveNext();
                Export Second = (Export)ete.Current;
                Assert.IsTrue(First.SerialOffset.Value != Second.SerialOffset.Value);

            }
            catch (Exception)
            {
                //Assert
                Assert.Fail();
            }
        }
        [TestMethod()]
        public void ExportTableLinqTest()
        {
            //Alloc
            NameTable nt = new NameTable(header, pf.Bytes);
            //Act 
            try
            {
                List<string> TestList = nt.Where(T => T.Length > 5).ToList();
                Assert.IsNotNull(TestList);
            }
            //Assert
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
    }
}