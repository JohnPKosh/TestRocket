using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IO;

namespace DapperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoNameController : ControllerBase
    {

    [HttpGet]
    public async Task Get()
    {
      //var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
      //if (syncIOFeature != null)
      //{
      //  syncIOFeature.AllowSynchronousIO = true;
      //}

      using var db = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=junk;Integrated Security=True;");

      var QUERY =
@"
SELECT TOP 100 [PostalCode]
      ,[PlaceName]
      ,[AdminName1]
      ,[AdminCode1]
      ,[AdminName2]
      ,[AdminCode2]
      ,[Latitude]
      ,[Longitude]
      ,[Accuracy]
FROM [dbo].[USGeoName]
FOR JSON PATH
";
      await db.QueryAsyncInto(Response.Body, QUERY, buffered: false);
      //await Task.Run(() =>
      // {
      //   db.QueryInto(Response.Body, QUERY);
      // });
    }


    [HttpGet("query")]
    public async Task<ActionResult> QueryGeoName()
    {
      //var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
      //if (syncIOFeature != null)
      //{
      //  syncIOFeature.AllowSynchronousIO = true;
      //}

      using var db = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=junk;Integrated Security=True;");

      var QUERY =
@"
SELECT TOP 100 [PostalCode]
      ,[PlaceName]
      ,[AdminName1]
      ,[AdminCode1]
      ,[AdminName2]
      ,[AdminCode2]
      ,[Latitude]
      ,[Longitude]
      ,[Accuracy]
FROM [dbo].[USGeoName]
";
      //var data = await db.QueryAsync(QUERY);
      //return new OkObjectResult((await db.QueryAsync(QUERY)).Select(x => new {x.PostalCode, x.PlaceName, x.AdminName1, x.AdminCode1, x.AdminName2, x.AdminCode2, x.Latitude, x.Longitude, x.Accuracy }));
      return new OkObjectResult((await db.QueryAsync<Geo>(QUERY)));
      //return new OkObjectResult(data.Select(x=> (JObject)x));
    }

    [HttpGet("pipe")]
    public async Task GetPipeContent()
    {
      QueryExecutor qe = new QueryExecutor();
      var query =
@"
SELECT TOP (1000) [AccessFailedCount]
      ,[UserName]
      ,[PasswordHash]
      ,[PasswordExpiration]
      ,[ConcurrencyStamp]
      ,[IsBlocked]
      ,[IsDeleted]
      ,[LockoutEnabled]
      ,[LockoutEnd]
      ,[SecurityStamp]
      ,[Data]
      ,[ModifiedBy]
      ,[ModifiedDate]
  FROM [dbo].[UserAuthentication]
";

      var ms = await qe.ExecuteJsonQueryAsync(query) as MemoryStream;
      await Response.Body.WriteAsync(ms.ToArray(), 0, (int)ms.Length);


      //byte[] buffer = Encoding.Default.GetBytes("Hello World");
      //await Response.Body.WriteAsync(buffer, 0, buffer.Length);

      // https://localhost:5001/api/geoname/pipe
    }

  }

  public class Geo
  {
    public int PostalCode { get; set; }
    public string PlaceName { get; set; }
    public string AdminName1 { get; set; }
    public string AdminCode1 { get; set; }
    public string AdminName2 { get; set; }
    public string AdminCode2 { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public byte Accuracy { get; set; }
  }
}

// or do this in ConfigureServices

//public void ConfigureServices(IServiceCollection services)
//{
//  // If using Kestrel:
//  services.Configure<KestrelServerOptions>(options =>
//  {
//    options.AllowSynchronousIO = true;
//  });

//  // If using IIS:
//  services.Configure<IISServerOptions>(options =>
//  {
//    options.AllowSynchronousIO = true;
//  });
//}