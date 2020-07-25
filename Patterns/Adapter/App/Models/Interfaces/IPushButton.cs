namespace adapt.Models.Interfaces
{
  /// <summary>
  /// An interface to allow you to push my buttons.
  /// </summary>
  /// <remarks> This is not the same as flipping my switch! We will need an adapter for that. </remarks>
  public interface IPushButton
  {
    /// <summary> The primary method of a button is to push it, push it real good. </summary>
    void Push();

    /// <summary> Property to track how many time you have pushed my button. </summary>
    int PushedCount { get; set; }
  }
}
