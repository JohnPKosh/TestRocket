namespace alterstate.Models
{
  /// <summary>
  /// The abstact base state control class that provides methods to assign the Context.CurrentState
  /// to one of the inherited concrete ProcessState derived class instances.
  /// </summary>
  public abstract class ProcessState
  {
    public ProcessState(ProcessContext context)
    {
      m_Context = context;
    }

    /// <summary> The target Context that exposes the existing CurrentState that can be updated </summary>
    protected ProcessContext m_Context;


    public virtual void Start(ProcessContext context) => m_Context.CurrentState = new StartedState(context);

    public virtual void Stop(ProcessContext context) => m_Context.CurrentState = new StoppedState(context);

    public virtual void Pause(ProcessContext context) => m_Context.CurrentState = new PausedState(context);
  }


  /// <summary>
  /// The concrete started state that inherits from ProcessState.
  /// </summary>
  public class StartedState : ProcessState
  {
    public StartedState(ProcessContext context) : base(context) { }
  }


  /// <summary>
  /// The concrete stopped state that inherits from ProcessState.
  /// </summary>
  public class StoppedState : ProcessState
  {
    public StoppedState(ProcessContext context) : base(context) { }

    public override void Pause(ProcessContext context)
    {
      context.InvokeStateUnChange(new PausedState(context));
    }
  }


  /// <summary>
  /// The concrete paused state that inherits from ProcessState.
  /// </summary>
  public class PausedState : ProcessState
  {
    public PausedState(ProcessContext context) : base(context) { }
  }

}
