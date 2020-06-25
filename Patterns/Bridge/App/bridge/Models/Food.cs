using bridge.Models.Interfaces;

namespace bridge.Models
{
  /// <summary>
  /// A concrete food product implementation that bridges a food item with an IDispenser.
  /// </summary>
  public class Food : Product
  {
    private string m_FoodName;

    /// <summary>
    /// Default constructor accepting an IDispenser and a food name string.
    /// </summary>
    public Food(IDispenser dispenser, string foodName):base(dispenser)
    {
      m_FoodName = foodName;
    }

    /// <summary>
    /// The overriden vend method to call IDispenser bridge logic.
    /// </summary>
    public override string Vend()
    {
      return m_Dispenser.DispenseItem(m_FoodName);
    }
  }
}
