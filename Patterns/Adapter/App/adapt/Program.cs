using System;
using adapt.Models;

namespace adapt
{
  class Program
  {
    static void Main(string[] args)
    {
      /*
        Since this is a fairly simple example we will skip using a seperate logic module, etc...
        See the models folder for implementation details.
      */

      hr();
      con("Prepare to Launch!");
      hr();


      /*
        ** PROBLEM # 1 - I have a switch in the command module that needs to perform like a button.
        Using an adapter I can do so.
      */

      hr();
      con("FLIP the BUTTON 3 times!");  // Apparently ground control is a bit confused. How do you flip a button?
      hr();

      var button = new ButtonPusher(); // I can push a button but I cannot flip it? To avoid disaster, this is where the adapter pattern is used.
      var buttonAdapter = new ButtonToSwitchAdapter(button);

      for (int i = 0; i < 3; i++)
      {
        buttonAdapter.FlipSwitch();
      }
      hr();
      con($"We FLIPPED the Button {buttonAdapter.FlippedCount} times");
      hr();


      /*
        ** PROBLEM # 2 - I have a button in the command module that needs to perform like a switch.
        Using an adapter I can do so.
      */

      hr();
      con("PUSH the SWITCH 5 times!");  // Apparently ground control is a bit confused. How do you push a switch?
      hr();

      var @switch = new SwitchFlipper(); // I can push a button but I cannot flip it? To avoid disaster, this is where the adapter pattern is used.
      var switchAdapter = new SwitchToButtonAdapter(@switch);

      for (int i = 0; i < 5; i++)
      {
        switchAdapter.Push();
      }
      hr();
      con($"We PUSHED the Switch {switchAdapter.PushedCount} times");
      hr();


      con("Press any key to abort mission!");
      Console.ReadKey(true);
    }


    // Helpers to make things easier to read above.
    private static void hr() => Console.WriteLine("\n**********************************\n");
    private static void con(string text) => Console.WriteLine(text);
  }
}
