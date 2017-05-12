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
    public class HeaderTests
    {
        PackageReader pf { set; get; } 

        [TestInitialize]
        public void Initialize()
        {
            try
            {
                pf = new PackageReader();
                pf.Read("D:\\la2\\maps\\17_21.unr");
            }
            catch (Exception ex)
            {
                //Assert (no exceptions reading...)
                Assert.Fail(ex.ToString());
            }
        }
        [TestMethod()]
        public void HeaderTest()
        {
            //Alloc
            //Act
            try
            {
                Header H = new Header(pf.Bytes);
                Assert.IsTrue(H.PackageVersion != 0);
                Assert.IsTrue(H.NameCount > 0);
                Assert.IsTrue(H.ImportCount > 0);
            }
            //Assert
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }

        }
    }
}