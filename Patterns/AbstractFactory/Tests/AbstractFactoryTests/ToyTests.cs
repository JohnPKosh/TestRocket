using AbstractFactoryLogic.Common;
using AbstractFactoryLogic.Models;
using AbstractFactoryTests.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace AbstractFactoryTests
{
  public class ToyTests : ModelPassengerTests, IClassFixture<ToyFixture>
  {
    public ToyTests(ITestOutputHelper output, ToyFixture fixture)
    {
      m_Output = output;
      m_Fix = fixture;
    }

    /* Since only a Toy can have it's string pulled we want to add a specific test here. Other tests handled in base (ModelPassengerTests) class */

    [Fact]
    public void CanPullToyString()
    {
      Toy got = m_Fix.sut_Passenger as Toy;
      m_Output.WriteLine("expecting: [{0}] and got: [{1}] as expected they should not match!", FactoryConstants.TOY_PULL_STR, got.PullString());
      Assert.Equal(FactoryConstants.TOY_PULL_STR, got.PullString());
    }

    [Fact]
    public void CanPullWeightlessToyString()
    {
      WeightlessToy got = m_Fix.sut_ZeroGPassenger as WeightlessToy;
      m_Output.WriteLine("expecting: [{0}] and got: [{1}] as expected they should not match!", FactoryConstants.TOY_PULL_STR_ZERO_G, got.PullString());
      Assert.Equal(FactoryConstants.TOY_PULL_STR_ZERO_G, got.PullString());
    }
  }
}

