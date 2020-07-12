using System;
using mediate.Models;

namespace mediate
{
  class Program
  {
    static void Main(string[] args)
    {
      // The chat room example exemplifies the traditional GoF mediator pattern.
      RunChatRoom();

      // The Module Controller example exemplifies an additional event driven design.
      RunServiceModules();
    }


    private static void RunChatRoom()
    {
      // Create a chat room and some people who like to babble
      var room = new ChatRoom("Astronaut Lounge");
      var sasha = new Person("Sasha");
      var neil = new Person("Neil");
      var buzz = new Person("Buzz");

      hr();
      con("Sample #1 - Let the chatting begin!");
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
      con("Show the number of persons in room...");
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

    private static void RunServiceModules()
    {
      hr();
      con("Sample #2 - Mediating components in the service module(s) using events.");
      con("*** Not strictly part of the pattern, but a good optional bolt on! ***");
      hr();


      hr();
      con("First we add a controller and attach at least one module. (could be similiar to a chat room with some imagination)");

      ModuleController controller = new ModuleController();
      controller.AttachModule("alimentary residue inhibitor");

      hr();
      con("Then we join (Attach) components to the first module.");

      hr();
      controller.AttachComponentToModule("alimentary residue inhibitor", "containment valve");

      controller.DetachComponentFromModule("alimentary residue inhibitor", "containment valve");

      controller.AttachComponentToModule("alimentary residue inhibitor", "nasalis restrictor");

      hr();
      con("Next we create another module and join (Attach) a component to it.");
      controller.AttachModule("taurus egesta polishing restrictor");

      hr();
      controller.AttachComponentToModule("taurus egesta polishing restrictor", "bs grandiloquence impeder");

      hr();
      con("We also want to connect the other direction from a module to a controller...");
      var randomModule = new ServiceModule("Randomizer");
      randomModule.AttachToController(controller);

      hr();
      con("Then we can add a component directly to the module");

      hr();
      randomModule.AttachComponent("cruft filter");

      hr();
      con("If we try to add a component that already exists somewhere else we can't!");
      con("Perhaps our mediator (ModuleController) is a curmudgeon?");

      hr();
      controller.AttachComponentToModule("Randomizer", "bs grandiloquence impeder");
      hr();
    }

    // Helpers to make things easier to read above.
    private static void hr() => Console.WriteLine("\n**********************************\n");
    private static void con(string text) => Console.WriteLine(text);
  }
}
