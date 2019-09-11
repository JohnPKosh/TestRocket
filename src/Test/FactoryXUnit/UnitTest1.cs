using System;
using System.Linq;
using Factory.Logic;
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

        [Fact]
        public void CanLoadRockets()
        {
            IRocketLoader loader = new RocketOrderFileLoader();
            var orders = new CurrentRocketOrders(loader);
            Assert.True(orders.RocketsOrders.Any());
        }
    }
}
