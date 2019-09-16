using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace ZipLib.Ext
{
  public static class StreamExtensions
  {
    public static void BufferCopyStream(
      this byte[] data,
      Stream outputStream,
      int bufferSize = 4096)
    {
      var inputStream = new MemoryStream(data);
      if (inputStream.CanSeek) inputStream.Seek(0, SeekOrigin.Begin);
      int readCount;
      var buffer = new byte[bufferSize];
      while ((readCount = inputStream.Read(buffer, 0, bufferSize)) != 0)
        outputStream.Write(buffer, 0, readCount);
    }

    public static void BufferCopyStream(
      this Stream inputStream,
      Stream outputStream,
      int bufferSize = 4096)
    {
      if (inputStream.CanSeek) inputStream.Seek(0, SeekOrigin.Begin);
      int readCount;
      var buffer = new byte[bufferSize];
      while ((readCount = inputStream.Read(buffer, 0, bufferSize)) != 0)
        outputStream.Write(buffer, 0, readCount);
    }

    public static async Task<string> ReadEmbeddedResourceAsync(string name, Assembly assembly)
    {
      using (var resourceStream = assembly.GetManifestResourceStream(FormatResourceName(assembly, name)))
      {
        if (resourceStream == null) return null;
        using (var reader = new StreamReader(resourceStream))
        {
          return await reader.ReadToEndAsync();
        }
      }
    }

    public static Stream GetEmbeddedResourceStream(string name, Assembly assembly)
    {
      return assembly.GetManifestResourceStream(FormatResourceName(assembly, name));
    }

    public static byte[] GetEmbeddedResourceAsBytes(string name, Assembly assembly)
    {
      var resource = FormatResourceName(assembly, name);
      using (var fs = assembly.GetManifestResourceStream(resource))
      {
        var buffersize = fs.Length > 4096 ? 4096 : (int)fs.Length;
        var bytes = new byte[buffersize];
        using (var ms = new MemoryStream())
        {
          while (true) // Loops Rule!!!!!!!!
          {
            var bytecount = fs.Read(bytes, 0, bytes.Length);
            if (bytecount > 0)
            {
              ms.Write(bytes, 0, bytecount);
            }
            else
            {
              break;
            }
          }
          return ms.ToArray();
        }
      }
    }

    public static async Task<byte[]> GetEmbeddedResourceAsBytesAsync(string name, Assembly assembly)
    {
      using (var fs = assembly.GetManifestResourceStream(FormatResourceName(assembly, name)))
      {
        var buffersize = fs.Length > 4096 ? 4096 : (int)fs.Length;
        var bytes = new byte[buffersize];
        using (var ms = new MemoryStream())
        {
          while (true) // Loops Rule!!!!!!!!
          {
            var bytecount = await fs.ReadAsync(bytes, 0, bytes.Length);
            if (bytecount > 0)
            {
              await ms.WriteAsync(bytes, 0, bytecount);
            }
            else
            {
              break;
            }
          }
          return ms.ToArray();
        }
      }
    }

    private static string FormatResourceName(Assembly assembly, string resourceName)
    {
      return assembly?.GetName().Name + "." + resourceName.Replace(" ", "_")
                                                         .Replace("\\", ".")
                                                         .Replace("/", ".");
    }
  }
}
