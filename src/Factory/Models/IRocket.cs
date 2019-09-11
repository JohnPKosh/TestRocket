namespace Factory.Models
{
  /// <summary>
  /// This is the main interface definition for rockets. I only started with height, but we may want to add more properties.
  /// </summary>
  public interface IRocket
  {
    int Height { get; set; }

    #region Just extending the interface after the fact with some default implementations

    private static int maxVelocity = 10000;
    public decimal MaximumVelocity() => DefaultMaximumVelocity(this);

    protected static decimal DefaultMaximumVelocity(IRocket c)
    {
      return maxVelocity;
    }

    private static int? payloadCapacity = null;
    public int? PayloadCapacity => DefaultPayloadCapacity(this);
    protected static int? DefaultPayloadCapacity(IRocket c)
    {
      return payloadCapacity;
    }

    private static bool? explodes = false;
    public bool? Explodes => DefaultExploding(this);

    protected static bool? DefaultExploding(IRocket c)
    {
      return explodes;
    }

    #endregion

  }
}