using System;
using System.Collections.Generic;
using System.Linq;

namespace mediate.Models
{
  /// <summary>
  /// Public class encapsulating the ChatRoom logic.
  /// </summary>
  public class ChatRoom
  {
    private const string JOIN_MSG = "{0} has joined the room!";
    private const string WELCOME_MSG = "Welcome {0} to the {1}";
    private const string LEAVE_MSG = "{0} has left the room!";

    #region Class Constructor and Initialization

    /// <summary> The default constructor taking a room's name </summary>
    public ChatRoom(string name) => RoomName = name;

    #endregion

    #region Fields and Properties

    private List<Person> m_People { get; set; } = new List<Person>();

    /// <summary> An internal property to track the number of persons in a room </summary>
    internal int PersonCount { get; set; } = 0;

    /// <summary> The public room name property </summary>
    public string RoomName { get; private set; }

    #endregion

    #region Public Methods

    /// <summary> Public method that joins a Person to the ChatRoom </summary>
    public void Join(Person person)
    {
      person.Room = this;
      m_People.Add(person);
      PersonCount++;
      MessageRoom("ROOM", string.Format(JOIN_MSG, person.Name));
      MessagePerson("ROOM", person.Name, string.Format(WELCOME_MSG, person.Name, RoomName));
    }

    public void Leave(Person person)
    {
      m_People.Remove(person);
      person.Room = null;
      PersonCount--;
      MessageRoom("room", string.Format(LEAVE_MSG, person.Name));
    }

    /// <summary> Public method to send a broadcast message to all people in the group </summary>
    public void MessageRoom(string from, string msg)
    {
      foreach (var p in m_People)
      {
        if (p.Name != from) p.ReceiveMessage(from, msg);
      }
    }

    /// <summary> Public method to send a message to an individual person </summary>
    public void MessagePerson(string from, string to, string msg)
    {
      m_People.FirstOrDefault(p => p.Name == to)?.ReceiveMessage(from, msg);
    }

    #endregion
  }

  /// <summary>
  /// Public class to represent an individual Person that can enter a ChatRoom.
  /// </summary>
  public class Person
  {
    #region Class Constructor and Initialization

    /// <summary> The default constructor taking a person's name </summary>
    public Person(string name) => Name = name;

    #endregion

    #region Fields and Properties

    /// <summary> Public Name property of the Person who can join a ChatRoom </summary>
    public string Name { get; private set; }

    /// <summary> The person's chat log </summary>
    internal List<string> ChatLog { get; set; } = new List<string>();

    /// <summary> Public ChatRoom that the Person is Joined to </summary>
    public ChatRoom Room { get; set; }

    #endregion

    #region Public Methods

    /// <summary> Internal recieve message method </summary>
    public void ReceiveMessage(string from, string message)
    {
      string s = $"From {from}: '{message}'";
      Console.WriteLine($"({Name}'s chat session) =  {s}");
      ChatLog.Add(s);
    }

    /// <summary> Public method to send a message to everyone in the ChatRoom </summary>
    public void MessageRoom(string message)
    {
      Room.MessageRoom(Name, message);
      ChatLog.Add($"From (ME) to Room: '{message}'");
    }

    /// <summary> Public method to send a message to another Person in the ChatRoom </summary>
    public void MessagePerson(string to, string message)
    {
      Room.MessagePerson(Name, to, message);
      ChatLog.Add($"From (ME) to {to}: '{message}'");
    }

    #endregion
  }
}
