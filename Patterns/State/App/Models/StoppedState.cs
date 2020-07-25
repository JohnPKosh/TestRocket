namespace alterstate.Models
{
  /// <summary>
  /// The concrete stopped state that inherits from ProcessState.
  /// </summary>
  public class StoppedState : ProcessState
  {
    public StoppedState(Context context) : base(context) { }

    public override void Pause(Context context)
    {
      context.InvokeStateUnChange(new PausedState(context));
    }
  }

}
