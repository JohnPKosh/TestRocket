using bridge.Models.Interfaces;

namespace bridge.Models
{
  /// <summary>
  /// Logic class to dispense an item. Intended to bridge
  /// a concrete Food class to the logic needed to dispense it.
  /// </summary>
  public class FoodDispenser : IDispenser
  {
    /// <summary> Method to dispense a food related item with a specific name. </summary>
    public string DispenseItem(string productName) => $"Food dispenser is now dispensing a {productName}.";
  }
}
