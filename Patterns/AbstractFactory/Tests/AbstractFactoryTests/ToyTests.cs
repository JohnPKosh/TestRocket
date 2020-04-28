using AbstractFactoryLogic.Enums;
using AbstractFactoryLogic.Models.Interfaces;
using AbstractFactoryTests.Fixtures;
using System;
using Xunit;
using Xunit.Abstractions;

namespace AbstractFactoryTests
{
  public class ToyTests : IClassFixture<ToyFixture>
  {
    private readonly ITestOutputHelper m_Output;
    private readonly ToyFixture m_Fix;

    public ToyTests(ITestOutputHelper output, ToyFixture fixture)
    {
      m_Output = output;
      m_Fix = fixture;
    }


    #region Positive Test Methods

    [Fact]
    public void CanWriteOutput()
    {
      m_Output.WriteLine(m_Fix.sut_SpeakExpected);
    }


    [Theory]
    [InlineData(GravityType.Normal)]
    [InlineData(GravityType.Weightless)]
    public void DoSpeakSamePhrase(GravityType gravity)
    {
      string got;
      switch (gravity)
      {
        case GravityType.Weightless:
          got = m_Fix.sut_ZeroGPassenger.Speak();
          m_Output.WriteLine(got);
          Assert.Equal(m_Fix.sut_SpeakZeroGExpected, got);
          break;
        default:
          got = m_Fix.sut_Passenger.Speak();
          m_Output.WriteLine(got);
          Assert.Equal(m_Fix.sut_SpeakExpected, got);
          break;
      }
    }

    [Theory]
    [InlineData(GravityType.Normal)]
    [InlineData(GravityType.Weightless)]
    public void DoSendSameLaunchCommand(GravityType gravity)
    {
      string got;
      switch (gravity)
      {
        case GravityType.Weightless:
          got = m_Fix.sut_ZeroGPassenger.LaunchCommand();
          m_Output.WriteLine(got);
          Assert.Equal(m_Fix.sut_LaunchZeroGExpected, got);
          break;
        default:
          got = m_Fix.sut_Passenger.LaunchCommand();
          m_Output.WriteLine(got);
          Assert.Equal(m_Fix.sut_LaunchExpected, got);
          break;
      }
    }

    [Theory]
    [InlineData(GravityType.Normal)]
    [InlineData(GravityType.Weightless)]
    public void TheCorrectGravityCreated(GravityType gravity)
    {
      IPassenger got;
      Type expected;
      switch (gravity)
      {
        case GravityType.Weightless:
          got = m_Fix.sut_ZeroGPassenger;
          expected = m_Fix.sut_ZeroGTypeExpected;
          m_Output.WriteLine("expecting: [{0}] and got: [{1}]", expected.Name, got.GetType().Name);
          Assert.IsType(expected, got);
          break;
        default:
          got = m_Fix.sut_Passenger;
          expected = m_Fix.sut_NormalTypeExpected;
          m_Output.WriteLine("expecting: [{0}] and got: [{1}]", expected.Name, got.GetType().Name);
          Assert.IsType(expected, got);
          break;
      }
    }

    #endregion


    #region Inverse Test Methods

    [Theory]
    [InlineData(GravityType.Normal)]
    [InlineData(GravityType.Weightless)]
    public void LaunchCommandsShouldDiffer(GravityType gravity)
    {
      string got;
      string expected;
      switch (gravity)
      {
        case GravityType.Weightless:
          got = m_Fix.sut_Passenger.LaunchCommand();
          expected = m_Fix.sut_LaunchZeroGExpected;
          break;
        default:
          got = m_Fix.sut_ZeroGPassenger.LaunchCommand();
          expected = m_Fix.sut_LaunchExpected;
          break;
      }
      m_Output.WriteLine("expecting: [{0}] and got: [{1}] as expected they should not match!", expected, got);
      Assert.NotEqual(expected, got);
    }


    [Theory]
    [InlineData(GravityType.Normal)]
    [InlineData(GravityType.Weightless)]
    public void SpeechShouldDiffer(GravityType gravity)
    {
      string got;
      string expected;
      switch (gravity)
      {
        case GravityType.Weightless:
          got = m_Fix.sut_Passenger.Speak();
          expected = m_Fix.sut_SpeakZeroGExpected;
          break;
        default:
          got = m_Fix.sut_ZeroGPassenger.Speak();
          expected = m_Fix.sut_SpeakExpected;
          break;
      }
      m_Output.WriteLine("expecting: [{0}] and got: [{1}] as expected they should not match!", expected, got);
      Assert.NotEqual(expected, got);
    }

    #endregion

  }
}
