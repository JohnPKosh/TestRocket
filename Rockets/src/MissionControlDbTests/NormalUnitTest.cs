using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MissionControlDbTests
{
  [TestClass]
  public class NormalUnitTest
  {
    [TestMethod]
    public void CanExecuteTestThatDoesNothing()
    {
      /* This test does nothing. Had this been an actual test the following code would have been followed with an actual test! */
      Assert.IsTrue(true);

      /* Some additional MSTest methods are shown below:

      AreEqual
      AreEqual<T>
      AreNotEqual
      AreNotSame
      AreSame
      Equals
      Fail
      Inconclusive
      IsFalse
      IsInstanceOfType
      IsNotInstanceOfType
      IsNotNull
      IsNull
      IsTrue
      */
    }
  }
}
