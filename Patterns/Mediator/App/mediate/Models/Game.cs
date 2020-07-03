using System;
using System.Collections.Generic;
using System.Text;

namespace mediate.Models
{
  public abstract class GameEventArgs : EventArgs
  {
    public abstract void Print();
  }

  public class Game
  {
    public event EventHandler<GameEventArgs> Events;
    public void Fire(GameEventArgs args)
    {
      Events?.Invoke(this, args);
    }
  }

  public class PlayerScoredEventArgs : GameEventArgs
  {
    public string PlayerName;

    public int GoalsScoredSoFar;

    public PlayerScoredEventArgs(string playerName, int goalsScoredSoFar)
    {
      PlayerName = playerName;
      GoalsScoredSoFar = goalsScoredSoFar;
    }

    public override void Print()
    {
      Console.WriteLine($"{PlayerName} has scored!" +
      $"(their {GoalsScoredSoFar} goal)");
    }
  }

  public class Player
  {
    private string name;

    private int goalsScored = 0;

    private Game game;

    public Player(Game game, string name)
    {
      this.name = name;
      this.game = game;
    }

    public void Score()
    {
      goalsScored++;
      var args = new PlayerScoredEventArgs(name, goalsScored);
      game.Fire(args);
    }
  }

  public class Coach
  {
    private Game game;

    public Coach(Game game)
    {
      this.game = game;
      // celebrate if player has scored <3 goals
      game.Events += (sender, args) =>
      {
        if (args is PlayerScoredEventArgs scored && scored.GoalsScoredSoFar < 3)
        {
          Console.WriteLine($"coach says: well done, {scored.PlayerName}");
        }
      };
    }
  }

}
