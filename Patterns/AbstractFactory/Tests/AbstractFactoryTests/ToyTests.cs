using AbstractFactoryTests.Fixtures;
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


    [Fact]
    public void CanWriteOutput()
    {
      m_Output.WriteLine(m_Fix.sut_SpeakExpected);
    }
  }
}
