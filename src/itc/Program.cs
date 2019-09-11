using System;

namespace itc
{
  class Program
  {
    static void Main(string[] args)
    {
      new Azos.Platform.Abstraction.NetCore.NetCore20Runtime();
      Erx.Business.Tests.Integ.ProgramBody.Main(args);
    }
  }
}
