namespace alterstate.Models
{
  /// <summary>
  /// The concrete started state that inherits from ProcessState.
  /// </summary>
  public class StartedState : ProcessState
  {
    public StartedState(Context context) : base(context) { }
  }

}
