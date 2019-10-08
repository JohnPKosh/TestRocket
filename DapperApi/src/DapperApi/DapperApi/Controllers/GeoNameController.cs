using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace DapperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoNameController : ControllerBase
    {

    [HttpGet]
    public async Task Get()
    {
      var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
      if (syncIOFeature != null)
      {
        syncIOFeature.AllowSynchronousIO = true;
      }

      using var db = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=junk;Integrated Security=True;");

      var QUERY =
@"
SELECT TOP (1000) [PostalCode]
      ,[PlaceName]
      ,[AdminName1]
      ,[AdminCode1]
      ,[AdminName2]
      ,[AdminCode2]
      ,[Latitude]
      ,[Longitude]
      ,[Accuracy]
FROM [junk].[dbo].[USGeoName]
FOR JSON PATH
";

      await Task.Run(() =>
       {
         db.QueryInto(Response.Body, QUERY);
       });
    }

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