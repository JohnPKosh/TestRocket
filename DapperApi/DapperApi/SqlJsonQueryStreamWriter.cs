using DapperApi.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DapperApi
{
  public class SqlJsonQueryStreamWriter : IAsyncDisposable, IDisposable
  {
    #region Class Initialization

    public SqlJsonQueryStreamWriter(SqlConnection connection)
    {
      m_Connection = connection;
    }

    public SqlJsonQueryStreamWriter(string connectionString)
    {
      m_Connection = new SqlConnection(connectionString);
    }

    #endregion

    #region Fields and Properties

    private SqlDataReader m_SqlDataReader;
    private Utf8JsonWriter m_JsonWriter;
    private SqlConnection m_Connection;
    private SqlCommand m_SqlCommand;
    private ReadOnlyMemory<DbColumnInfo> m_DbColumns;

    #endregion

    public async Task<Stream> ExecuteJsonQueryAsync(QueryCommandOption queryCommand)
    {
      m_SqlCommand = new SqlCommand(queryCommand.CommandText);
      m_SqlCommand.CommandTimeout = queryCommand.CommandTimeout;
      m_SqlCommand.CommandType = queryCommand.CommandType;
      // TODO: add parameter create logic here
      return await ExecuteJsonQueryAsync(m_SqlCommand);
    }

    public async Task<Stream> ExecuteJsonQueryAsync(string query)
    {
      m_SqlCommand = new SqlCommand(query);
      return await ExecuteJsonQueryAsync(m_SqlCommand);
    }

    public async Task<Stream> ExecuteJsonQueryAsync(SqlCommand sqlCommand)
    {
      EnsureConnection(sqlCommand);

      m_SqlDataReader = await sqlCommand.ExecuteReaderAsync();
      SetColumnsFromReader();

      var ms = new MemoryStream();
      await WriteJsonAsync(ms);

      await m_JsonWriter.DisposeAsync();
      await m_SqlDataReader.DisposeAsync();
      return ms;
    }

    private async Task WriteJsonAsync(Stream stream)
    {
      m_JsonWriter = new Utf8JsonWriter(stream);
      m_JsonWriter.WriteStartArray();
      while (await m_SqlDataReader.ReadAsync())
      {
        m_JsonWriter.WriteStartObject();
        for (int i = 0; i < m_SqlDataReader.FieldCount; i++)
        {
          await WriteValueAsync(i);
        }
        m_JsonWriter.WriteEndObject();
      }
      m_JsonWriter.WriteEndArray();
    }

    private async Task WriteValueAsync(int i)
    {
      var info = m_DbColumns.Span[i];
      if (await m_SqlDataReader.IsDBNullAsync(i)) m_JsonWriter.WriteNull(info.Name);
      else
      {
        switch (info.Type)
        {
          case "int":
            m_JsonWriter.WriteNumber(info.Name, await m_SqlDataReader.GetFieldValueAsync<int>(i));
            break;
          case "bigint":
            m_JsonWriter.WriteNumber(info.Name, await m_SqlDataReader.GetFieldValueAsync<long>(i));
            break;
          case "smallint":
            m_JsonWriter.WriteNumber(info.Name, await m_SqlDataReader.GetFieldValueAsync<short>(i));
            break;
          case "tinyint":
            m_JsonWriter.WriteNumber(info.Name, await m_SqlDataReader.GetFieldValueAsync<byte>(i));
            break;
          case "bit":
            m_JsonWriter.WriteBoolean(info.Name, await m_SqlDataReader.GetFieldValueAsync<bool>(i));
            break;
          case "numeric":
            m_JsonWriter.WriteNumber(info.Name, await m_SqlDataReader.GetFieldValueAsync<decimal>(i));
            break;
          case "decimal":
            m_JsonWriter.WriteNumber(info.Name, await m_SqlDataReader.GetFieldValueAsync<decimal>(i));
            break;
          case "smallmoney":
            m_JsonWriter.WriteNumber(info.Name, await m_SqlDataReader.GetFieldValueAsync<decimal>(i));
            break;
          case "money":
            m_JsonWriter.WriteNumber(info.Name, await m_SqlDataReader.GetFieldValueAsync<decimal>(i));
            break;
          case "float":
            m_JsonWriter.WriteNumber(info.Name, await m_SqlDataReader.GetFieldValueAsync<double>(i));
            break;
          case "real":
            m_JsonWriter.WriteNumber(info.Name, await m_SqlDataReader.GetFieldValueAsync<float>(i));
            break;
          case "date":
            m_JsonWriter.WriteString(info.Name, await m_SqlDataReader.GetFieldValueAsync<DateTime>(i));
            break;
          case "datetime":
            m_JsonWriter.WriteString(info.Name, await m_SqlDataReader.GetFieldValueAsync<DateTime>(i));
            break;
          case "datetime2":
            m_JsonWriter.WriteString(info.Name, await m_SqlDataReader.GetFieldValueAsync<DateTime>(i));
            break;
          case "smalldatetime":
            m_JsonWriter.WriteString(info.Name, await m_SqlDataReader.GetFieldValueAsync<DateTime>(i));
            break;
          case "datetimeoffset":
            m_JsonWriter.WriteString(info.Name, await m_SqlDataReader.GetFieldValueAsync<DateTimeOffset>(i));
            break;
          case "time":
            m_JsonWriter.WriteString(info.Name, (await m_SqlDataReader.GetFieldValueAsync<TimeSpan>(i)).ToString());
            break;
          case "binary":
            m_JsonWriter.WriteBase64String(info.Name, await m_SqlDataReader.GetFieldValueAsync<byte[]>(i));
            break;
          case "varbinary":
            m_JsonWriter.WriteBase64String(info.Name, await m_SqlDataReader.GetFieldValueAsync<byte[]>(i));
            break;
          case "image":
            m_JsonWriter.WriteBase64String(info.Name, await m_SqlDataReader.GetFieldValueAsync<byte[]>(i));
            break;
          case "rowversion":
            m_JsonWriter.WriteBase64String(info.Name, await m_SqlDataReader.GetFieldValueAsync<byte[]>(i));
            break;
          case "timestamp":
            m_JsonWriter.WriteBase64String(info.Name, await m_SqlDataReader.GetFieldValueAsync<byte[]>(i));
            break;
          case "uniqueidentifier":
            m_JsonWriter.WriteString(info.Name, await m_SqlDataReader.GetFieldValueAsync<Guid>(i));
            break;
          default:
            m_JsonWriter.WriteString(info.Name, await m_SqlDataReader.GetFieldValueAsync<string>(i));
            break;
        }
      }
    }

    private void EnsureConnection(SqlCommand sqlCommand)
    {
      if (m_Connection.State != System.Data.ConnectionState.Open) m_Connection.Open();
      sqlCommand.Connection = m_Connection;
    }

    #region DB Column Info

    private void SetColumnsFromReader()
    {
      m_DbColumns = new ReadOnlyMemory<DbColumnInfo>(GetColumnInfo().ToArray());
    }

    private IEnumerable<DbColumnInfo> GetColumnInfo()
    {
      for (int i = 0; i < m_SqlDataReader.FieldCount; i++)
      {
        yield return new DbColumnInfo() { Name = m_SqlDataReader.GetName(i), Type = m_SqlDataReader.GetDataTypeName(i) };
      }
    }

    private class DbColumnInfo
    {
      public string Name { get; set; }
      public string Type { get; set; }
    }

    #endregion

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          // TODO: dispose managed state (managed objects).
          m_DbColumns = null;
        }

        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.
        if (m_Connection.State == System.Data.ConnectionState.Open || m_Connection.State != System.Data.ConnectionState.Closed) m_Connection.Close();
        m_Connection.Dispose();
        m_SqlCommand.Dispose();
        m_SqlDataReader.Dispose();
        m_JsonWriter.Dispose();

        disposedValue = true;
      }
    }

    protected virtual async Task DisposeAsync(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          // TODO: dispose managed state (managed objects).
          m_DbColumns = null;
        }

        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.
        if (m_Connection.State == System.Data.ConnectionState.Open || m_Connection.State != System.Data.ConnectionState.Closed) m_Connection.Close();
        await m_Connection.DisposeAsync();
        await m_SqlCommand.DisposeAsync();
        await m_SqlDataReader.DisposeAsync();
        await m_JsonWriter.DisposeAsync();

        disposedValue = true;
      }
    }

    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    ~SqlJsonQueryStreamWriter()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(false);
    }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
      // TODO: uncomment the following line if the finalizer is overridden above.
      GC.SuppressFinalize(this);
    }

    public ValueTask DisposeAsync()
    {
      try
      {
        return new ValueTask(DisposeAsync(true));
      }
      catch (Exception exc)
      {
        return new ValueTask(Task.FromException(exc));
      }
    }

    #endregion

  }
}
