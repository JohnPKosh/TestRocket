namespace adapt.Models.Interfaces
{
  public interface IButton
  {
    void Push();

    int PushedCount { get; set; }
  }
}
