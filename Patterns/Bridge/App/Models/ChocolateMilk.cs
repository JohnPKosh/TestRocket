namespace bridge.Models
{
  /// <summary>
  /// A concrete chocolate milk implementation that bridges a drink item with an IDispenser.
  /// </summary>
  class ChocolateMilk : Product
  {
    /// <summary>The default constructor that does not do anything but apply a DrinkDispenser to the base Product class.</summary>
    public ChocolateMilk() : base(new DrinkDispenser()) { }

    /// <summary>
    /// The overriden vend method to call IDispenser bridge logic.
    /// Notice that we changed the method declaration to use a Lambda operator to dispense our chocolate milk.
    /// </summary>
    public override string Vend() => m_Dispenser.DispenseItem("chocolate milk");
  }
}
