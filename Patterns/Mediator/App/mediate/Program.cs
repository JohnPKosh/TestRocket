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
      john.MessageRoom("hi room");
      jane.MessageRoom("oh, hey john");
      room.Leave(john);
      var simon = new Person("Simon");
      room.Join(simon);
      simon.MessageRoom("hi everyone!");
      jane.MessagePerson("Simon", "glad you could join us!");
      Console.WriteLine($"{room.PersonCount} people in chat room");

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
