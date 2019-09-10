using System.Threading.Tasks;
using Factory.Models;

namespace Factory.Logic
{
  /// <summary>
  /// Current Rocket Order data array property class.
  /// </summary>
  public class CurrentRocketOrders
  {
    private IRocketLoader m_RocketLoader;

    public CurrentRocketOrders(IRocketLoader loader)
    {
      m_RocketLoader = loader;
      LoadRocketOrders(m_RocketLoader);
    }

    public void LoadRocketOrders(IRocketLoader loader)
    {
      rocketsOrders = loader.Load();
    }
    public async Task LoadRocketOrdersAsync(IRocketLoader loader)
    {
      rocketsOrders = await loader.LoadAsync();
    }

    /// <summary>
    /// Private backing store for my current order array.
    /// </summary>
    private RocketOrder[] rocketsOrders = new RocketOrder[0];

    /// <summary>
    /// Public Rocket Order array property (Full Range of Rockets)
    /// </summary>
    public RocketOrder[] RocketsOrders { get => rocketsOrders; set => rocketsOrders = value; }

    /// <summary>
    /// First 3 current Rocket Orders (Range 0..3)
    /// </summary>
    public RocketOrder[] FirstThreeOrders => RocketsOrders[0..3];

    /// <summary>
    /// Last 3 current Rocket Orders (Range ^3..)
    /// </summary>
    public RocketOrder[] LastThreeOrders => RocketsOrders[^3..];

    /// <summary>
    /// All but the first current Rocket Orders (Range 1..)
    /// </summary>
    public RocketOrder[] SkipFirstOrder => RocketsOrders[1..];
  }
}