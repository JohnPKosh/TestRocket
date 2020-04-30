using AbstractFactoryLogic.Common;
using AbstractFactoryLogic.Models;
using AbstractFactoryTests.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace AbstractFactoryTests
{
  public class CosmonautTests : ModelPassengerTests, IClassFixture<CosmonautFixture>
  {
    public CosmonautTests(ITestOutputHelper output, CosmonautFixture fixture)
    {
      m_Output = output;
      m_Fix = fixture;
    }

    /* Since only a Cosmonaut can flip a switch we want to add specific test here. Other tests handled in base (ModelPassengerTests) class */

    [Fact]
    public void CosmonautCanFlipSwitch()
    {
      WeightlessCosmonaut got = m_Fix.sut_ZeroGPassenger as WeightlessCosmonaut;
      m_Output.WriteLine("expecting: [{0}] and got: [{1}] as expected they should not match!", FactoryConstants.CSM_FLIP_ZERO_G, got.FlipSwitch());
      Assert.Equal(FactoryConstants.CSM_FLIP_ZERO_G, got.FlipSwitch());
    }
  }
}
