using System.Threading.Tasks;
using Factory.Models;

namespace Factory.Logic
{
  public interface IRocketLoader
  {
    RocketOrder[] Load();
    Task<RocketOrder[]> LoadAsync();
  }
}