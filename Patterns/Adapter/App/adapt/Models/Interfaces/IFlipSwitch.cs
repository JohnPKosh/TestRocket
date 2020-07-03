namespace adapt.Models.Interfaces
{
  /// <summary>
  /// An interface to allow you to flip a switch.
  /// </summary>
  /// <remarks>
  /// This is not the same as pushing someone's button! We will need an adapter for that.
  /// </remarks>
  public interface IFlipSwitch
  {
    /// <summary>
    /// Flip the switch method.
    /// </summary>
    void FlipSwitch();

    /// <summary>
    /// Property to track the number of times you flipped by switch.
    /// </summary>
    int FlippedCount { get; set; }
  }
}
