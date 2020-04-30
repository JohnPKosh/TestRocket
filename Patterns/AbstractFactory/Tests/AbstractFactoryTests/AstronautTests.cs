using AbstractFactoryLogic.Common;
using AbstractFactoryLogic.Models;
using AbstractFactoryTests.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace AbstractFactoryTests
{
  public class AstronautTests : ModelPassengerTests, IClassFixture<AstronautFixture>
  {
    public AstronautTests(ITestOutputHelper output, AstronautFixture fixture)
    {
      m_Output = output;
      m_Fix = fixture;
    }

    /* Since only an Astronaut can push a button we want to add a specific test here. Other tests handled in base (ModelPassengerTests) class */

    [Fact]
    public void AstronautCanPushButton()
    {
      Astronaut got = m_Fix.sut_Passenger as Astronaut;
      m_Output.WriteLine("expecting: [{0}] and got: [{1}] as expected they should not match!", FactoryConstants.AST_PUSH_BTN, got.PushButton());
      Assert.Equal(FactoryConstants.AST_PUSH_BTN, got.PushButton());
    }
  }
}
