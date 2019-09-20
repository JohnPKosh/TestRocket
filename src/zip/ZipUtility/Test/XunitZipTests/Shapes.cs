using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XunitZipTests
{

  public sealed class SingletonShapes
  {
    private static readonly Lazy<SingletonShapes>lazy = new Lazy<SingletonShapes>(() => new SingletonShapes());

    public static SingletonShapes Instance { get { return lazy.Value; } }

    private Shapes m_Shapes = new Shapes();

    public Shapes GetHexagons()
    {
      var rv = new Shapes();
      rv.AddRange(m_Shapes.Where(x => x.GetType().Equals(typeof(Hexagon))));
      return rv;
    }

    private SingletonShapes()
    {
      m_Shapes.AddRange(LoadHexagons(5, ColorType.Orange));
      m_Shapes.AddRange(LoadHexagons(5, ColorType.Blue));
      m_Shapes.AddRange(LoadHexagons(5, ColorType.Red));
      m_Shapes.AddRange(LoadHexagons(5, ColorType.Grey));
      m_Shapes.AddRange(LoadStars(5, ColorType.Grey));
      m_Shapes.AddRange(LoadStars(5, ColorType.Blue));
      m_Shapes.AddRange(LoadCircles(10, ColorType.Grey));
      m_Shapes.AddRange(LoadCircles(10, ColorType.Red));
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
    public static Shapes GetHexagons() => SingletonShapes.Instance.GetHexagons();
  }

  public class Shapes: List<IShape> { }

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

}
