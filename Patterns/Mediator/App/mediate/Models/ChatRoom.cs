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

    private const string LEAVE_MSG = "{0} has left the room!";

    #region Fields and Properties

    private List<Person> m_People { get; set; } = new List<Person>();

    internal int PersonCount { get; set; } = 0;

    #endregion

    #region Public Methods

    /// <summary> Public method that joins a Person to the ChatRoom </summary>
    public void Join(Person person)
    {
      MessageRoom("room", string.Format(JOIN_MSG, person.Name));
      person.Room = this;
      m_People.Add(person);
      PersonCount++;
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
        if (p.Name != from) p.Receive(from, msg);
      }
    }

    /// <summary> Public method to send a message to an individual person </summary>
    public void MessagePerson(string from, string to, string msg)
    {
      m_People.FirstOrDefault(p => p.Name == to)?.Receive(from, msg);
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
    public List<string> ChatLog { get; set; } = new List<string>();

    /// <summary> Public ChatRoom that the Person is Joined to </summary>
    public ChatRoom Room { get; set; }

    #endregion

    #region Public Methods

    /// <summary> Internal recieve message method </summary>
    public void Receive(string sender, string message)
    {
      string s = $"{sender}: '{message}'";
      Console.WriteLine($"[{Name}'s chat session] {s}");
      ChatLog.Add(s);
    }

    /// <summary> Public method to send a message to everyone in the ChatRoom </summary>
    public void MessageRoom(string message)
      => Room.MessageRoom(Name, message);

    /// <summary> Public method to send a message to another Person in the ChatRoom </summary>
    public void MessagePerson(string who, string message)
      => Room.MessagePerson(Name, who, message);

    #endregion
  }
}
