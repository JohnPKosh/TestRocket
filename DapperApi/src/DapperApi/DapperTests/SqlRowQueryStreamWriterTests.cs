using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using DapperApi;
using System.Diagnostics;
using static DapperApi.SqlRowQueryStreamWriter;
using System.Text.Json;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace DapperTests
{
  public class SqlRowQueryStreamWriterTests
  {
    private readonly ITestOutputHelper output;

    public SqlRowQueryStreamWriterTests(ITestOutputHelper output)
    {
      this.output = output;
    }


    [Fact]
    public void CanReadStream()
    {
      var got = GetJsonStreamAsync().Result;
      Assert.NotNull(got);

      //using var fs = new FileStream("rawdata.json", FileMode.Create);
      //await JsonSerializer.SerializeAsync(fs, got, new JsonSerializerOptions() {  IgnoreNullValues = true}, CancellationToken.None).ConfigureAwait(false);

      var gotout = got.GetResults();
      var output = new DbResultOutput()
      {
        ColumnInfo = gotout.Item1,
        Rows = gotout.Item2
      };

      var dbresults = JsonSerializer.Serialize(output, output.GetType());

      //var result = JsonSerializer.Serialize<object[]>(output.Rows.Select(x=> x.Row).ToArray());
      var result = JsonSerializer.Serialize<IEnumerable<object[]>>(gotout.Item2);
    }

    [Fact]
    public async Task CanReadStream2()
    {
      DbResultOutput got = GetJsonStreamAsync().Result;
      Assert.NotNull(got);

      //using var fs = new FileStream("rawdata.json", FileMode.Create);
      //await JsonSerializer.SerializeAsync(fs, got, got.GetType(), new JsonSerializerOptions() { IgnoreNullValues = true }, CancellationToken.None).ConfigureAwait(false);

      using var ms = new MemoryStream();
      await JsonSerializer.SerializeAsync(ms, got, got.GetType(), new JsonSerializerOptions() { IgnoreNullValues = true }, CancellationToken.None).ConfigureAwait(false);

      //var gotout = got.GetResults();
      //var output = new DbResultOutput()
      //{
      //  ColumnInfo = gotout.Item1,
      //  Rows = gotout.Item2
      //};

      //var dbresults = JsonSerializer.Serialize(got, got.GetType());

      //var result = JsonSerializer.Serialize<object[]>(output.Rows.Select(x=> x.Row).ToArray());
      //var result = JsonSerializer.Serialize<IEnumerable<object[]>>(gotout.Item2);
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
        DbResultOutput got = GetJsonStreamAsync().Result;
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
        DbResultOutput got = GetJsonStreamAsync().Result;
        var dbresults = JsonSerializer.Serialize(got, got.GetType());
        Assert.NotNull(dbresults);
      }
      sw.Stop();
      output.WriteLine($"Ran {n} times in {sw.ElapsedMilliseconds} milliseconds.");
    }

    //[Fact]
    //public void CanReadToString()
    //{
    //  var ms = GetJsonStreamAsync().Result;
    //  ms.Position = 0;
    //  Assert.True(ms.Length > 1024);
    //  using var sr = new StreamReader(ms);
    //  string text = sr.ReadToEnd();
    //  output.WriteLine(text);
    //  Assert.Contains("20190803084204", text);
    //}

    private async Task<DbResults> GetJsonStreamAsync()
    {
      using var qe = new SqlRowQueryStreamWriter(ApiConstants.TEST_CONNECT_STRING);
      var query =
@"
SELECT TOP (100) [PostalCode]
      ,[PlaceName]
      ,[AdminName1]
      ,[AdminCode1]
      ,[AdminName2]
      ,[AdminCode2]
      ,[Latitude]
      ,[Longitude]
      ,[Accuracy]
  FROM [junk].[dbo].[USGeoName]
";
      var firstPass = await qe.ExecuteQueryAsync(query);

      return await qe.ExecuteQueryAsync(query);
    }

  }
}
