using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using NJsonSchema.Generation;

namespace LogApi
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
      //services.AddSingleton<IBackgroundServiceToggle, BackgroundServiceToggle>();
      services.AddControllers()
        .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));

      //// You can optionally add the background service here instead of in Program.cs.
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
      }

      // Register the Swagger generator and the Swagger UI middlewares
      app.UseOpenApi();
      app.UseSwaggerUi3();   // view Swagger UI at: /swagger/
      //app.UseReDoc();

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
