using FactoryMethodTests.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace FactoryMethodTests
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


    [Fact]
    public void CanWriteOutput()
    {
      m_Output.WriteLine(m_Fix.sut_SpeakExpected);
    }

    [Fact]
    public void DoSpeakSamePhrase()
    {
      var got = m_Fix.sut_Passenger.Speak();
      m_Output.WriteLine(got);
      Assert.Equal(m_Fix.sut_SpeakExpected, got);
    }

    [Fact]
    public void DoSendSameLaunchCommand()
    {
      var got = m_Fix.sut_Passenger.LaunchCommand();
      m_Output.WriteLine(got);
      Assert.Equal(m_Fix.sut_LaunchExpected, got);
    }
  }
}
