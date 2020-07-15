namespace bridge.Models
{
  /// <summary>
  /// A concrete pancake implementation that bridges a food item with an IDispenser.
  /// </summary>
  public class Pancake : Product
  {
    /// <summary>The default constructor that does not do anything but apply a FoodDispenser to the base Product class.</summary>
    public Pancake(): base(new FoodDispenser()) {  }

    /// <summary> The overriden vend method to call IDispenser bridge logic. </summary>
    public override string Vend()
    {
      return m_Dispenser.DispenseItem("pancake");
    }
  }
}
