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
    public void Speak()
    {
      Console.WriteLine("Hello I am Buzz");
    }

    /// <summary>Begin launch command instruction.</summary>
    public void LaunchCommand()
    {
      Console.WriteLine("To infinity and beyond!");
    }

    /// <summary>Pull his strings.</summary>
    public void PullString()
    {
      Console.WriteLine("Help I am unravelling!");
    }
  }

  /// <summary>
  /// This is the Weightless Toy concrete model implementation of the IPassenger interface.
  /// </summary>
  public class WeightlessToy : IToy
  {
    /// <summary>Say some clever phrase here.</summary>
    public void Speak()
    {
      Console.WriteLine("Hello I am Buzz");
    }

    /// <summary>Begin launch command instruction.</summary>
    public void LaunchCommand()
    {
      Console.WriteLine("To the Stars!");
    }

    /// <summary>Pull his strings.</summary>
    public void PullString()
    {
      Console.WriteLine("Hip Hip Hooray!");
    }
  }
}
