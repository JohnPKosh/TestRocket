using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace compose.Models.Generic
{
  public class Node<T> : IEnumerable<Node<T>>
  {
    public T Value { get; set; } = default;

    public List<Node<T>> In = new List<Node<T>>(), Out = new List<Node<T>>();

    public IEnumerator<Node<T>> GetEnumerator()
    {
      yield return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }

  public class ChildNodes<T> : Collection<Node<T>>
  {
    public ChildNodes(int count)
    {
      while (count-- > 0)
        Add(new Node<T>());
    }

    public ChildNodes(IEnumerable<T> values)
    {
      foreach (var v in values)
      {
        Add(new Node<T>() { Value = v });
      }
    }
  }

  public static class NodeExtensions
  {
    public static void ConnectTo<T>(this IEnumerable<Node<T>> self, IEnumerable<Node<T>> other)
    {
      if (ReferenceEquals(self, other)) return;

      foreach (var from in self)
        foreach (var to in other)
        {
          from.Out.Add(to);
          to.In.Add(from);
        }
    }
  }

}
