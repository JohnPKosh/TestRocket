using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace SRF.BasicAuth.Logic
{
  public static class StartupExtensions
  {
    public static IServiceCollection AddBasicAuth(this IServiceCollection services, SymmetricSecurityKey SecurityKey)
    {
      services.AddSingleton<ITokenBuilder, JwtTokenBuilder>();

      services.AddAuthorization(options =>
      {
        options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
        {
          policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
          policy.RequireClaim(ClaimTypes.Name);
        });
      });
      services.AddAuthentication("BasicAuthentication")
          .AddJwtBearer(options =>
          {
            options.SaveToken = true; // ???
            options.RequireHttpsMetadata = false; // ???
            options.TokenValidationParameters =
                      new TokenValidationParameters
                      {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateActor = false,
                        ValidateLifetime = true,
                        IssuerSigningKey = SecurityKey
                      };
          })
          .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
      ;

      // configure basic authentication
      //services.AddAuthentication("BasicAuthentication")
      //    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
      return services;

      // Multiple Policy schemes documentation:
      // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/policyschemes?view=aspnetcore-3.1
      // https://docs.microsoft.com/en-us/aspnet/core/security/authorization/limitingidentitybyscheme?view=aspnetcore-3.1
      // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/?view=aspnetcore-3.1

    }
  }
}
