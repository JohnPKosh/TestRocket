using bridge.Models.Interfaces;

namespace bridge.Models
{
  /// <summary>
  /// A concrete drink product implementation that bridges a drink item with an IDispenser.
  /// </summary>
  public class Drink : Product
  {
    private string m_DrinkName;

    /// <summary>
    /// Default constructor accepting an IDispenser and a drink name string.
    /// </summary>
    public Drink(IDispenser dispenser, string drinkName) : base(dispenser)
    {
      m_DrinkName = drinkName;
    }

    /// <summary>
    /// The overriden vend method to call IDispenser bridge logic.
    /// </summary>
    public override string Vend()
    {
      return m_Dispenser.DispenseItem(m_DrinkName);
    }
  }
}
