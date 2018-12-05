using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocup.Util;

namespace Vocup.UnitTests
{
    [TestClass]
    public class ListCompositorTest
    {
        [TestMethod]
        public void TestAddArgumentNull()
        {
            var compositor = new ListCompositor<int>();
            Assert.ThrowsException<ArgumentNullException>(() => compositor.AddSource(null, 0));
        }

        [TestMethod]
        public void TestAddArgumentOutOfRange()
        {
            var compositor = new ListCompositor<int>();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => compositor.AddSource(new List<int>(), -0.3));
        }

        [TestMethod]
        public void TestToListArgumentOutOfRange()
        {
            var compositor = new ListCompositor<int>();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => compositor.ToList(-1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => compositor.ToList(1));
            compositor.AddSource(new List<int>() { 3, 4, 5, 6, 7, 8 }, 0.3);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => compositor.ToList(13));
        }
    }
}
