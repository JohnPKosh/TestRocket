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

      var output = new DbResultOutput()
      {
        ColumnInfo = got.ColumnInfo.ToArray(),
        Rows = got.Rows.ToArray()
      };

      var result = JsonSerializer.Serialize<object[]>(output.Rows.Select(x=> x.Row).ToArray());
    }

    [Theory]
    [InlineData(1000)]
    public void CanReadStreamOverNTimes(int n)
    {
      var totalRows = 0L;
      var sw = new Stopwatch();
      sw.Start();
      for (int i = 0; i < n; i++)
      {
        var got = GetJsonStreamAsync().Result;
        Assert.NotNull(got);
        totalRows += got.Rows.Length;
      }
      sw.Stop();
      output.WriteLine($"Ran {n} times in {sw.ElapsedMilliseconds} milliseconds. Total Bytes {totalRows}");
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
SELECT TOP (1000) [AccessFailedCount]
      ,[UserName]
     --,[PasswordHash]
     --,[PasswordExpiration]
     --,[ConcurrencyStamp]
     --,[IsBlocked]
     --,[IsDeleted]
     --,[LockoutEnabled]
     --,[LockoutEnd]
     --,[SecurityStamp]
     --,[Data]
     --,[ModifiedBy]
     --,[ModifiedDate]
  FROM [dbo].[UserAuthentication]
";
      var firstPass = await qe.ExecuteQueryAsync(query);

      return await qe.ExecuteQueryAsync(query);
    }

  }
}
