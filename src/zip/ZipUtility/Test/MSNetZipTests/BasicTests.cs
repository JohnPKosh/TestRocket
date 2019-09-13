using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSNetZipTests
{
  [TestClass]
  public class BasicTests
  {
    [TestMethod]
    public void CanRun_MSNetTest_True()
    {
      Assert.IsTrue(true); /* Sanity check to make sure test framework is working */
    }
  }
}
