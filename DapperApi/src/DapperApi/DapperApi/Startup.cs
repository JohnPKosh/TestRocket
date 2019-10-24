using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;

namespace DapperApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc(option => option.EnableEndpointRouting = false);

      // or
      //services.AddControllersWithViews();
      //services.AddRazorPages();
      //services.AddControllers();

      services.AddDbContext<ApplicationDbContext>(options =>
      {
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

        options.UseOpenIddict();
      });

      services.AddOpenIddict()

    // Register the OpenIddict core services.
    .AddCore(options =>
    {
                  // Register the Entity Framework stores and models.
                  options.UseEntityFrameworkCore()
                         .UseDbContext<ApplicationDbContext>();
    })

    // Register the OpenIddict server handler.
    .AddServer(options =>
    {
                  // Register the ASP.NET Core MVC binder used by OpenIddict.
                  // Note: if you don't call this method, you won't be able to
                  // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                  options.UseMvc();

                  // Enable the token endpoint.
                  options.EnableTokenEndpoint("/connect/token");

                  // Enable the client credentials flow.
                  options.AllowClientCredentialsFlow();

                  // During development, you can disable the HTTPS requirement.
                  options.DisableHttpsRequirement();

                })

    // Register the OpenIddict validation handler.
    .AddValidation();

      // Register the Swagger services
      services.AddSwaggerDocument();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      //app.UseRouting();

      //app.UseAuthorization();

      //app.UseEndpoints(endpoints =>
      //{
      //  endpoints.MapControllers();
      //});

      app.UseAuthentication();

      // Register the Swagger generator and the Swagger UI middlewares
      app.UseOpenApi();
      app.UseSwaggerUi3();

      app.UseMvcWithDefaultRoute();

      app.UseWelcomePage();

      // Seed the database with the sample application.
      // Note: in a real world application, this step should be part of a setup script.
      InitializeAsync(app.ApplicationServices).GetAwaiter().GetResult();
    }

    private async Task InitializeAsync(IServiceProvider services)
    {
      // Create a new service scope to ensure the database context is correctly disposed when this methods returns.
      using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();

        var manager = scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication>>();

        if (await manager.FindByClientIdAsync("console") == null)
        {
          var descriptor = new OpenIddictApplicationDescriptor
          {
            ClientId = "console",
            ClientSecret = "388D45FA-B36B-4988-BA59-B187D329C207",
            DisplayName = "My client application",
            Permissions =
                        {
                            OpenIddictConstants.Permissions.Endpoints.Token,
                            OpenIddictConstants.Permissions.GrantTypes.ClientCredentials
                        }
          };

          await manager.CreateAsync(descriptor);
        }
      }
    }

  }
}
