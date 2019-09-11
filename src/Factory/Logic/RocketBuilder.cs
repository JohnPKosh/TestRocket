using System.Collections.Generic;
using System.Threading.Tasks;
using Factory.Models;

namespace Factory.Logic
{
  public static class RocketBuilder
  {
    public static async IAsyncEnumerable<string> Build(IEnumerable<RocketOrder> orders)
    {
      foreach (var order in orders)
      {
        await Task.Delay(500); // Do some pretend work here on the order
        yield return CreateRocket(order);
      }
    }

    public static string CreateRocket(RocketOrder order)
    => (order.BallisticRocket, order.CarrierRocket, order.Spaceship) switch
    {
      (null, null, null) => $"[{order.OrderId}:{order.Customer}] Nothing to build here",
      (null, null, Spaceship _) => DescribeRocket(order.Spaceship, order),
      (null, CargoRocket _, null) => DescribeCarrierRocket(order.CarrierRocket, order),
      (BallisticRocket _, null, null) => DescribeRocket(order.BallisticRocket, order),
      (BallisticRocket _, CargoRocket _, Spaceship _) => $"[{order.OrderId}:{order.Customer}] All Rockets Built!",
      _ => $"[{order.OrderId}:{order.Customer}] A few rockets were built but I am too lazy to name them all."
    };

    private static string DescribeRocket(IRocket rocket, RocketOrder order)
    => $"[{order.OrderId}:{order.Customer}] A {rocket.Height} meter {rocket.GetType().Name} was built to reach {rocket.MaximumVelocity()} feet per second.";

    private static string DescribeCarrierRocket(IRocket rocket, RocketOrder order)
    {
      return $@"[{order.OrderId}:{order.Customer}] A {rocket.Height} meter {rocket.GetType().Name} was built to reach {rocket.MaximumVelocity()} feet per second 
      and can carry {(rocket.PayloadCapacity.HasValue ? rocket.PayloadCapacity.Value : 0)} pounds of cargo.";
    }
  }
}