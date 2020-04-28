using System;
using abstractfactory.Models.Interfaces;

namespace abstractfactory.Models
{
  /// <summary>
  /// This is the Toy concrete model implementation of the IPassenger interface.
  /// </summary>
  public class Toy : IToy
  {
    /// <summary>Say some clever phrase here.</summary>
    public string Speak()
    {
      return "Hello I am Buzz";
    }

    /// <summary>Begin launch command instruction.</summary>
    public string LaunchCommand()
    {
      return "To infinity and beyond!";
    }

    /// <summary>Pull his strings.</summary>
    public string PullString()
    {
      return "Help I am unravelling!";
    }
  }

  /// <summary>
  /// This is the Weightless Toy concrete model implementation of the IPassenger interface.
  /// </summary>
  public class WeightlessToy : IToy
  {
    /// <summary>Say some clever phrase here.</summary>
    public string Speak()
    {
      return "Hello I am Buzz";
    }

    /// <summary>Begin launch command instruction.</summary>
    public string LaunchCommand()
    {
      return "To the Stars!";
    }

    /// <summary>Pull his strings.</summary>
    public string PullString()
    {
      return "Hip Hip Hooray!";
    }
  }
}
