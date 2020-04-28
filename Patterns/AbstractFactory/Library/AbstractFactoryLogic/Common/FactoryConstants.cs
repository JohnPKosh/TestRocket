namespace AbstractFactoryLogic.Common
{
  /// <summary>
  /// Contains Factory Method specific constants for IPassenger Speak() and LaunchCommand() strings etc.
  /// </summary>
  public static class FactoryConstants
  {
    /* Toy passengers can speak, send launch command, and PULL STRING. They do not PUSH BUTTON or FLIP SWITCH! */

    public const string TOY_SPK = "Hello I am Buzz";

    public const string TOY_LAUNCH = "To infinity and beyond!";

    public const string TOY_PULL_STR = "Help I am unravelling!";

    public const string TOY_SPK_ZERO_G = "Woody, I am upside down?";

    public const string TOY_LAUNCH_ZERO_G = "To the Stars!";

    public const string TOY_PULL_STR_ZERO_G = "Hip Hip Hooray!";

    /* Astronaut passengers can speak, send launch command, and PUSH BUTTON. They do not PULL STRING or FLIP SWITCH! */

    public const string AST_SPK = "Where is my Tang?";

    public const string AST_LAUNCH = "Git-r-Done!";

    public const string AST_PUSH_BTN = "Why did you push that?";

    public const string AST_SPK_ZERO_G = "Look at me I am floating!";

    public const string AST_LAUNCH_ZERO_G = "Up up and away!";

    public const string AST_PUSH_BTN_ZERO_G = "Look at the blinky lights!";

    /* Cosmoonaut passengers can speak, send launch command, and FLIP SWITCH. They do not PULL STRING or PUSH BUTTON! */

    public const string CSM_SPK = "Sukin syn!";

    public const string CSM_LAUNCH = "Slava stalinu...";

    public const string CSM_FLIP = "pit' bol'she vodki";

    public const string CSM_SPK_ZERO_G = "slava gosudarstvu!";

    public const string CSM_LAUNCH_ZERO_G = "do svidaniya tovarishchi";

    public const string CSM_FLIP_ZERO_G = "initsiirovaniye vyklyucheniya";
  }
}
