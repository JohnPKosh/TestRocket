namespace decorate.Models
{
  /// <summary>
  /// Represents an abstract class that our concrete decorator implementors will
  /// inherit from to provide our Decorator pattern implementation.
  /// </summary>
  public abstract class Decorator // : IDecorate     We can just excise the IDecorate interface since this abstract class performs the same duties.
  {
    /// <summary>Method used to apply a decoration.</summary>
    public string Location { get; set; }

    /// <summary>Property specifying a location of the decoration.</summary>
    public abstract string Apply();
  }
}
