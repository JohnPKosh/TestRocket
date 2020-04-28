using AbstractFactoryLogic.Models.Interfaces;
using System;

namespace AbstractFactoryTests.Fixtures
{
  public interface IModelFixture
  {
    string sut_LaunchExpected { get; }
    string sut_LaunchZeroGExpected { get; }
    Type sut_NormalTypeExpected { get; }
    IPassenger sut_Passenger { get; set; }
    string sut_SpeakExpected { get; }
    string sut_SpeakZeroGExpected { get; }
    IPassenger sut_ZeroGPassenger { get; set; }
    Type sut_ZeroGTypeExpected { get; }

    void Dispose();
  }
}