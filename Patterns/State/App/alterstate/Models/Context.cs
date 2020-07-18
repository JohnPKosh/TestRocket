using System;

namespace alterstate.Models
{
  /// <summary>
  /// The public context class that will act as a container that will maintain our state.
  /// </summary>
  public class Context
  {
    #region Public Constructor(s)

    /// <summary>
    /// The public default constructor for our context that sets the CurrentState
    /// to Stopped and registers the event args for changed and unchanged.
    /// </summary>
    public Context()
    {
      CurrentState = new StoppedState(this);
      ProcessStateChanged += OnProcessStateChanged;
      ProcessStateUnchanged += OnProcessStateUnChanged;
    }

    #endregion

    #region Fields and Properties

    private ProcessState m_CurrentState; // private backing field for current state below.

    /// <summary> The public property that exposes and sets the private backing field </summary>
    public ProcessState CurrentState { get => m_CurrentState; set => InvokeStateChange(value); }

    /// <summary> The public event handler field for our process changed event </summary>
    public event EventHandler<ProcessStateChangingEventArgs> ProcessStateChanged;

    /// <summary> The public event handler field for our process UNCHAGED event </summary>
    public event EventHandler<ProcessStateChangingEventArgs> ProcessStateUnchanged;

    #endregion

    #region Public Methods (State Strategy Pattern)

    /// <summary>
    /// Public Method to update our current state to Stopped
    /// </summary>
    public virtual void Stop()
    {
      // here we delegate our state change passing our context to the current state
      CurrentState.Stop(this);
    }

    /// <summary>
    /// Public Method to update our current state to Started
    /// </summary>
    public virtual void Start()
    {
      // here we delegate our state change passing our context to the current state
      CurrentState.Start(this);
    }

    /// <summary>
    /// Public Method to update our current state to Paused
    /// </summary>
    public virtual void Pause()
    {
      // here we delegate our state change passing our context to the current state
      CurrentState.Pause(this);
    }

    #endregion

    #region Internal Utility Methods

    /// <summary> An internal utility method to handle changes to CurrentState </summary>
    internal virtual void InvokeStateChange(ProcessState value)
    {
      var args = ProcessStateChangingEventArgs.Create(m_CurrentState, value);
      if (m_CurrentState?.GetType() == value.GetType())
      {
        ProcessStateUnchanged?.Invoke(this, args);
      }
      else
      {
        ProcessStateChanged?.Invoke(this, args);
        m_CurrentState = value;
      }
    }

    /// <summary>
    /// An internal utility method used by ProcessState implementations to
    /// override and short circuit restricted state changes.
    /// </summary>
    /// <remarks> See the StoppedState class </remarks>
    internal virtual void InvokeStateUnChange(ProcessState value)
    {
      var args = ProcessStateChangingEventArgs.Create(m_CurrentState, value);
      ProcessStateUnchanged?.Invoke(this, args);
    }

    #endregion

    #region Protected Virtual Methods for Event Handling

    /// <summary>
    /// The default registered on state changed ProcessStateChangingEventArgs handler
    /// </summary>
    protected virtual void OnProcessStateChanged(object sender, ProcessStateChangingEventArgs e)
    {
      var from = e.FromState?.GetType().Name;
      var to = e.ToState?.GetType().Name;
      Console.WriteLine("Changing state from {0} to {1}", from, to);
    }

    /// <summary>
    /// The default registered on state UNCHAGED ProcessStateChangingEventArgs handler
    /// </summary>
    protected virtual void OnProcessStateUnChanged(object sender, ProcessStateChangingEventArgs e)
    {
      var from = e.FromState?.GetType().Name;
      var to = e.ToState?.GetType().Name;
      Console.WriteLine("UNABLE to change state from {0} to {1}", from, to);
    }

    #endregion
  }

}
