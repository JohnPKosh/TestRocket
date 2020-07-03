using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mediate.Models
{
  public class ChatRoom
  {
    private const string JOIN_MSG = "{0} has joined the room!";

    private List<Person> people = new List<Person>();

    public void Broadcast(string source, string message)
    {
      foreach (var p in people)
      {
        if (p.Name != source) p.Receive(source, message);
      }
    }


    public void Join(Person p)
    {
      Broadcast("room", string.Format(JOIN_MSG, p.Name));
      p.Room = this;
      people.Add(p);
    }


    public void Message(string source, string destination, string message)
    {
      people.FirstOrDefault(p => p.Name == destination)
      ?.Receive(source, message);
    }
  }

  public class Person
  {
    public string Name;

    public ChatRoom Room;

    private List<string> chatLog = new List<string>();

    public Person(string name) => Name = name;

    public void Receive(string sender, string message)
    {
      string s = $"{sender}: '{message}'";
      Console.WriteLine($"[{Name}'s chat session] {s}");
      chatLog.Add(s);
    }

    public void Say(string message) => Room.Broadcast(Name, message);

    public void PrivateMessage(string who, string message)
    {
      Room.Message(Name, who, message);
    }
  }
}
