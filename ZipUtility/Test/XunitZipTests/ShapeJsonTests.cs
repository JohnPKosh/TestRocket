using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace XunitZipTests
{
  public class ShapeJsonTests
  {
    private readonly ITestOutputHelper output;

    public ShapeJsonTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact(Skip = "We do not need to run this every time")]
    public void CanSerializeJson_True()
    {
      var got = new ShapeDto() {
        Hexagons = SampleShapes.Hexagons.Cast<Hexagon>().ToList(),
        Circles = SampleShapes.Circles.Cast<Circle>().ToList(),
        Stars = SampleShapes.Stars.Cast<Star>().ToList()
      };
      File.WriteAllText(Path.Combine(Environment.CurrentDirectory, TestConstants.SHAPES_FILE_01_PATH), JsonConvert.SerializeObject(got));
      Assert.True(true);
    }

    [Fact]
    public void CanDeserializeJson_True()
    {
      ShapeDto m_NewShapes = JsonConvert.DeserializeObject<ShapeDto>(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, TestConstants.SHAPES_FILE_01_PATH)));
      Assert.True(true);
    }

  }
}
