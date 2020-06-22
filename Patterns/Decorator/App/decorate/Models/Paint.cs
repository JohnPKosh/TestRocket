
namespace decorate.Models
{
  public class Paint : Decorator
  {
    public override string Apply()
    {
      return string.Format("Giving the {0} a little {1} color!", Location, Color);
    }

    public string Color { get; set; }
  }
}
