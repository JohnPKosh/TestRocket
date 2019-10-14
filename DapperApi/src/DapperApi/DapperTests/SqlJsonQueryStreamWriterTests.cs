using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using DapperApi;
using System.Diagnostics;

namespace DapperTests
{
  public class SqlJsonQueryStreamWriterTests
  {
    private readonly ITestOutputHelper output;

    public SqlJsonQueryStreamWriterTests(ITestOutputHelper output)
    {
      this.output = output;
    }


    [Fact]
    public void CanReadStream()
    {
      var ms = GetJsonStreamAsync().Result;
      Assert.True(ms.Length > 1024);
    }

    [Theory]
    [InlineData(1000)]
    public void CanReadStreamOverNTimes(int n)
    {
      var totalbytes = 0L;
      var sw = new Stopwatch();
      sw.Start();
      for (int i = 0; i < n; i++)
      {
        var ms = GetJsonStreamAsync().Result;
        totalbytes += ms.Length;
        Assert.True(ms.Length > 1024);
      }
      sw.Stop();
      output.WriteLine($"Ran {n} times in {sw.ElapsedMilliseconds} milliseconds. Total Bytes {totalbytes}");
    }

    [Fact]
    public void CanReadToString()
    {
      var ms = GetJsonStreamAsync().Result;
      ms.Position = 0;
      Assert.True(ms.Length > 1024);
      using var sr = new StreamReader(ms);
      string text = sr.ReadToEnd();
      output.WriteLine(text);
      Assert.Contains("20190803084204", text);
    }

    private async Task<MemoryStream> GetJsonStreamAsync()
    {
      using var qe = new SqlJsonQueryStreamWriter(ApiConstants.TEST_CONNECT_STRING);
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
      var firstPass = await qe.ExecuteJsonQueryAsync(query) as MemoryStream;

      return await qe.ExecuteJsonQueryAsync(query) as MemoryStream;
    }

  }
}
