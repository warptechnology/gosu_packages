using Microsoft.VisualStudio.TestTools.UnitTesting;
using L2Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Package
{
    [TestClass()]
    public class ImportTableTests
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
        public void ImportTableTest()
        {
            try
            {
                //alloc
                //act (Alloc is Act for ctor)
                ImportTable et = new ImportTable(header, pf.Bytes);
            }
            catch (Exception)
            {
                //assert
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void ImportTableCopyToTest()
        {
            //Alloc
            ImportTable IT = new ImportTable(header, pf.Bytes);
            Import[] IA = new Import[IT.Count];            
            try
            {
                //Act
                IT.CopyTo(IA, 0);
                //Assert
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void ImportTableIndexOperatorTest()
        {
            //Alloc
            Import imp;
            ImportTable nt = new ImportTable(header, pf.Bytes);
            //Act 
            try
            {
                for (int i = 0; i < nt.Count; i++)
                    imp = nt[i];
            }
            //Assert
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
        [TestMethod()]
        public void GetImportTableEnumeratorTest()
        {
            //Alloc
            ImportTable et = new ImportTable(header, pf.Bytes);
            try
            {
                //Act
                ImportTableEnumerator<Import> ete = (ImportTableEnumerator<Import>)et.GetEnumerator();
                Assert.IsNotNull(ete);
            }
            catch (Exception)
            {
                //Assert
                Assert.Fail();
            }
        }





        [TestMethod()]
        public void MoveImportTableEnumeratorTest()
        {
            //Alloc
            Import exp;
            ImportTable et = new ImportTable(header, pf.Bytes);
            try
            {
                //Act

                ImportTableEnumerator<Import> ete = (ImportTableEnumerator<Import>)et.GetEnumerator();
                while (ete.MoveNext())
                {
                    exp = ((IEnumerator<Import>)ete).Current;
                }
            }
            catch (Exception)
            {
                //Assert
                Assert.Fail();
            }
        }
        [TestMethod()]
        public void ResetImportTableEnumeratorTest()
        {
            //Alloc

            ImportTable et = new ImportTable(header, pf.Bytes);
            try
            {
                //Act

                ImportTableEnumerator<Import> ete = (ImportTableEnumerator<Import>)et.GetEnumerator();
                ete.MoveNext(); ;
                Import First = (Import)ete.Current;
                ete.MoveNext();
                Import Second = (Import)ete.Current;
                Assert.IsTrue(First.ObjectName.Value != Second.ObjectName.Value);

            }
            catch (Exception)
            {
                //Assert
                Assert.Fail();
            }
        }
        [TestMethod()]
        public void ImportTableLinqTest()
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