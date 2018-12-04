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
        public void TestArgumentNull()
        {
            var compositor = new ListCompositor<int>();
            Assert.ThrowsException<ArgumentNullException>(() => compositor.AddSource(null, 0));
        }
    }
}
