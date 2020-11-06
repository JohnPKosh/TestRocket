using System;
using TestConsole.Logic;

namespace TestConsole
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Starting!");
      var nrows = 1000;
      if(args != null && args.Length> 0) nrows = int.Parse(args[0]);
      var tester = new TestLogic();
      tester.CanQueryParallelNTimes(nrows);
    }
  }
}
