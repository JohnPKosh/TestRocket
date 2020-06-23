namespace decorate.Models
{
  /// <summary>
  /// An interface to specify the basic logic of a Decorator.
  /// * Note - This interface could be removed since our abstract
  /// Decorator class already enforces the same contract
  /// and no other implementors exist.
  /// </summary>
  interface IDecorate
  {
    /// <summary>Method used to apply a decoration.</summary>
    string Apply();

    /// <summary>Property specifying a location of the decoration.</summary>
    string Location { get; set; }
  }
}
