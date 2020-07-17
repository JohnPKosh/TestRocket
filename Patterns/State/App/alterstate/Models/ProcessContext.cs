using System;
using System.Collections.Generic;
using System.Text;

namespace alterstate.Models
{

  public interface IProcessState
  {
    void Start(ProcessContext context);

    void Stop(ProcessContext context);

    void Pause(ProcessContext context);

    // TODO: consider Disable and Enable
  }


  public class Stopped : IProcessState
  {
    ProcessContext m_Context;

    public Stopped(ProcessContext context)
    {
      Console.WriteLine("ProcessContext is Stopped now.");
      m_Context = context;
    }

    public void Start(ProcessContext context)
    {
      Console.WriteLine("Going from Stopped state to Started state");
      m_Context.CurrentState = new Started(context);
    }


    public void Stop(ProcessContext context)
    {
      Console.WriteLine("ProcessContext is already in Stopped state");
    }

    public void Pause(ProcessContext context)
    {
      Console.WriteLine("ProcessContext is already in Stopped state, so Paused state is not possible.");
    }
  }

  public class Started : IProcessState
  {
    ProcessContext m_Context;
    public Started(ProcessContext context)
    {
      Console.WriteLine("ProcessContext is Started now.");
      m_Context = context;
    }

    public void Start(ProcessContext context)
    {
      Console.WriteLine("ProcessContext is already in Started state.");
    }

    public void Stop(ProcessContext context)
    {
      Console.WriteLine("Going from Started state to Stopped state.");
      m_Context.CurrentState = new Stopped(context);
    }

    public void Pause(ProcessContext context)
    {
      Console.WriteLine("Going from Started state to Paused state.");
      m_Context.CurrentState = new Paused(context);
    }
  }

  public class Paused : IProcessState
  {
    ProcessContext m_Context;
    public Paused(ProcessContext context)
    {
      Console.WriteLine("ProcessContext is in Paused state now.");
      m_Context = context;
    }

    public void Start(ProcessContext context)
    {
      Console.WriteLine("Going from Paused state to Started state.");
      m_Context.CurrentState = new Started(context);
    }

    public void Stop(ProcessContext context)
    {
      Console.WriteLine("Going from Paused state to Stopped state.");
      m_Context.CurrentState = new Stopped(context);
    }

    public void Pause(ProcessContext context)
    {
      Console.WriteLine("ProcessContext is already in Paused state.");
    }
  }

  public class ProcessContext
  {
    public IProcessState CurrentState { get; set; }

    public ProcessContext()
    {
      CurrentState = new Stopped(this);
    }

    public void Stop()
    {
      // Delegating the state
      CurrentState.Stop(this);
    }

    public void Start()
    {
      // Delegating the state
      CurrentState.Start(this);
    }

    public void Pause()
    {
      // Delegating the state
      CurrentState.Pause(this);
    }
  }

}
