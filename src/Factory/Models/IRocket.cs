namespace Factory.Models
{
  public interface IRocket
  {
    int Height { get; set; }

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
  }
}