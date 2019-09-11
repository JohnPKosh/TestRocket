using System;

using Azos;
using Azos.Scripting;
using Azos.Apps.Injection;
using Azos.Conf;

using Factory.Logic;
using System.Linq;

namespace RocketFactoryTests
{
  [Runnable]
  public class SampleTests
  {
    [Run]
    public void TrunWorking()
    {
      /* Arrange */
      int x;

      /* Act */
      x = 1;

      /* Assert */
      Aver.IsTrue(x == 1);
    }

    [Run]
    public void CanLoadRockets()
    {
      /* Arrange */
      IRocketLoader loader = new RocketOrderFileLoader();

      /* Act */
      var got = new CurrentRocketOrders(loader);
      //got.See();

      /* Assert */
      Aver.IsTrue(got.RocketsOrders.Any());
    }

  }
}
