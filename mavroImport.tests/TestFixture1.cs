using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mavroImport.tests
{
    [TestFixture]
    public class WhenLoadDataFile
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ShouldGetFile()
        {
            var x = 1;
            var y = 1; 

            Assert.AreEqual(x, y);
        }
    }
}
