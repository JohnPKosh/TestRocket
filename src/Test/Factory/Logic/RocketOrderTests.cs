using System.Collections.Generic;
using System.Linq;
using Azos;
using Azos.Scripting;
using Factory.Common;
using Factory.Models;

namespace Factory.Logic
{
  [Runnable]
  public class RocketOrderTests
  {

    #region Unit test some basic mock orders.

    [Run]
    public void MockRocketOrdersCreate()
    {
      /* Arrange */
      IEnumerable<RocketOrder> got;

      /* Act */
      got = MockRockets.GetSimpleOrders();
      got.See();

      /* Assert */
      Aver.IsNotNull(got);
      Aver.IsTrue(got.Any());
      var gotObject1 = got.FirstOrDefault(x => x.Customer == MockConstants.TEST_CUSTOMER_1);
      Aver.IsNotNull(gotObject1);
      Aver.IsTrue(gotObject1.Spaceship.Height == 68); // 68 is default height of a space ship
    }

    [Run]
    public void MockRocketOrdersHasMoreThanOneOrder()
    {
      /* Arrange */
      IEnumerable<RocketOrder> got;

      /* Act */
      got = MockRockets.GetSimpleOrders();
      //got.See();

      /* Assert */
      Aver.IsNotNull(got);
      Aver.IsTrue(got.Any());
      Aver.IsTrue(got.Count() > 1);
    }

    [Run]
    public void MockHasAnOrder()
    {
      /* Arrange */
      IEnumerable<RocketOrder> got;

      /* Act */
      got = MockRockets.GetSimpleOrders();
      //got.See();

      /* Assert */
      Aver.IsNotNull(got);
      Aver.IsTrue(got.Any());
      var gotObject1 = got.FirstOrDefault(x => x.Customer == MockConstants.TEST_CUSTOMER_8);
      Aver.IsNotNull(gotObject1);
      gotObject1.See();
    }

    [Run]
    public void MockOrderedBallisticRocket()
    {
      /* Arrange */
      IEnumerable<RocketOrder> got;

      /* Act */
      got = MockRockets.GetSimpleOrders();
      //got.See();

      /* Assert */
      Aver.IsNotNull(got);
      Aver.IsTrue(got.Any());
      var gotObject1 = got.FirstOrDefault(x => x.Customer == MockConstants.TEST_CUSTOMER_8);
      Aver.IsNotNull(gotObject1);
      Aver.IsNotNull(gotObject1?.BallisticRocket);
      gotObject1.See();
    }

    [Run]
    public void MockForgotSpaceship()
    {
      /* Arrange */
      IEnumerable<RocketOrder> got;

      /* Act */
      got = MockRockets.GetSimpleOrders();
      //got.See();

      /* Assert */
      Aver.IsNotNull(got);
      Aver.IsTrue(got.Any());
      var gotObject1 = got.FirstOrDefault(x => x.Customer == MockConstants.TEST_CUSTOMER_8);
      Aver.IsNotNull(gotObject1);
      Aver.IsNull(gotObject1?.Spaceship);
      gotObject1.See();
    }

    #endregion


  }
}