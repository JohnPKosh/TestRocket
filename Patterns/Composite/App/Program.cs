using System;
using System.Collections.Generic;
using System.Linq;

using compose.Models.Concrete;
using compose.Logic;

using CompositeLibrary.Infrastructure;

namespace compose
{
  class Program
  {
    static void Main(string[] args)
    {
      // The traditional conceptual example highlighting
      // only scalar (leaf) and composite objects.
      GofLogic.Run();

      // The over the top and perhaps a little insane
      // advanced version utilizing generics and fair
      // bit of abstraction to handle this powerful pattern.
      // Notice how we use recursion to walk our object graph
      // when working with parents, children, and descendants.
      RobotLogic.Run();
    }
  }
}
