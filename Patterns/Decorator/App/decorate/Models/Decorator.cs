namespace decorate.Models
{
  public abstract class Decorator : IDecorate
  {
    public string Location { get; set; }

    public abstract string Apply();
  }
}
