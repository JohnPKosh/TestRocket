using System;
using System.Linq;
using Factory.Logic;
using Factory.Util;
using Xunit;

namespace FactoryXUnit
{
  public class SampleTests
  {
    [Fact]
    public void XUnitWorking()
    {
      /* Arrange */
      int x;

      /* Act */
      x = 1;

      /* Assert */
      Assert.True(x == 1);
    }

    [Fact]
    public void CanLoadRockets()
    {
      /* Arrange */
      IRocketLoader loader = new RocketOrderFileLoader();

      /* Act */
      var got = new CurrentRocketOrders(loader);
      Console.WriteLine(got.ToPrettyString());

      /* Assert */
      Assert.True(got.RocketsOrders.Any());
    }
  }
}
