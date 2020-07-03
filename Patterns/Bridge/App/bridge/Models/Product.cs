using bridge.Models.Interfaces;

namespace bridge.Models
{
  /// <summary>
  /// An abstrace product class that accepts an IDispenser interface
  /// that will supply a bridge to the logic needed to dispense it.
  /// </summary>
  public abstract class Product
  {
    protected IDispenser m_Dispenser;

    /// <summary> The default constructor that accepts an IDispenser interface. </summary>
    public Product(IDispenser dispenser)
    {
      m_Dispenser = dispenser;
    }

    /// <summary> The abstract method to override when bridging the logic. </summary>
    public abstract string Vend();
  }
}
