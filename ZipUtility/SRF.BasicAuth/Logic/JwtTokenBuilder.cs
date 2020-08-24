using Microsoft.IdentityModel.Tokens;
using SRF.BasicAuth.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SRF.BasicAuth.Logic
{
  /// <summary>
  /// JWT ITokenBuilder concrete implementation
  /// </summary>
  public class JwtTokenBuilder : ITokenBuilder
  {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
    // TODO: Refactor all of the below.

    private const string NAME_NOT_SPECIFIED = "Name is not specified.";

    private readonly JwtSecurityTokenHandler JwtTokenHandler = new JwtSecurityTokenHandler();

    /// <summary>
    /// Generates a JWT Token using the specified TokenRequest and SymmetricSecurityKey
    /// </summary>
    /// <param name="request">TokenRequest</param>
    /// <param name="securityKey">SymmetricSecurityKey</param>
    /// <returns>string</returns>
    public string GenerateToken(TokenRequest request, SymmetricSecurityKey securityKey)
    {
      if (request == null) throw new ArgumentNullException(nameof(request));
      if (string.IsNullOrEmpty(request.Name))
      {
        throw new InvalidOperationException(NAME_NOT_SPECIFIED);
      }

      var claims = new[] {
        new Claim(ClaimTypes.Name, request.Name),
        new Claim(ClaimTypes.Email, request.Email),
        new Claim("UserIdentity",request.UserIdentity )
      };
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
      var token = new JwtSecurityToken("ExampleServer", "ExampleClients", claims, expires: DateTime.Now.AddSeconds(60), signingCredentials: credentials);
      return JwtTokenHandler.WriteToken(token);
    }
  }
#pragma warning restore CA1303 // Do not pass literals as localized parameters
}
