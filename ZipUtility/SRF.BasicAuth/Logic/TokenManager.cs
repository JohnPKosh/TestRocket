using Microsoft.IdentityModel.Tokens;
using SRF.BasicAuth.Models;

namespace SRF.BasicAuth.Logic
{
  /// <summary>
  /// Tocken Manager Class used to generate a token based on the supplied ITokenBuilder
  /// </summary>
  public class TokenManager
  {
    private readonly ITokenBuilder _builder;

    /// <summary>
    /// The Token Manager Constructor
    /// </summary>
    /// <param name="builder"></param>
    public TokenManager(ITokenBuilder builder)
    {
      _builder = builder;
    }

    /// <summary>
    /// Generates a JWT Token using the specified TokenRequest and SymmetricSecurityKey
    /// </summary>
    /// <param name="request">TokenRequest</param>
    /// <param name="securityKey">SymmetricSecurityKey</param>
    /// <returns>string</returns>
    public string GenerateToken(TokenRequest request, SymmetricSecurityKey securityKey)
    {
      return _builder.GenerateToken(request, securityKey);
    }
  }
}
