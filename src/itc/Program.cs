using System;

namespace itc
{
  class Program
  {
    static void Main(string[] args)
    {
      new Azos.Platform.Abstraction.NetCore.NetCore20Runtime();
      RocketFactoryTests.ProgramBody.Main(args);
    }
  }
}
