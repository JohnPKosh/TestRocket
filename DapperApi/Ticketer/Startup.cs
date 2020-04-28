using System;
using System.Buffers;
using System.IdentityModel.Tokens.Jwt;
using System.IO.Pipelines;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Ticketer.Services;

namespace Ticketer
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddGrpc();
      services.AddSingleton<TicketRepository>();

      services.AddAuthorization(options =>
      {
        options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
              {
            policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
            policy.RequireClaim(ClaimTypes.Name);
          });
      });
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
            options.TokenValidationParameters =
                      new TokenValidationParameters
                  {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateActor = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = SecurityKey
                  };
          });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        // Communication with gRPC endpoints must be made through a gRPC client.
        // To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909
        endpoints.MapGrpcService<TicketerService>();

        endpoints.MapGet("/generateJwtToken", context =>
              {
            return context.Response.WriteAsync(GenerateJwtToken(context.Request.Query["name"]));
          });

        // TODO: refactor and improve below logic.  POST will be prefered way to pass token data.
        endpoints.MapPost("token", context =>
        {
          var cancellationToken = context.RequestAborted;
          var body = JsonSerializer.DeserializeAsync<InputModel>(context.Request.Body, null, cancellationToken);

          return context.Response.WriteAsync(GenerateJwtToken(body.Result.Name));
        });
      });
    }


    private class InputModel // TODO: Move, extend, and rename this model for specific token specific content.
    {
      public string Name { get; set; }
    }

    private string GenerateJwtToken(string name)
    {
      if (string.IsNullOrEmpty(name))
      {
        throw new InvalidOperationException("Name is not specified.");
      }

      var claims = new[] { new Claim(ClaimTypes.Name, name) };
      var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
      var token = new JwtSecurityToken("ExampleServer", "ExampleClients", claims, expires: DateTime.Now.AddSeconds(60), signingCredentials: credentials);
      return JwtTokenHandler.WriteToken(token);
    }

    private readonly JwtSecurityTokenHandler JwtTokenHandler = new JwtSecurityTokenHandler();
    private readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Guid.NewGuid().ToByteArray());
  }
}
