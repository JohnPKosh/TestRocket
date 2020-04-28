using FactoryMethodTests.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace FactoryMethodTests
{
  public class FactoryTests : IClassFixture<FactoryFixture>
  {
    private readonly ITestOutputHelper m_Output;
    private readonly FactoryFixture m_Fix;

    public FactoryTests(ITestOutputHelper output, FactoryFixture fixture)
    {
      m_Output = output;
      m_Fix = fixture;
    }

    #region Positive Test Methods

    [Fact]
    public void CanCreateToy()
    {
      // Arrange
      var expected = m_Fix.sut_ExpectedToy.GetType();

      // Act
      var got = m_Fix.sut_ToyFactory.NewPassenger();
      m_Output.WriteLine("expecting: [{0}] and got: [{1}]", expected.Name, got.GetType().Name);

      // Assert
      Assert.IsType(expected, got);
    }

    [Fact]
    public void CanCreateAstronaut()
    {
      // Arrange
      var expected = m_Fix.sut_ExpectedAstronaut.GetType();

      // Act
      var got = m_Fix.sut_AstronautFactory.NewPassenger();
      m_Output.WriteLine("expecting: [{0}] and got: [{1}]", expected.Name, got.GetType().Name);

      // Assert
      Assert.IsType(expected, got);
    }

    [Fact]
    public void CanCreateCosmonaut()
    {
      // Arrange
      var expected = m_Fix.sut_ExpectedCosmonaut.GetType();

      // Act
      var got = m_Fix.sut_CosmonautFactory.NewPassenger();
      m_Output.WriteLine("expecting: [{0}] and got: [{1}]", expected.Name, got.GetType().Name);

      // Assert
      Assert.IsType(expected, got);
    }

    #endregion

    #region Inverse Test Methods

    [Fact]
    public void AnAstronautIsNotAToy()
    {
      // Arrange
      var expected = m_Fix.sut_ExpectedAstronaut.GetType();

      // Act
      var got = m_Fix.sut_ToyFactory.NewPassenger();
      m_Output.WriteLine("expecting: [{0}] and got: [{1}] as expected they should not match!", expected.Name, got.GetType().Name);

      // Assert
      Assert.IsNotType(expected, got);
    }


    [Fact]
    public void DifferentLaunchCommandsAreExpected()
    {
      // Arrange
      var expected = m_Fix.sut_ExpectedAstronaut.LaunchCommand();

      // Act
      var got = m_Fix.sut_CosmonautFactory.NewPassenger().LaunchCommand();
      m_Output.WriteLine("expecting: [{0}] and got: [{1}] as expected they should not match!", expected, got);

      // Assert
      Assert.NotEqual(expected, got);
    }


    [Fact]
    public void DifferentSpeechExpected()
    {
      // Arrange
      var expected = m_Fix.sut_ExpectedAstronaut.Speak();

      // Act
      var got = m_Fix.sut_CosmonautFactory.NewPassenger().Speak();
      m_Output.WriteLine("expecting: [{0}] and got: [{1}] as expected they should not match!", expected, got);

      // Assert
      Assert.NotEqual(expected, got);
    }

    #endregion
  }
}
