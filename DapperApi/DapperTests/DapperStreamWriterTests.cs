using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using DapperApi;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Dynamic;

namespace DapperTests
{
  public class DapperStreamWriterTests
  {
    private readonly ITestOutputHelper output;

    public DapperStreamWriterTests(ITestOutputHelper output)
    {
      this.output = output;
    }


    [Fact]
    public void CanReadItems()
    {
      var got = GetGeoResultsAsync().Result;
      Assert.NotNull(got);
    }

    [Theory]
    [InlineData(1000)]
    public async Task CanReadStreamOverNTimes(int n)
    {
      var totalbytes = 0L;
      var sw = new Stopwatch();
      sw.Start();
      for (int i = 0; i < n; i++)
      {
        var got = GetGeoResultsAsync().Result;
        using var ms = new MemoryStream();
        await JsonSerializer.SerializeAsync(ms, got, got.GetType()).ConfigureAwait(false);
        totalbytes += ms.Length;
        Assert.True(ms.Length > 64);
      }
      sw.Stop();
      output.WriteLine($"Ran {n} times in {sw.ElapsedMilliseconds} milliseconds. Total Bytes {totalbytes}");
    }

    [Theory]
    [InlineData(1000)]
    public void CanReadStringOverNTimes(int n)
    {
      var sw = new Stopwatch();
      sw.Start();
      for (int i = 0; i < n; i++)
      {
        var got = GetGeoResultsAsync().Result;
        var dbresults = JsonSerializer.Serialize(got, got.GetType());
        Assert.NotNull(dbresults);
      }
      sw.Stop();
      output.WriteLine($"Ran {n} times in {sw.ElapsedMilliseconds} milliseconds.");
    }

    private async Task<IEnumerable<Geo>> GetGeoResultsAsync()
    {
      using var db = new SqlConnection(ApiConstants.MASTER_REF_CONNECT_STRING);
      var QUERY =
@"
SELECT TOP 1000 [PostalCode]
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
      return await db.QueryAsync<Geo>(QUERY).ConfigureAwait(false);
    }


    [Theory]
    [InlineData(1000)]
    public async Task CanReadDynamicStreamOverNTimes(int n)
    {
      var totalbytes = 0L;
      var sw = new Stopwatch();
      sw.Start();
      for (int i = 0; i < n; i++)
      {
        var got = GetDynamicResultsAsync().Result;
        using var ms = new MemoryStream();
        await JsonSerializer.SerializeAsync(ms, got, got.GetType()).ConfigureAwait(false);
        totalbytes += ms.Length;
        Assert.True(ms.Length > 64);
      }
      sw.Stop();
      output.WriteLine($"Ran {n} times in {sw.ElapsedMilliseconds} milliseconds. Total Bytes {totalbytes}");
    }

    [Theory]
    [InlineData(1000)]
    public void CanReadDynamicStringOverNTimes(int n)
    {
      var sw = new Stopwatch();
      sw.Start();
      for (int i = 0; i < n; i++)
      {
        var got = GetDynamicResultsAsync().Result;
        var dbresults = JsonSerializer.Serialize(got, got.GetType());
        Assert.NotNull(dbresults);
      }
      sw.Stop();
      output.WriteLine($"Ran {n} times in {sw.ElapsedMilliseconds} milliseconds.");
    }


    private async Task<IEnumerable<Dictionary<string, object>>> GetDynamicResultsAsync()
    {
      using var db = new SqlConnection(ApiConstants.MASTER_REF_CONNECT_STRING);
      var QUERY =
@"
SELECT TOP 1000 [PostalCode]
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
      return (await db.QueryAsync(QUERY).ConfigureAwait(false)).Select<dynamic, Dictionary<string, object>>(x => ToDictionary(x));
    }

    //public static dynamic ToExpandoObject(object value)
    //{
    //  IDictionary<string, object> dapperRowProperties = value as IDictionary<string, object>;

    //  IDictionary<string, object> expando = new ExpandoObject();

    //  foreach (KeyValuePair<string, object> property in dapperRowProperties)
    //    expando.Add(property.Key, property.Value);

    //  return expando as ExpandoObject;
    //}

    public static Dictionary<string, object> ToDictionary(object value)
    {
      IDictionary<string, object> dapperRowProperties = value as IDictionary<string, object>;
      Dictionary<string, object> rv = new Dictionary<string, object>();

      foreach (KeyValuePair<string, object> property in dapperRowProperties)
        rv.Add(property.Key, property.Value);

      return rv;
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
