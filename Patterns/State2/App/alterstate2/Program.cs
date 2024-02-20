using System;

using alterstate.Models;

namespace alterstate
{
  class Program
  {
    static void Main(string[] args)
    {
      hr();
      con("Countdown has started!");
      hr();

      con("Expected Launch sequence: ");
      con("StoppedState > PausedState > StartedState > StartedState > PausedState > PausedState > StoppedState");
      hr();

      // The count down is initially stopped
      var countDown = new ProcessContext();
      hr();
      con("The countdown is initially in the [{0}]!", countDown.CurrentState.GetType().Name);
      hr();

      /*
        Since it makes more sense we added event handling for our base Context class.
        This is really not a formal part of the state design pattern but it goes a
        long way to add simple extensible funtionality. The state changes below will
        write to the console as defined in our logic on each event.
      */

      // The count down is already in stopped state
      countDown.Stop();
      hr();

      // The count down is already in stopped state, pausing will do nothing
      countDown.Pause();
      hr();

      //Making the count down start
      countDown.Start();
      hr();

      // The count down is already started, starting again will do nothing
      countDown.Start();
      hr();

      // Putting the count down in paused state
      countDown.Pause();
      hr();

      // The count down is already paused, pausing will do nothing
      countDown.Pause();
      hr();

      // Stopping the count down
      countDown.Stop();
      hr();

      con("Failure to Launch!");
    }


    // Helpers to make things easier to read above.
    private static void hr() => Console.WriteLine("\n**********************************\n");
    private static void con(string text) => Console.WriteLine(text);
    private static void con(string text, params object[] args) => Console.WriteLine(text, args);
  }
}
