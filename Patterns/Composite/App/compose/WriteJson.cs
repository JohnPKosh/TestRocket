using System;
using System.Text.Json;

namespace compose
{
  /// <summary>
  /// Basic JSON Extensions using System.Text.Json
  /// </summary>
  public static class WriteJson
  {
    /// <summary>
    /// Converts an object of T to a JSON string
    /// </summary>
    public static string ToJson<T>(this T instance, JsonSerializerOptions options = null) where T : class
    {
      return instance == null ? null : JsonSerializer.Serialize(instance, options);
    }

    /// <summary>
    /// Converts an object to a JSON string
    /// </summary>
    public static string ToJson(this object instance, Type inputType, JsonSerializerOptions options = null)
    {
      return instance == null ? null : JsonSerializer.Serialize(instance, inputType, options);
    }

    /// <summary>
    /// Converts an object of T to a JSON string
    /// </summary>
    public static string ToPrettyJson<T>(this T instance) where T : class
    {
      return instance == null ? null : JsonSerializer.Serialize(instance, new JsonSerializerOptions { WriteIndented = true});
    }
  }
}
