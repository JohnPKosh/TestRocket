using Microsoft.IdentityModel.Tokens;
using SRF.BasicAuth.Models;

namespace SRF.BasicAuth.Logic
{
  /// <summary>
  /// Token Builder Interface used to generate a token from a TokenRequest
  /// </summary>
  public interface ITokenBuilder
  {
    /// <summary>
    /// Generates a Token using the specified TokenRequest and SymmetricSecurityKey
    /// </summary>
    /// <param name="request">TokenRequest</param>
    /// <param name="securityKey">SymmetricSecurityKey</param>
    /// <returns>string</returns>
    string GenerateToken(TokenRequest request, SymmetricSecurityKey securityKey);
  }
}
