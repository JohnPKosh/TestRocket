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
