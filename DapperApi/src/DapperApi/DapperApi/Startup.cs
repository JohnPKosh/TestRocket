using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
      services.AddControllers();

      // Add NSwag OpenAPI/Swagger DI services and configure documents
      // For more advanced setup, see NSwag.Sample.NETCore20 project

      services.AddOpenApiDocument(document => document.DocumentName = "a");
      services.AddSwaggerDocument(document => document.DocumentName = "b");
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      // Add middlewares to service the OpenAPI/Swagger document and the web UI

      // URLs:
      // - http://localhost:32367/swagger/a/swagger.json
      // - http://localhost:32367/swagger/b/swagger.json
      // - http://localhost:32367/swagger

      app.UseOpenApi(); // registers the two documents in separate routes
      app.UseSwaggerUi3(); // registers a single Swagger UI (v3) with the two documents
    }
  }
}
