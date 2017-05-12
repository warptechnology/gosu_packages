using Microsoft.VisualStudio.TestTools.UnitTesting;
using L2Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace L2Package.Tests
{
    [TestClass()]
    public class NameTableTests
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
        public void NameTableTest()
        {
            //Alloc
            //Act (Act is Alloc here)
            try
            {
                NameTable nt = new NameTable(header, pf.Bytes);
            }
            //Assert
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
       
        [TestMethod()]
        public void NameTableIndexOperatorTest()
        {
            //Alloc
            string str = "";
            NameTable nt = new NameTable(header, pf.Bytes);
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
        public void NameTableCopyToTest()
        {
            //Alloc
            NameTable nt = new NameTable(header, pf.Bytes);
            string[] str = new string[nt.Count];
            //Act 
            try
            {
                nt.CopyTo(str, 0);
                for (int i = 0; i < nt.Count; i++)
                {
                    if(str[i] != nt[i])
                    {
                        Assert.Fail("Not equal");
                        break;
                    }
                }
            }
            //Assert
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void GetNameTableEnumeratorTest()
        {
            //Alloc
            NameTable nt = new NameTable(header, pf.Bytes);
            //Act 
            try
            {
                IEnumerator nte = nt.GetEnumerator();
                Assert.IsNotNull(nte);
            }
            //Assert
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void MoveNameTableEnumeratorTest()
        {
            //Alloc
            NameTable nt = new NameTable(header, pf.Bytes);
            IEnumerator nte = nt.GetEnumerator();
            //Act 
            try
            {
                nte.MoveNext();
                string First = (string)nte.Current;
                nte.MoveNext();
                string Second = (string)nte.Current;
                Assert.AreNotSame(First, Second);
                Assert.IsNotNull(First);
                Assert.IsNotNull(Second);
            }
            //Assert
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void ResetNameTableEnumeratorTest()
        {
            //Alloc
            NameTable nt = new NameTable(header, pf.Bytes);
            IEnumerator nte = nt.GetEnumerator();
            //Act 
            try
            {
                nte.MoveNext();
                string First = (string)nte.Current;
                nte.MoveNext();
                string Second = (string)nte.Current;
                nte.Reset();
                nte.MoveNext();
                string Third = (string)nte.Current;
                Assert.IsTrue(First == Third);
                Assert.IsNotNull(Third);
            }
            //Assert
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
        [TestMethod()]
        public void NameTableEnumeratorEnumeratesAllTest()
        {
            //Alloc
            NameTable nt = new NameTable(header, pf.Bytes);
            IEnumerator nte = nt.GetEnumerator();
            int i = 0;
            //Act 
            try
            {
                while (nte.MoveNext())
                    i++;
                Assert.AreEqual(i, nt.Count);
            }
            //Assert
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
        [TestMethod()]
        public void NameTableLinqTest()
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