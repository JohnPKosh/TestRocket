using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace XunitZipTests
{
  public sealed class SingletonShapes
  {
    private static readonly Lazy<SingletonShapes>lazy = new Lazy<SingletonShapes>(() => new SingletonShapes());

    public static SingletonShapes Instance { get { return lazy.Value; } }

    private Shapes m_Shapes = new Shapes();

    public IEnumerable<Shape> Shapes => m_Shapes;
    public IEnumerable<Shape> Hexagons => m_Shapes.Where(x => x.GetType() == typeof(Hexagon));
    public IEnumerable<Shape> Stars => m_Shapes.Where(x => x.GetType() == typeof(Star));
    public IEnumerable<Shape> Circles => m_Shapes.Where(x => x.GetType() == typeof(Circle));

    private SingletonShapes()
    {
      ShapeDto m_NewShapes = JsonConvert.DeserializeObject<ShapeDto>(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, TestConstants.SHAPES_FILE_01_PATH)));
      m_Shapes.AddRange(m_NewShapes.Hexagons);
      m_Shapes.AddRange(m_NewShapes.Stars);
      m_Shapes.AddRange(m_NewShapes.Circles);

      //m_Shapes.AddRange(LoadHexagons(5, ColorType.Orange));
      //m_Shapes.AddRange(LoadHexagons(5, ColorType.Blue));
      //m_Shapes.AddRange(LoadHexagons(5, ColorType.Red));
      //m_Shapes.AddRange(LoadHexagons(5, ColorType.Grey));
      //m_Shapes.AddRange(LoadStars(5, ColorType.Grey));
      //m_Shapes.AddRange(LoadStars(5, ColorType.Blue));
      //m_Shapes.AddRange(LoadCircles(10, ColorType.Grey));
      //m_Shapes.AddRange(LoadCircles(10, ColorType.Red));
    }

    private static IEnumerable<Hexagon> LoadHexagons(int qty, ColorType color)
    {
      for (int i = 0; i < qty; i++)
      {
        yield return new Hexagon() { Color = color };
      }
    }

    private static IEnumerable<Star> LoadStars(int qty, ColorType color)
    {
      for (int i = 0; i < qty; i++)
      {
        yield return new Star() { Color = color };
      }
    }

    private static IEnumerable<Circle> LoadCircles(int qty, ColorType color)
    {
      for (int i = 0; i < qty; i++)
      {
        yield return new Circle() { Color = color };
      }
    }
  }

  public static class SampleShapes
  {
    public static IEnumerable<Shape> Shapes => SingletonShapes.Instance.Shapes;
    public static IEnumerable<Shape> Hexagons => SingletonShapes.Instance.Hexagons;
    public static IEnumerable<Shape> Stars => SingletonShapes.Instance.Stars;
    public static IEnumerable<Shape> Circles => SingletonShapes.Instance.Circles;
  }

  public class Shapes: List<Shape> { }

  public interface IShape
  {
    ColorType Color{ get; set; }
  }

  public class Shape : IShape
  {
    public ColorType Color { get; set; }
  }

  public class Hexagon : Shape { }
  public class Star : Shape { }
  public class Circle : Shape { }

  public enum ColorType
  {
     Orange,Blue,Red,Grey
  }

  public class ShapeDto
  {
    public List<Hexagon> Hexagons { get; set; }
    public List<Star> Stars { get; set; }
    public List<Circle> Circles { get; set; }
  }
}
