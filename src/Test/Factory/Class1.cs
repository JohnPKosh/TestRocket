using System;

using Azos;
using Azos.Scripting;
using Azos.Apps.Injection;
using Azos.Conf;

namespace RocketFactoryTests
{
  [Runnable]
  public class Class1
  {
    [Run]
    public void TrunWorking()
    {
        Aver.IsTrue(1==1);
    }

  }
}
