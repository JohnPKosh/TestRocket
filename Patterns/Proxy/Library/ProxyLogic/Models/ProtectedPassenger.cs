using ProxyLogic.Enums;
using ProxyLogic.Logic;
using ProxyLogic.Models.Interfaces;

namespace ProxyLogic.Models
{
  public class ProtectedPassenger : IPassenger
  {
    private IPassenger m_Passenger { get; set; } // Here we store the target of our proxy wrapper class.

    public bool IsWearingSpaceSuit { get; set; } // Here we extend and add a proxy property without modifying IPassenger.

    public static ProtectedPassenger Create(PassengerType type, bool isWearingSpaceSuit)
    {
      return new ProtectedPassenger() { m_Passenger = PassengerCreator.Create(type), IsWearingSpaceSuit = isWearingSpaceSuit };
    }

    public string LaunchCommand()
    {
      return IsWearingSpaceSuit ? m_Passenger.LaunchCommand(): "FAILURE TO LAUNCH! The passenger is naked and requires a space suit.";
    }

    public string Speak()
    {
      return IsWearingSpaceSuit ? m_Passenger.Speak(): "It feels a little breezy in here!";
    }
  }
}
