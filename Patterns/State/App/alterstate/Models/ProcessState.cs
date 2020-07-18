namespace alterstate.Models
{
  public abstract class ProcessState
  {
    public ProcessState(Context context)
    {
      m_Context = context;
    }

    protected Context m_Context;

    public virtual void Start(Context context)
    {
      m_Context.CurrentState = new StartedState(context);
    }

    public virtual void Stop(Context context)
    {
      m_Context.CurrentState = new StoppedState(context);
    }

    public virtual void Pause(Context context)
    {
      m_Context.CurrentState = new PausedState(context);
    }

  }

}
