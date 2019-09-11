using System;
using System.Collections.Generic;
using Factory.Models;
using Factory.Common;

namespace Factory.Logic
{
  public static class MockRockets
  {

    public static IEnumerable<RocketOrder> GetSimpleOrders()
    {
        var rv = new List<RocketOrder>();
        var o1 = new RocketOrder(){
            OrderId = 1001,
            Customer = MockConstants.TEST_CUSTOMER_1,
            Spaceship = new Spaceship(),
            CarrierRocket = new CargoRocket()
        };
        rv.Add(o1);
        var o2 = new RocketOrder(){
            OrderId = 1001,
            Customer = MockConstants.TEST_CUSTOMER_8,
            BallisticRocket = new BallisticRocket(),
            CarrierRocket = new CargoRocket()
        };
        rv.Add(o2);
        return rv;
    }

  }
}