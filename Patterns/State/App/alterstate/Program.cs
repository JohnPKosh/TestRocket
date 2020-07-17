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

      con("Launch sequence: ");
      con("Stopped > Paused > Started > Started > Paused > Paused > Stopped");
      hr();

      // The count down is initially stopped
      var countDown = new ProcessContext();

      //Console.WriteLine(countDown.CurrentState.GetType().Name);
      //countDown.CurrentState = new Started(countDown);
      //Console.WriteLine(countDown.CurrentState.GetType().Name);
      //countDown.CurrentState = new Stopped(countDown);
      //Console.WriteLine(countDown.CurrentState.GetType().Name);

      // The count down is already in stopped state
      countDown.Stop();
      // The count down is already in stopped state, pausing will do nothing
      countDown.Pause();
      //Making the count down start
      countDown.Start();
      // The count down is already started, starting again will do nothing
      countDown.Start();
      // Putting the count down in paused state
      countDown.Pause();
      // The count down is already paused, pausing will do nothing
      countDown.Pause();
      // Stopping the count down
      countDown.Stop();

    }


    // Helpers to make things easier to read above.
    private static void hr() => Console.WriteLine("\n**********************************\n");
    private static void con(string text) => Console.WriteLine(text);
    private static void con(string text, params object[] args) => Console.WriteLine(text, args);
  }
}
