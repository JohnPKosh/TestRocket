using System;
using ProxyLogic.Enums;
using ProxyLogic.Models;
using ProxyLogic.Models.Interfaces;

namespace proxy
{
  class Program
  {
    static void Main(string[] args)
    {
      /*
        We are reusing the PassengerCreator Factory pattern again but with a new twist.
        Instead of calling PassengerCreator.Create(type); in our private prepare for launch
        method below we just moved that logic into our proxy ProtectedPassenger class.
        Notice that we added an additional parameter that does not modify any of our
        existing Factory logic.
      */

      hr();
      con("Prepare to Launch!");
      hr();

      prepareForLaunch(PassengerType.Astronaut, true);
      prepareForLaunch(PassengerType.Cosmonaut, true);
      prepareForLaunch(PassengerType.Toy, false);
      prepareForLaunch(PassengerType.RemoteControlToy, true);

      /*
        The point of this sample is to see how we can use a proxy to not violate the
        Single Responsibility principle or changing our existing code to create a
        Protection proxy.
      */

      con("Press any key to abort mission!");
      Console.ReadKey(true);
    }

    private static void prepareForLaunch(PassengerType type, bool isWearingSpaceSuit)
    {
      IPassenger passenger = ProtectedPassenger.Create(type, isWearingSpaceSuit); // Notice ProtectedPassenger also implements IPassenger.
      con(passenger.Speak());
      con(passenger.LaunchCommand());
      hr();
    }

    // Helpers to make things easier to read above.
    private static void hr() => Console.WriteLine("\n**********************************\n");
    private static void con(string text) => Console.WriteLine(text);

  }
}
