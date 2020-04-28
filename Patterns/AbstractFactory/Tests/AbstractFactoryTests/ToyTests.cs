using AbstractFactoryTests.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace AbstractFactoryTests
{
  public class ToyTests : IClassFixture<ToyFixture>
  {
    private readonly ITestOutputHelper output;
    private readonly ToyFixture m_Fix;

    public ToyTests(ITestOutputHelper output, ToyFixture fixture)
    {
      this.output = output;
      m_Fix = fixture;
    }


    [Fact]
    public void Test1()
    {
      output.WriteLine(m_Fix.sut_SpeakExpected);
    }
  }
}
