namespace bridge.Models.Interfaces
{
  /// <summary>Interface for dispensing an item for consumption.</summary>
  public interface IDispenser
  {
    /// <summary>Method to encapsulate the dispensing an item with a specific name.</summary>
    string DispenseItem(string productName);
  }
}
