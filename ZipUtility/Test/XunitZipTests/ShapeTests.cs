using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace XunitZipTests
{
  public class ShapeTests
  {
    private readonly ITestOutputHelper output;

    public ShapeTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public void CanGetShapes_True()
    {
      var got = SampleShapes.Shapes;
      Assert.NotEmpty(got);
    }

    [Fact]
    public void CanCountAllShapes_True()
    {
      var got = SampleShapes.Shapes;
      output.WriteLine(got?.Count().ToString());
      Assert.True(got.Count() == 50);
    }

    [Theory]
    [InlineData(typeof(Hexagon), 20)]
    [InlineData(typeof(Star), 10)]
    [InlineData(typeof(Circle), 20)]
    [InlineData(typeof(Shape), 50)]
    public void CanCountShapes_True(Type shapeType, int count)
    {
      IEnumerable<Shape> got;
      switch (shapeType.Name)
      {
        case "Hexagon":
          got = SampleShapes.Hexagons;
          break;
        case "Star":
          got = SampleShapes.Stars;
          break;
        case "Circle":
          got = SampleShapes.Circles;
          break;
        default:
          got = SampleShapes.Shapes;
          break;
      }
      output.WriteLine(got?.Count().ToString() + shapeType.Name);
      Assert.True(got.Count() == count);
    }

    [Theory]
    [InlineData(ColorType.Blue, 10)]
    [InlineData(ColorType.Grey, 20)]
    [InlineData(ColorType.Orange, 5)]
    [InlineData(ColorType.Red, 15)]
    public void CanCountColors_True(ColorType color, int count)
    {
      var got = SampleShapes.Shapes.Where(x => x.Color == color);
      output.WriteLine(got?.Count().ToString() + color);
      Assert.True(got.Count() == count);
    }

    [Theory]
    [InlineData(typeof(Hexagon), ColorType.Blue, 5)]
    [InlineData(typeof(Hexagon), ColorType.Grey, 5)]
    [InlineData(typeof(Hexagon), ColorType.Orange, 5)]
    [InlineData(typeof(Hexagon), ColorType.Red, 5)]
    [InlineData(typeof(Star), ColorType.Blue, 5)]
    [InlineData(typeof(Star), ColorType.Grey, 5)]
    [InlineData(typeof(Circle), ColorType.Grey, 10)]
    [InlineData(typeof(Circle), ColorType.Red, 10)]
    [InlineData(typeof(Shape), ColorType.Blue, 10)]
    [InlineData(typeof(Shape), ColorType.Grey, 20)]
    [InlineData(typeof(Shape), ColorType.Orange, 5)]
    [InlineData(typeof(Shape), ColorType.Red, 15)]
    public void CanCountShapesAndColors_True(Type shapeType, ColorType color, int count)
    {
      IEnumerable<Shape> got;
      switch (shapeType.Name)
      {
        case "Hexagon":
          got = SampleShapes.Hexagons.Where(x => x.Color == color);
          break;
        case "Star":
          got = SampleShapes.Stars.Where(x => x.Color == color);
          break;
        case "Circle":
          got = SampleShapes.Circles.Where(x => x.Color == color);
          break;
        default:
          got = SampleShapes.Shapes.Where(x => x.Color == color);
          break;
      }
      output.WriteLine(got?.Count().ToString() + shapeType.Name);
      Assert.True(got.Count() == count);
    }

    [Fact]
    public void CanCountShapesIndividually_True()
    {
      var m_HexCnt = 0;
      var m_StarCnt = 0;
      var m_CircleCnt = 0;
      foreach (var s in SampleShapes.Shapes)
      {
        if (s is Hexagon) m_HexCnt++;
        if (s is Star) m_StarCnt++;
        if (s is Circle) m_CircleCnt++;
      }
      output.WriteLine($"Hexagons: {m_HexCnt} Stars: {m_StarCnt} Circles: {m_CircleCnt}");
      Assert.True(m_HexCnt == 20);
      Assert.True(m_StarCnt == 10);
      Assert.True(m_CircleCnt == 20);
    }

    [Theory]
    [InlineData(100)]
    [InlineData(1000)]
    [InlineData(10000)]
    [InlineData(100000)]
    [InlineData(1000000)]
    public void CanCountColorsOverAndOver_True(int count)
    {
      var m_Blue = 0;
      var m_Grey = 0;
      var m_Orange = 0;
      var m_Red = 0;
      for (int i = 0; i < count; i++)
      {
        m_Blue += SampleShapes.Shapes.Count(x => x.Color == ColorType.Blue);
        m_Grey += SampleShapes.Shapes.Count(x => x.Color == ColorType.Grey);
        m_Orange += SampleShapes.Shapes.Count(x => x.Color == ColorType.Orange);
        m_Red += SampleShapes.Shapes.Count(x => x.Color == ColorType.Red);
      }
      output.WriteLine($"Blue Shapes: {m_Blue.ToString("N0")} Grey Shapes: {m_Grey.ToString("N0")} Orange Shapes: {m_Orange.ToString("N0")} Red Shapes: {m_Red.ToString("N0")}");
      Assert.True((m_Blue + m_Grey + m_Orange + m_Red) > 0);
    }

  }
}
