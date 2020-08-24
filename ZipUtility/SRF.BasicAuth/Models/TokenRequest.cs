namespace SRF.BasicAuth.Models
{
  /// <summary>
  /// The JWT RPC Token Model
  /// </summary>
  public class TokenRequest
  {
    /// <summary>
    /// Identity Name value
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Identity Email value
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// The User Identity Name value
    /// </summary>
    public string UserIdentity { get; set; }
  }
}
