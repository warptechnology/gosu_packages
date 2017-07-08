using Microsoft.VisualStudio.TestTools.UnitTesting;
using L2Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using L2Package.DataStructures;

namespace L2Package.Tests
{
    [TestClass()]
    public class L2PackageTests
    {
        internal Mock<IUnrealSerializer> SzrMock;

        [TestInitialize]
        public void Initialize()
        {
            SzrMock = new Mock<IUnrealSerializer>();
            SzrMock.Setup(a => a.Deserialize(It.IsAny<Export>())).Returns(new StaticMeshActor());


        }

        [TestMethod]
        public void PackageCtorDoesntFail()
        {
            // Alloc is act for ctor
            try
            {
                L2Package Pack = new L2Package("D:\\la2\\maps\\17_21.unr", SzrMock.Object);
            }
            catch (Exception Ex)
            {
                // Assert
                Assert.Fail();
            }

        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexerThrowsExLessZero()
        {
            //Alloc
            L2Package Pack = new L2Package("D:\\la2\\maps\\17_21.unr", SzrMock.Object);
            //Act
            var qwe = Pack[-25];
            //Assert

        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexerThrowsExMoreThenContains()
        {
            //Alloc
            L2Package Pack = new L2Package("D:\\la2\\maps\\17_21.unr", SzrMock.Object);
            //Act
            var qwe = Pack[Pack.Count + 25];
            //Assert

        }
        [TestMethod]
        public void IndexerReturnsUObject()
        {
            L2Package Pack = new L2Package("D:\\la2\\maps\\17_21.unr", SzrMock.Object);

            var qwe = Pack[10];
            Assert.IsInstanceOfType(qwe, typeof(UObject));
        }

        [TestMethod()]
        public void CopyToTest()
        {
            L2Package Pack = new L2Package("D:\\la2\\maps\\17_21.unr", SzrMock.Object);

            var qwe = new UObject[Pack.Count];
            Pack.CopyTo(qwe, 0);
            bool OK = true;
            for (int i = 0; i < Pack.Count; i++)
            {
                OK = OK && qwe[i].Name == Pack[i].Name;
                if (!OK) break;
            }
            Assert.IsTrue(OK);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyToNullTest()
        {
            L2Package Pack = new L2Package("D:\\la2\\maps\\17_21.unr", SzrMock.Object);

            UObject[] qwe = null;

            Pack.CopyTo(qwe, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyToOutOfRange_IndexIsBiggerThenArraySizeTest()
        {
            L2Package Pack = new L2Package("D:\\la2\\maps\\17_21.unr", SzrMock.Object);

            UObject[] qwe = new UObject[Pack.Count];

            Pack.CopyTo(qwe, Pack.Count + 300);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyToOutOfRange_IndexLessThenZero()
        {
            L2Package Pack = new L2Package("D:\\la2\\maps\\17_21.unr", SzrMock.Object);

            UObject[] qwe = new UObject[Pack.Count];

            Pack.CopyTo(qwe, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyToOutOfRange_PackWillNotFitInTheArray()
        {
            L2Package Pack = new L2Package("D:\\la2\\maps\\17_21.unr", SzrMock.Object);

            UObject[] qwe = new UObject[2];

            Pack.CopyTo(qwe, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyToOutOfRange_MultiDimensionalArrayGiven()
        {
            L2Package Pack = new L2Package("D:\\la2\\maps\\17_21.unr", SzrMock.Object);

            UObject[,] qwe = new UObject[200,200];

            Pack.CopyTo(qwe, 0);
        }

        [TestMethod]
        public void IEnumerable_GetEnumerator_ReturnsEnumerator()
        {
            L2Package Pack = new L2Package("D:\\la2\\maps\\17_21.unr", SzrMock.Object);

            IEnumerator<UObject> En = (Pack as IEnumerable<UObject>).GetEnumerator();

            Assert.IsInstanceOfType(En, typeof(L2PackageEnumerator));
        }
        [TestMethod]
        public void GetEnumerator_ReturnsEnumerator()
        {
            L2Package Pack = new L2Package("D:\\la2\\maps\\17_21.unr", SzrMock.Object);

            System.Collections.IEnumerator En = Pack.GetEnumerator();

            Assert.IsInstanceOfType(En, typeof(L2PackageEnumerator));
        }
    }
}