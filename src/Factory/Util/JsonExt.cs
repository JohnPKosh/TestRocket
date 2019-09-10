using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;


namespace Factory.Util
{
  public static class JsonExt
  {
    public static string ToString<T>(this T obj) where T : class
    {
      return JsonSerializer.Serialize(obj);
    }

    public static string ToString<T>(this T obj, JsonSerializerOptions options) where T : class
    {
      return JsonSerializer.Serialize(obj, options);
    }

    public static string ToPrettyString<T>(this T obj) where T : class
    {
      return JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
    }

    ///<summary>Writes to UTF8 JSON file.</summary>
    ///<usage>var file = MyRocketOrders.RocketsOrders.WriteToFile(new FileInfo("rocket-orders.json"));</usage>
    public static async Task<FileInfo> WriteToFile<T>(this T obj, FileInfo file) where T : class
    {
      using var fs = file.OpenWrite();
      await JsonSerializer.SerializeAsync<T>(fs, obj, null, CancellationToken.None);
      file.Refresh();
      return file;
    }
    ///<summary>Reads from UTF8 JSON file.</summary>
    ///<usage>MyRocketOrders.RocketsOrders = await MyRocketOrders.RocketsOrders.ReadFromFile(new FileInfo("rocket-orders.json"));</usage>    
    public static async Task<T> ReadFromFileAsync<T>(this T obj, FileInfo file) where T : class
    {
      using var fs = file.OpenRead();
      CancellationToken t = new CancellationToken();
      return await JsonSerializer.DeserializeAsync<T>(fs, new JsonSerializerOptions { WriteIndented = true }, t);
    }

    ///<summary>Reads from UTF8 JSON file.</summary>
    ///<usage>MyRocketOrders.RocketsOrders = await MyRocketOrders.RocketsOrders.ReadFromFile(new FileInfo("rocket-orders.json"));</usage>    
    public static T ReadFromFile<T>(this T obj, FileInfo file) where T : class
    {
      return ReadFromFileAsync(obj, file).GetAwaiter().GetResult();
    }

    public static async Task<T> ReadFromStreamAsync<T>(this Stream stream)
    {
      CancellationToken t = new CancellationToken();
      return await JsonSerializer.DeserializeAsync<T>(stream, null, t);
    }
  }
}