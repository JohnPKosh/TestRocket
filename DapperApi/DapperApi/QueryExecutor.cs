using System;
using System.Collections.Generic;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace DapperApi
{
  public class QueryExecutor
  {
    private SqlDataReader sqlreader;
    private Utf8JsonWriter jw;

    public async Task<Stream> ExecuteJsonQueryAsync(string query)
    {
      using var DbConnection = new SqlConnection(ApiConstants.TEST_CONNECT_STRING);
      DbConnection.Open();

      using var sqlcmd = new SqlCommand(query, DbConnection);
      sqlreader = await sqlcmd.ExecuteReaderAsync();

      var ms = new MemoryStream();
      jw = new Utf8JsonWriter(ms);
      jw.WriteStartArray();

      ReadOnlyMemory<DbColumnInfo> cols = new ReadOnlyMemory<DbColumnInfo>(GetColumnInfo().ToArray());

      while (await sqlreader.ReadAsync())
      {
        jw.WriteStartObject();
        for (int i = 0; i < sqlreader.FieldCount; i++)
        {
          await WriteValueAsync(i, cols.Span[i]);
        }
        jw.WriteEndObject();
      }

      jw.WriteEndArray();

      jw.Dispose(); // Clean Up
      while (jw.BytesPending > 0)
      {
        System.Threading.Thread.Sleep(5);
      }
      await sqlreader.DisposeAsync();
      return ms;
    }

    private async Task WriteValueAsync(int i, DbColumnInfo info)
    {
      if(await sqlreader.IsDBNullAsync(i)) jw.WriteNull(info.Name);
      else
      {
        switch (info.Type)
        {
          case "int":
            jw.WriteNumber(info.Name, await sqlreader.GetFieldValueAsync<int>(i));
            break;
          case "bigint":
            jw.WriteNumber(info.Name, await sqlreader.GetFieldValueAsync<long>(i));
            break;
          case "smallint":
            jw.WriteNumber(info.Name, await sqlreader.GetFieldValueAsync<short>(i));
            break;
          case "tinyint":
            jw.WriteNumber(info.Name, await sqlreader.GetFieldValueAsync<byte>(i));
            break;
          case "bit":
            jw.WriteBoolean(info.Name, await sqlreader.GetFieldValueAsync<bool>(i));
            break;
          case "numeric":
            jw.WriteNumber(info.Name, await sqlreader.GetFieldValueAsync<decimal>(i));
            break;
          case "decimal":
            jw.WriteNumber(info.Name, await sqlreader.GetFieldValueAsync<decimal>(i));
            break;
          case "smallmoney":
            jw.WriteNumber(info.Name, await sqlreader.GetFieldValueAsync<decimal>(i));
            break;
          case "money":
            jw.WriteNumber(info.Name, await sqlreader.GetFieldValueAsync<decimal>(i));
            break;
          case "float":
            jw.WriteNumber(info.Name, await sqlreader.GetFieldValueAsync<double>(i));
            break;
          case "real":
            jw.WriteNumber(info.Name, await sqlreader.GetFieldValueAsync<float>(i));
            break;
          case "date":
            jw.WriteString(info.Name, await sqlreader.GetFieldValueAsync<DateTime>(i));
            break;
          case "datetime":
            jw.WriteString(info.Name, await sqlreader.GetFieldValueAsync<DateTime>(i));
            break;
          case "datetime2":
            jw.WriteString(info.Name, await sqlreader.GetFieldValueAsync<DateTime>(i));
            break;
          case "smalldatetime":
            jw.WriteString(info.Name, await sqlreader.GetFieldValueAsync<DateTime>(i));
            break;
          case "datetimeoffset":
            jw.WriteString(info.Name, await sqlreader.GetFieldValueAsync<DateTimeOffset>(i));
            break;
          case "time":
            jw.WriteString(info.Name, (await sqlreader.GetFieldValueAsync<TimeSpan>(i)).ToString());
            break;
          case "binary":
            jw.WriteBase64String(info.Name, await sqlreader.GetFieldValueAsync<byte[]>(i));
            break;
          case "varbinary":
            jw.WriteBase64String(info.Name, await sqlreader.GetFieldValueAsync<byte[]>(i));
            break;
          case "image":
            jw.WriteBase64String(info.Name, await sqlreader.GetFieldValueAsync<byte[]>(i));
            break;
          case "rowversion":
            jw.WriteBase64String(info.Name, await sqlreader.GetFieldValueAsync<byte[]>(i));
            break;
          case "timestamp":
            jw.WriteBase64String(info.Name, await sqlreader.GetFieldValueAsync<byte[]>(i));
            break;
          case "uniqueidentifier":
            jw.WriteString(info.Name, await sqlreader.GetFieldValueAsync<Guid>(i));
            break;
          default:
            jw.WriteString(info.Name, await sqlreader.GetFieldValueAsync<string>(i));
            break;
        }
      }
    }

    private IEnumerable<DbColumnInfo> GetColumnInfo()
    {
      for (int i = 0; i < sqlreader.FieldCount; i++)
      {
        yield return new DbColumnInfo() { Name = sqlreader.GetName(i), Type = sqlreader.GetDataTypeName(i) };
      }
    }

    private class DbColumnInfo
    {
      public string Name { get; set; }
      public string Type { get; set; }
    }
  }
}


// https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-data-type-mappings
// https://docs.microsoft.com/en-us/sql/t-sql/data-types/data-types-transact-sql?view=sql-server-2017