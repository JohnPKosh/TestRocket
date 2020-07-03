using System;
using mediate.Models;

namespace mediate
{
  class Program
  {
    static void Main(string[] args)
    {
      // ChatRoom sample
      var room = new ChatRoom();
      var john = new Person("John");
      var jane = new Person("Jane");
      room.Join(john);
      room.Join(jane);
      john.Say("hi room");
      jane.Say("oh, hey john");
      var simon = new Person("Simon");
      room.Join(simon);
      simon.Say("hi everyone!");
      jane.PrivateMessage("Simon", "glad you could join us!");

      // Game sample
      var game = new Game();
      var player = new Player(game, "Sam");
      var coach = new Coach(game);
      player.Score(); // coach says: well done, Sam
      player.Score(); // coach says: well done, Sam
      player.Score(); //
    }
  }
}
