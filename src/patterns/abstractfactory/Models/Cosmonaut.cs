using System;
using abstractfactory.Models.Interfaces;

namespace abstractfactory.Models
{
  /// <summary>
  /// This is the Cosmonaut concrete model implementation of the IPassenger interface.
  /// </summary>
  public class Cosmonaut : ICosmonaut
  {
    /// <summary>Say some clever phrase here.</summary>
    public void Speak()
    {
      Console.WriteLine("Sukin syn!");
    }

    /// <summary>Begin launch command instruction.</summary>
    public void LaunchCommand()
    {
      Console.WriteLine("Slava stalinu...");
    }

    public void FlipSwitch()
    {
      Console.WriteLine("pit' bol'she vodki");
    }
  }

  /// <summary>
  /// This is the Weightless Cosmonaut concrete model implementation of the IPassenger interface.
  /// </summary>
  public class WeightlessCosmonaut : ICosmonaut
  {
    /// <summary>Say some clever phrase here.</summary>
    public void Speak()
    {
      Console.WriteLine("slava gosudarstvu!");
    }

    /// <summary>Begin launch command instruction.</summary>
    public void LaunchCommand()
    {
      Console.WriteLine("do svidaniya tovarishchi");
    }

    /// <summary>Flip out cosmonaut.</summary>
    public void FlipSwitch()
    {
      Console.WriteLine("initsiirovaniye vyklyucheniya");
    }
  }


}
