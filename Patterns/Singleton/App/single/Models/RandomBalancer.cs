using System;
using System.Collections.Generic;

namespace single.Models
{
  /// <summary>
  /// The random load balanced instance
  /// </summary>
  sealed class RandomBalancer
  {
    private static readonly RandomBalancer m_Instance = new RandomBalancer();
    private readonly List<Resource> m_Resources;
    private readonly Random m_Random = new Random();

    /// <summary>
    /// The **private** constructor
    /// </summary>
    private RandomBalancer()
    {
      // You would not want to hard code these in your real-world code!
      m_Resources = new List<Resource>
        {
         new Resource{ Name = "US East - 01", Target = "server://commodityserver001.us-east/resources" },
         new Resource{ Name = "US East - 02", Target = "server://commodityserver002.us-east/resources" },
         new Resource{ Name = "US Central - 03", Target = "server://commodityserver001.us-central/resources" },
         new Resource{ Name = "Europe West - 04", Target = "server://cheapserver001.eu-west/resources" },
         new Resource{ Name = "Australia - 05", Target = "server://expensiveserver001.australia/resources" },
        };
    }

    /// <summary>
    /// The public static method to get the singleton instance of our class
    /// </summary>
    /// <remarks> Could be shortened to: "public static RandomBalancer GetBalancer() => m_Instance;" </remarks>
    public static RandomBalancer GetBalancer()
    {
      return m_Instance;
    }

    /// <summary> Gets the next resource from our resource collection </summary>
    public Resource NextResource
    {
      get
      {
        int r = m_Random.Next(m_Resources.Count);
        return m_Resources[r];
      }
    }
  }

  /// <summary>
  /// The basic resource item class to hold a reference mapping to a particular distributed resource
  /// </summary>
  public class Resource
  {
    /// <summary> The name of our resource item </summary>
    public string Name { get; set; }

    /// <summary> The target resource path identifier of our resource </summary>
    public string Target { get; set; }
  }
}
