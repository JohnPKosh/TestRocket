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
      using var DbConnection = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=junk;Integrated Security=True;");
      DbConnection.Open();

      using var sqlcmd = new SqlCommand(query, DbConnection);
      sqlreader = await sqlcmd.ExecuteReaderAsync();

      var ms = new MemoryStream();
      jw = new Utf8JsonWriter(ms);
      jw.WriteStartArray();

      var columnNames = new List<string>();
      var columnTypes = new List<string>();

      for (int i = 0; i < sqlreader.FieldCount; i++)
      {
        columnNames.Add(sqlreader.GetName(i));
        columnTypes.Add(sqlreader.GetDataTypeName(i));
      }

      while (await sqlreader.ReadAsync())
      {
        jw.WriteStartObject();
        for (int i = 0; i < sqlreader.FieldCount; i++)
        {
          await WriteValueAsync(i, columnNames[i], columnTypes[i]);
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

    private async Task WriteValueAsync(int i, string columnName, string typeName)
    {
      if(await sqlreader.IsDBNullAsync(i)) jw.WriteNull(columnName);
      else
      {
        switch (typeName)
        {
          case "int":
            jw.WriteNumber(columnName, await sqlreader.GetFieldValueAsync<int>(i));
            break;
          case "bigint":
            jw.WriteNumber(columnName, await sqlreader.GetFieldValueAsync<long>(i));
            break;
          case "smallint":
            jw.WriteNumber(columnName, await sqlreader.GetFieldValueAsync<short>(i));
            break;
          case "tinyint":
            jw.WriteNumber(columnName, await sqlreader.GetFieldValueAsync<byte>(i));
            break;
          case "bit":
            jw.WriteBoolean(columnName, await sqlreader.GetFieldValueAsync<bool>(i));
            break;
          case "numeric":
            jw.WriteNumber(columnName, await sqlreader.GetFieldValueAsync<decimal>(i));
            break;
          case "decimal":
            jw.WriteNumber(columnName, await sqlreader.GetFieldValueAsync<decimal>(i));
            break;
          case "smallmoney":
            jw.WriteNumber(columnName, await sqlreader.GetFieldValueAsync<decimal>(i));
            break;
          case "money":
            jw.WriteNumber(columnName, await sqlreader.GetFieldValueAsync<decimal>(i));
            break;
          case "float":
            jw.WriteNumber(columnName, await sqlreader.GetFieldValueAsync<double>(i));
            break;
          case "real":
            jw.WriteNumber(columnName, await sqlreader.GetFieldValueAsync<float>(i));
            break;
          case "date":
            jw.WriteString(columnName, await sqlreader.GetFieldValueAsync<DateTime>(i));
            break;
          case "datetime":
            jw.WriteString(columnName, await sqlreader.GetFieldValueAsync<DateTime>(i));
            break;
          case "datetime2":
            jw.WriteString(columnName, await sqlreader.GetFieldValueAsync<DateTime>(i));
            break;
          case "smalldatetime":
            jw.WriteString(columnName, await sqlreader.GetFieldValueAsync<DateTime>(i));
            break;
          case "datetimeoffset":
            jw.WriteString(columnName, await sqlreader.GetFieldValueAsync<DateTimeOffset>(i));
            break;
          case "time":
            jw.WriteString(columnName, (await sqlreader.GetFieldValueAsync<TimeSpan>(i)).ToString());
            break;
          case "binary":
            jw.WriteBase64String(columnName, await sqlreader.GetFieldValueAsync<byte[]>(i));
            break;
          case "varbinary":
            jw.WriteBase64String(columnName, await sqlreader.GetFieldValueAsync<byte[]>(i));
            break;
          case "image":
            jw.WriteBase64String(columnName, await sqlreader.GetFieldValueAsync<byte[]>(i));
            break;
          case "rowversion":
            jw.WriteBase64String(columnName, await sqlreader.GetFieldValueAsync<byte[]>(i));
            break;
          case "timestamp":
            jw.WriteBase64String(columnName, await sqlreader.GetFieldValueAsync<byte[]>(i));
            break;
          case "uniqueidentifier":
            jw.WriteString(columnName, await sqlreader.GetFieldValueAsync<Guid>(i));
            break;
          default:
            jw.WriteString(columnName, await sqlreader.GetFieldValueAsync<string>(i));
            break;
        }
      }
    }
  }
}


// https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-data-type-mappings
// https://docs.microsoft.com/en-us/sql/t-sql/data-types/data-types-transact-sql?view=sql-server-2017