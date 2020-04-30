using System;
using System.Collections.Generic;
using Factory.Models;
using Factory.Common;

namespace Factory.Logic
{
  /// <summary>
  /// This is not a real mocking framework, but this will work for launching rockets.
  /// </summary>
  public static class MockRockets
  {
    public static IEnumerable<RocketOrder> GetSimpleOrders()
    {
      var rv = new List<RocketOrder>();
      var o1 = new RocketOrder()
      {
        OrderId = 1001,
        Customer = MockConstants.TEST_CUSTOMER_1,
        Spaceship = new Spaceship(),
        CarrierRocket = new CargoRocket()
      };
      rv.Add(o1);
      var o2 = new RocketOrder()
      {
        OrderId = 1001,
        Customer = MockConstants.TEST_CUSTOMER_8,
        BallisticRocket = new BallisticRocket(),
        CarrierRocket = new CargoRocket()
      };
      rv.Add(o2);
      return rv;
    }

    public static IEnumerable<RocketOrder> GetComplexOrders()
    {
      var rv = new List<RocketOrder>();
      var o1 = new RocketOrder()
      {
        OrderId = 1001,
        Customer = MockConstants.TEST_CUSTOMER_1,
        Spaceship = GetExplodingSpaceship()
      };
      rv.Add(o1);
      var o2 = new RocketOrder()
      {
        OrderId = 1001,
        Customer = MockConstants.TEST_CUSTOMER_8,
        BallisticRocket = new BallisticRocket(),
        CarrierRocket = new CargoRocket()
      };
      rv.Add(o2);
      var o3 = new RocketOrder()
      {
        OrderId = 1001,
        Customer = MockConstants.TEST_CUSTOMER_2,
        Spaceship = GetExplodingSpaceship(),
        CarrierRocket = new CargoRocket()
      };
      rv.Add(o3);
      return rv;
    }

    private static BottleRocket GetDudBottleRocket()
    {
      return new BottleRocket()
      {
          Explodes = false
      };
    }

    private static Spaceship GetExplodingSpaceship()
    {
      return new Spaceship()
      {
          Explodes = true
      };
    }

  }
}