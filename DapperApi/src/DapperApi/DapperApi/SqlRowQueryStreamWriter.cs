using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DapperApi
{
  public class SqlRowQueryStreamWriter : IAsyncDisposable, IDisposable
  {
    #region Class Initialization

    public SqlRowQueryStreamWriter(SqlConnection connection)
    {
      m_Connection = connection;
    }

    public SqlRowQueryStreamWriter(string connectionString)
    {
      m_Connection = new SqlConnection(connectionString);
    }

    #endregion

    #region Fields and Properties

    private SqlDataReader m_SqlDataReader;
    private SqlConnection m_Connection;
    private SqlCommand m_SqlCommand;
    private ReadOnlyMemory<DbColumnInfo> m_DbColumns;

    #endregion

    public async Task<DbResults> ExecuteQueryAsync(string query)
    {
      m_SqlCommand = new SqlCommand(query);
      return await ExecuteQueryAsync(m_SqlCommand);
    }

    public async Task<DbResults> ExecuteQueryAsync(SqlCommand sqlCommand)
    {
      var rv = new DbResults();
      EnsureConnection(sqlCommand);

      m_SqlDataReader = await sqlCommand.ExecuteReaderAsync();
      SetColumnsFromReader();
      rv.ColumnInfo = m_DbColumns;
      rv.Rows = await ReadRowsAsync();

      //await m_JsonWriter.DisposeAsync();
      await m_SqlDataReader.DisposeAsync();
      return rv;
    }

    private async Task<DbRowData[]> ReadRowsAsync()
    {
      var rows = new List<DbRowData>();
      while (await m_SqlDataReader.ReadAsync())
      {
        object[] raw = new object[m_DbColumns.Length];
        _ = m_SqlDataReader.GetValues(raw);
        rows.Add(new DbRowData(raw));
      }
      return rows.ToArray();
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
        yield return new DbColumnInfo(i, m_SqlDataReader.GetName(i), m_SqlDataReader.GetDataTypeName(i), m_SqlDataReader.GetFieldType(i));
      }
    }

    public struct DbColumnInfo
    {
      public DbColumnInfo(int ordinal, string name, string typeName, Type dataType)
      {
        Ordinal = ordinal;
        Name = name;
        TypeName = typeName;
        DataType = dataType;
      }
      public int Ordinal { get; set; }
      public string Name { get; set; }
      public string TypeName { get; set; }

      [JsonIgnore]
      public Type DataType { get; set; }
    }

    public struct DbRowData
    {
      public DbRowData(object[] row)
      {
        Row = row;
      }
      public object[] Row;
    }

    public class DbResults
    {
      public ReadOnlyMemory<DbColumnInfo> ColumnInfo { get; set; }
      public ReadOnlyMemory<DbRowData> Rows { get; set; }

      public (DbColumnInfo[], object[][]) GetResults()
      {
        return (ColumnInfo.ToArray(), Rows.ToArray().Select(x => x.Row).ToArray());
      }
    }

    public class DbResultOutput
    {
      public DbColumnInfo[] ColumnInfo { get; set; }
      public object[][] Rows { get; set; }
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

        disposedValue = true;
      }
    }

    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    ~SqlRowQueryStreamWriter()
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
