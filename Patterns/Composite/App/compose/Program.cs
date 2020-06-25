using System;
using System.Collections.Generic;
using compose.Models;

namespace compose
{
  class Program
  {
    static void Main(string[] args)
    {

      var dad = new Node<string>
      {
        Value = "John"
      };

      var mom = new Node<string>
      {
        Value = "Wendy"
      };

      dad.ConnectTo(mom);

      //var children = new NodeLayer<string>(2);
      //mom.ConnectTo(children);

      var childNames = new List<string>() { "Parker", "Sierra" };
      var children = new ChildNodes<string>(childNames);
      mom.ConnectTo(children);

      Console.WriteLine("In: {0} / Out {1}", mom.In.Count, mom.Out.Count);

      Console.WriteLine("Hello World!");
    }
  }
}
