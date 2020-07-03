using ProxyLogic.Enums;
using ProxyLogic.Logic;
using ProxyLogic.Models.Interfaces;

namespace ProxyLogic.Models
{
  /// <summary>
  /// A concrete proxy ProtectedPassenger class that implements IPassenger and
  /// passes through the logic to the proxied logic.
  /// </summary>
  public class ProtectedPassenger : IPassenger
  {
    /// <summary>
    /// The property to hold our interface used for passenger models functionality
    /// </summary>
    /// <remarks> Here we store the target of our proxy wrapper class when calling Create method below. </remarks>
    private IPassenger m_Passenger { get; set; }

    /// <summary> The property indicating if the passenger is naked or wearing a space suit </summary>
    /// <remarks> Here we extend and add a proxy property without modifying IPassenger </remarks>
    public bool IsWearingSpaceSuit { get; set; }

    /// <summary>
    /// The public static factory method accepting a PassengerType and bool (additional proxy logic)
    /// </summary>
    public static ProtectedPassenger Create(PassengerType type, bool isWearingSpaceSuit)
    {
      return new ProtectedPassenger() { m_Passenger = PassengerCreator.Create(type), IsWearingSpaceSuit = isWearingSpaceSuit };
    }

    /// <summary> Begin launch command instruction. </summary>
    /// <remarks> Here we extend and add additional method logic without modifying the underlying IPassenger </remarks>
    public string LaunchCommand()
    {
      return IsWearingSpaceSuit ? m_Passenger.LaunchCommand(): "FAILURE TO LAUNCH! The passenger is naked and requires a space suit.";
    }

    /// <summary> Say some clever phrase here. </summary>
    /// <remarks> Here we extend and add additional method logic without modifying the underlying IPassenger </remarks>
    public string Speak()
    {
      return IsWearingSpaceSuit ? m_Passenger.Speak(): "It feels a little breezy in here!";
    }
  }
}
