namespace alterstate.Models
{
  /// <summary>
  /// The concrete paused state that inherits from ProcessState.
  /// </summary>
  public class PausedState : ProcessState
  {
    public PausedState(Context context) : base(context) { }
  }

}
