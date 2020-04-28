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

    /// <summary>
    /// Reads an embedded resource from the specified assembly as a string.
    /// </summary>
    /// <param name="name">Root relative assembly path (\ seperated) to file.</param>
    /// <param name="assembly">The assembly to read from.</param>
    /// <returns>string</returns>
    public static string ReadEmbeddedResource(string name, Assembly assembly)
    {
      using (var resourceStream = assembly.GetManifestResourceStream(FormatResourceName(assembly, name)))
      {
        if (resourceStream == null) return null;
        using (var reader = new StreamReader(resourceStream))
        {
          return reader.ReadToEnd();
        }
      }
    }

    /// <summary>
    /// Reads an embedded resource from the specified assembly as a string.
    /// </summary>
    /// <param name="name">Root relative assembly path (\ seperated) to file.</param>
    /// <param name="assembly">The assembly to read from.</param>
    /// <returns><![CDATA[Task<string>]]></returns>
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

    /// <summary>
    /// Gets an embedded resource from the specified assembly as a stream.
    /// </summary>
    /// <param name="name">Root relative assembly path (\ seperated) to file.</param>
    /// <param name="assembly">The assembly to read from.</param>
    /// <returns>Stream</returns>
    public static Stream GetEmbeddedResourceStream(string name, Assembly assembly)
    {
      return assembly.GetManifestResourceStream(FormatResourceName(assembly, name));
    }

    /// <summary>
    /// Reads an embedded resource from the specified assembly as byte array.
    /// </summary>
    /// <param name="name">Root relative assembly path (\ seperated) to file.</param>
    /// <param name="assembly">The assembly to read from.</param>
    /// <returns><![CDATA[byte[]]]></returns>
    /// <example>
    /// var bytes = StreamExtensions.GetEmbeddedResourceAsBytes(@"Data\Params2.json", Assembly.GetExecutingAssembly());
    /// var str = new ParameterizedText().CreateFromJson(Encoding.Default.GetString(bytes));
    /// </example>
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

    /// <summary>
    /// Reads an embedded resource from the specified assembly as byte array.
    /// </summary>
    /// <param name="name">Root relative assembly path (\ seperated) to file.</param>
    /// <param name="assembly">The assembly to read from.</param>
    /// <returns><![CDATA[Task<byte[]>]]></returns>
    /// <example>
    /// var bytes = await StreamExtensions.GetEmbeddedResourceAsBytesAsync(@"Data\Params2.json", Assembly.GetExecutingAssembly());
    /// var str = new ParameterizedText().CreateFromJson(Encoding.Default.GetString(bytes));
    /// </example>
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
