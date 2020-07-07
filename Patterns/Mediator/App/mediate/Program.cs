using System;
using mediate.Models;

namespace mediate
{
  class Program
  {
    static void Main(string[] args)
    {
      RunChatRoom();

      RunGame();
    }

    private static void RunChatRoom()
    {
      // Create a chat room and some people who like to babble
      var room = new ChatRoom("Astronaut Lounge");
      var sasha = new Person("Sasha");
      var neil = new Person("Neil");
      var buzz = new Person("Buzz");

      hr();
      con("Let the chatting begin!");
      hr();

      // Add some people to room
      hr();
      con("Sasha and Neil join room...");
      hr();

      con("Sasha Joins...");
      room.Join(sasha);

      con("Neil Joins...");
      room.Join(neil);

      // Start the chatting
      hr();
      con("Sasha and Neil begin banter...");
      hr();

      con("Sasha says hello...");
      sasha.MessageRoom("hello room");

      con("Neil welcomes Sasha...");
      neil.MessageRoom("oh, hey Sasha");

      // Add and remove some persons
      hr();
      con("Sasha leaves and Buzz joins...");
      hr();

      room.Leave(sasha);
      room.Join(buzz);

      // Resume the chatting
      hr();
      con("Buzz and Neil begin banter...");
      hr();

      buzz.MessageRoom("hi everyone!");
      neil.MessagePerson("Buzz", "Glad you could join it was getting lonely in here!");
      buzz.MessagePerson("Neil", "Have you seen Woody?");
      neil.MessagePerson("Buzz", "No");

      // Get a count of persons in the room
      hr();
      con("Show numer of persons in room...");
      hr();

      Console.WriteLine($"{room.PersonCount} people in room");
      hr();

      // Review Neil's chat log
      hr();
      con("Review Neil's Log...");
      hr();
      foreach (var log in neil.ChatLog)
      {
        con(log);
      }

    }

    private static void RunGame()
    {
      hr();
      con("Let the game begin!");
      hr();

      // Create the game and participants
      var game = new Game();
      var player = new Player(game, "Sam");
      var coach = new Coach(game);

      // Raise some events
      player.Score(); // coach says: well done, Sam
      player.Score(); // coach says: well done, Sam
      player.Score(); //
    }


    // Helpers to make things easier to read above.
    private static void hr() => Console.WriteLine("\n**********************************\n");
    private static void con(string text) => Console.WriteLine(text);
  }
}
