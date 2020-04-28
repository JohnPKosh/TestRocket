using System.Collections.Generic;
using System.Threading.Tasks;
using Factory.Models;

namespace Factory.Logic
{
  /// <summary>
  /// This is the rocket builder logic that spits out the descriptions of the rockets in the provided RocketOrder collection.
  /// </summary>
  public static class RocketBuilder
  {
    /// <summary>
    /// Method to build rocket descriptions from the supplied orders
    /// </summary>
    public static async IAsyncEnumerable<string> Build(IEnumerable<RocketOrder> orders)
    {
      foreach (var order in orders)
      {
        await Task.Delay(500); // Do some pretend work here on the order
        yield return CreateRocket(order);
      }
    }

    /// <summary>
    /// Create a description of the RocketOrder here using fancy new CSharp 8 switch syntactic sugar.
    /// </summary>
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

    #region Private Utility Methods
    private static string DescribeRocket(IRocket rocket, RocketOrder order)
    => $"[{order.OrderId}:{order.Customer}] A {rocket.Height} meter {rocket.GetType().Name} was built to reach {rocket.MaximumVelocity()} feet per second. Explodes: {rocket.Explodes}";

    private static string DescribeCarrierRocket(IRocket rocket, RocketOrder order)
    {
      return $@"[{order.OrderId}:{order.Customer}] A {rocket.Height} meter {rocket.GetType().Name} was built to reach {rocket.MaximumVelocity()} feet per second 
      and can carry {(rocket.PayloadCapacity.HasValue ? rocket.PayloadCapacity.Value : 0)} pounds of cargo. Explodes: {rocket.Explodes}";
    }
    #endregion
  }
}