using Newtonsoft.Json;

namespace CompositeLibrary.Infrastructure
{
  /// <summary>
  /// Basic JSON Extensions using System.Text.Json
  /// </summary>
  public static class WriteJson
  {
    // NOTE* - To serialize composite objects you should specify
    // ReferenceLoopHandling.Serialize like below in Json.Net.
    // Loop handling will not be part of .Net Core until .Net 5.0.
    // NewtonSoft Json.net is used here instead by using
    // the ReferenceLoopHandling.Serialize JsonSerializerSettings or
    // [JsonProperty(ReferenceLoopHandling = ReferenceLoopHandling.Serialize)] attribute.

    /// <summary>
    /// Converts an object of T to a JSON string (with indenting)
    /// </summary>
    /// <remarks>
    /// Add [JsonProperty(ReferenceLoopHandling = ReferenceLoopHandling.Serialize)] to composites.
    /// </remarks>
    public static string ToPrettyJson<T>(this T instance) where T : class
      => JsonConvert.SerializeObject(instance, Formatting.Indented);

    /// <summary>
    /// Converts an object of T to a JSON string with additional Formatting and JsonSerializerSettings
    /// </summary>
    /// <example>
    /// var json = root.ToJson(settings: new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Serialize });
    /// </example>
    public static string ToJson<T>(this T instance, Formatting formatting = Formatting.None,
      JsonSerializerSettings settings = null) where T : class
      => JsonConvert.SerializeObject(instance, formatting, settings);
  }
}
