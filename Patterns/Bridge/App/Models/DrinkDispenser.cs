using bridge.Models.Interfaces;

namespace bridge.Models
{
  /// <summary>
  /// Logic class to dispense an item. Intended to bridge
  /// a concrete Drink class to the logic needed to dispense it.
  /// </summary>
  public class DrinkDispenser : IDispenser
  {
    /// <summary>Method to dispense a drink related item with a specific name.</summary>
    public string DispenseItem(string productName) => $"Drink dispenser is now pouring a {productName}.";
  }
}
