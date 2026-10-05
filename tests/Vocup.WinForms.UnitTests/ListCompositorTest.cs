using System;
using Vocup.Util;
using Xunit;

namespace Vocup.UnitTests
{
    public class ListCompositorTest
    {
        [Fact]
        public void TestAddArgumentNull()
        {
            var compositor = new ListCompositor<int>();
            Assert.Throws<ArgumentNullException>(() => compositor.AddSource(null!, 0));
        }

        [Fact]
        public void TestAddArgumentOutOfRange()
        {
            var compositor = new ListCompositor<int>();
            Assert.Throws<ArgumentOutOfRangeException>(() => compositor.AddSource([], -0.3));
        }

        [Fact]
        public void TestToListArgumentOutOfRange()
        {
            var compositor = new ListCompositor<int>();
            Assert.Throws<ArgumentOutOfRangeException>(() => compositor.ToList(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => compositor.ToList(1));
            compositor.AddSource([3, 4, 5, 6, 7, 8], 0.3);
            Assert.Throws<ArgumentOutOfRangeException>(() => compositor.ToList(13));
        }
    }
}
