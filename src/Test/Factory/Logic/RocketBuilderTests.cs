using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azos;
using Azos.Scripting;
using Factory.Common;
using Factory.Models;

namespace Factory.Logic
{
  [Runnable]
  public class RocketBuilderTests
  {
    [Run]
    public void CreateOneSimpleRocket()
    {
      /* Arrange */
      IEnumerable<RocketOrder> got;

      /* Act */
      got = MockRockets.GetSimpleOrders();
      //got.See();

      /* Assert */
      Aver.IsNotNull(got);
      Aver.IsTrue(got.Any());

      var gotDescription = RocketBuilder.CreateRocket(got.FirstOrDefault());
      gotDescription.See(); 

      Aver.IsTrue(!string.IsNullOrWhiteSpace(gotDescription));
    }

    [Run]
    public async Task BuildSimpleRockets()
    {
      /* Arrange */
      IEnumerable<RocketOrder> got;

      /* Act */
      got = MockRockets.GetSimpleOrders();
      //got.See();

      /* Assert */
      Aver.IsNotNull(got);
      Aver.IsTrue(got.Any());

      var builtRockets = RocketBuilder.Build(got);
      Aver.IsTrue(await builtRockets.AnyAsync());

      await foreach (var rocket in builtRockets)
      {
        rocket.See();
      }      
    }

    [Run]
    public void CreateOneComplexRocket()
    {
      /* Arrange */
      IEnumerable<RocketOrder> got;

      /* Act */
      got = MockRockets.GetComplexOrders();
      //got.See();

      /* Assert */
      Aver.IsNotNull(got);
      Aver.IsTrue(got.Any());

      var gotDescription = RocketBuilder.CreateRocket(got.FirstOrDefault());
      gotDescription.See(); 

      Aver.IsTrue(!string.IsNullOrWhiteSpace(gotDescription));
    }

    
    [Run]
    public async Task BuildComplexRockets()
    {
      /* Arrange */
      IEnumerable<RocketOrder> got;

      /* Act */
      got = MockRockets.GetComplexOrders();
      //got.See();

      /* Assert */
      Aver.IsNotNull(got);
      Aver.IsTrue(got.Any());

      var builtRockets = RocketBuilder.Build(got);
      Aver.IsTrue(await builtRockets.AnyAsync());

      await foreach (var rocket in builtRockets)
      {
        rocket.See();
      }      
    }

  }
}