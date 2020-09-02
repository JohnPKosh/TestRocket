using System;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;
using NJsonSchema.Generation;
using SRF.BasicAuth.Logic;
using SRF.BasicAuth.Models;

namespace LogApi
{
  public class Startup
  {
    private readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Guid.NewGuid().ToByteArray());

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers()
        .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));

      // Add Basic Auth (SRF.BasicAuth.Logic.StartupExtensions) and UserService
      services.AddBasicAuth(SecurityKey);
      services.AddScoped<IUserService, UserService>();

      // configure DI for additional application services here:

      // You can optionally add the background service in Program.cs instead of here.
      //services.AddSingleton<IBackgroundServiceToggle, BackgroundServiceToggle>();
      //services.AddHostedService<Worker>();

      services.AddOpenApiDocument(document =>
        {
          document.Version = "v1";
          document.Title = "Log API";
          document.Description = "The kick ass log management API";
          document.DefaultReferenceTypeNullHandling = ReferenceTypeNullHandling.NotNull;

          document.PostProcess = d =>
          {
            d.Info.TermsOfService = "https://github.com/JohnPKosh";
            d.Info.Contact = new NSwag.OpenApiContact
            {
              Name = "John Kosh",
              Email = "devprohome@live.com",
              Url = "https://github.com/JohnPKosh"
            };
            d.Info.License = new NSwag.OpenApiLicense
            {
              Name = "Use under license",
              Url = "https://github.com/JohnPKosh"
            };
          };
        }
      );; // add OpenAPI v3 document

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();

        // Register the Swagger generator and the Swagger UI middlewares
        app.UseOpenApi();
        app.UseSwaggerUi3();   // view Swagger UI at: /swagger/
        //app.UseReDoc();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();

        // TODO: refactor and improve below logic.  POST will be prefered way to pass token data.
        endpoints.MapPost("token", context =>
        {
          if (!context.User.Identity.IsAuthenticated)
          {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return context.Response.CompleteAsync();
            //return context.Response.WriteAsync("Unauthorized");
          }

          var tokenManager = app.ApplicationServices.GetService<ITokenBuilder>();
          var cancellationToken = context.RequestAborted;
          var body = JsonSerializer.DeserializeAsync<TokenRequest>(context.Request.Body, null, cancellationToken);

          return context.Response.WriteAsync(tokenManager.GenerateToken(body.Result, SecurityKey));
        });
      });
    }
  }
}
