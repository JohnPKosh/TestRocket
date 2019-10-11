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
          //object obj = await sqlreader.GetFieldValueAsync<object>(i);
          //jw.WriteString(columnNames[i], obj.ToString());
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
          case "numeric":
            jw.WriteNumber(columnName, await sqlreader.GetFieldValueAsync<decimal>(i));
            break;
          case "smallint":
            jw.WriteNumber(columnName, await sqlreader.GetFieldValueAsync<short>(i));
            break;
          case "decimal":
            jw.WriteNumber(columnName, await sqlreader.GetFieldValueAsync<decimal>(i));
            break;
          case "smallmoney":
            jw.WriteNumber(columnName, await sqlreader.GetFieldValueAsync<decimal>(i));
            break;
          case "tinyint":
            jw.WriteNumber(columnName, await sqlreader.GetFieldValueAsync<byte>(i));
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