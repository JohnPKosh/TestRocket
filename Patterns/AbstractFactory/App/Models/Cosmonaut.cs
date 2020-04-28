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
    public string Speak()
    {
      return "Sukin syn!";
    }

    /// <summary>Begin launch command instruction.</summary>
    public string LaunchCommand()
    {
      return "Slava stalinu...";
    }

    /// <summary>Flip out cosmonaut.</summary>
    public string FlipSwitch()
    {
      return "pit' bol'she vodki";
    }
  }

  /// <summary>
  /// This is the Weightless Cosmonaut concrete model implementation of the IPassenger interface.
  /// </summary>
  public class WeightlessCosmonaut : ICosmonaut
  {
    /// <summary>Say some clever phrase here.</summary>
    public string Speak()
    {
      return "slava gosudarstvu!";
    }

    /// <summary>Begin launch command instruction.</summary>
    public string LaunchCommand()
    {
      return "do svidaniya tovarishchi";
    }

    /// <summary>Flip out cosmonaut.</summary>
    public string FlipSwitch()
    {
      return "initsiirovaniye vyklyucheniya";
    }
  }


}
