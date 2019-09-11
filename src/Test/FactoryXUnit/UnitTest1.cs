using System;
using Xunit;

namespace FactoryXUnit
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var x = 1;
            Assert.True(x == 1);
        }
    }
}
