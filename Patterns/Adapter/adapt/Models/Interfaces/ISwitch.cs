namespace adapt.Models.Interfaces
{
  public interface ISwitch
  {
    void FlipSwitch();

    int FlippedCount { get; set; }
  }
}
