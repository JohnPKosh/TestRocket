using System;
using SRF.CommandItems;

namespace LogApiTestClient
{
  class Program
  {
    static void Main(string[] args)
    {
      Bootstrap.Run(args, typeof(Commands), "The Log API test client console application");
    }
  }
}
