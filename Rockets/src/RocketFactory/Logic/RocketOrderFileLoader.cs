using System.IO;
using System.Threading.Tasks;
using Factory.Common;
using Factory.Models;
using Factory.Util;

namespace Factory.Logic
{
  public class RocketOrderFileLoader : IRocketLoader
  {
    public RocketOrderFileLoader()
    {
      m_Source = new FileInfo(Constants.DEFAULT_ORDER_FILE_NAME);
    }
    public RocketOrderFileLoader(string filePath)
    {
      m_Source = new FileInfo(filePath);
    }

    public RocketOrderFileLoader(FileInfo fileInfo)
    {
      m_Source = fileInfo;
    }

    private FileInfo m_Source { get; set; }

    public RocketOrder[] Load()
    {
      var rv = new RocketOrder[0];
      return rv.ReadFromFile(m_Source);
    }

    public async Task<RocketOrder[]> LoadAsync()
    {
      var rv = new RocketOrder[0];
      return await rv.ReadFromFileAsync(m_Source);
    }
  }
}