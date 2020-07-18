using System;

namespace alterstate.Models
{
  public class ProcessStateChangingEventArgs : EventArgs
  {
    public static ProcessStateChangingEventArgs Create(ProcessState fromState, ProcessState ToState)
      => new ProcessStateChangingEventArgs() { FromState = fromState, ToState = ToState };

    public ProcessState FromState { get; set; }
    public ProcessState ToState { get; set; }
  }

}
