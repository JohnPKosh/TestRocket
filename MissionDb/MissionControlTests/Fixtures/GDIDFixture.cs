using Microsoft.Data.SqlClient;
using MissionControlTests.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Data;

namespace MissionControlTests.Fixtures
{
  public class GDIDFixture : IDisposable
  {
    #region Class Initialization

    public const string CONN_STR = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=MissionControl;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";

    public const string ROWID_CMDTEXT = @"SELECT [GDID],[ROWID],[BODY] FROM [dbo].[ROWID_BODY] WHERE [ROWID] = @ROWID";

    public const string GDID_CMDTEXT = @"SELECT [GDID],[ROWID],[BODY] FROM [dbo].[GDID_BODY] WHERE [GDID] = @GDID";

    public const string ROWID_REL_CMDTEXT = @"
SELECT b.[GDID]
	,a.[ROWID]
	,b.[EXTERNALID]
FROM [MissionControl].[dbo].[ROWID_BODY] as a
INNER JOIN [MissionControlRef].[dbo].[ROWID_EXT_RES_CHILD] as b
ON a.[ROWID] = b.[ROWID]
WHERE a.[ROWID] = @ROWID";

    public const string GDID_REL_CMDTEXT = @"
SELECT b.[GDID]
	,a.[ROWID]
	,b.[EXTERNALID]
FROM [MissionControl].[dbo].[GDID_BODY] as a
INNER JOIN [MissionControlRef].[dbo].[GDID_EXT_RES_CHILD] as b
ON a.[GDID] = b.[GDID]
WHERE a.[GDID] = @GDID";

    public const int INT_COUNT = 10000;

    public GDIDFixture()
    {
      //FillIntPool();
      SutROWIDConnection = new SqlConnection(CONN_STR);
      SutGDIDConnection = new SqlConnection(CONN_STR);

      //SutROWIDCommand = new SqlCommand(ROWID_CMDTEXT, SutROWIDConnection);
      //SutGDIDCommand = new SqlCommand(GDID_CMDTEXT, SutGDIDConnection);

      SutROWIDCommand = new SqlCommand(ROWID_REL_CMDTEXT, SutROWIDConnection);
      SutGDIDCommand = new SqlCommand(GDID_REL_CMDTEXT, SutGDIDConnection);
    }

    #endregion

    public string SutConnectionString => CONN_STR;

    public Stopwatch SutROWIDStopwatch { get; set; } = new Stopwatch();

    public Stopwatch SutGDIDStopwatch { get; set; } = new Stopwatch();

    public HashSet<int> SutRandomIntPool { get; set; }

    public SqlConnection SutROWIDConnection { get; set; }

    public SqlConnection SutGDIDConnection { get; set; }

    public SqlCommand SutROWIDCommand { get; set; }

    public SqlCommand SutGDIDCommand { get; set; }

    #region Private Methods

    public void EnsureROWIDConnection(SqlCommand command)
    {
      if (SutROWIDConnection.State != ConnectionState.Open) SutROWIDConnection.Open();
      command.Connection = SutROWIDConnection;
    }

    public void EnsureGDIDConnection(SqlCommand command)
    {
      if (SutGDIDConnection.State != ConnectionState.Open) SutGDIDConnection.Open();
      command.Connection = SutGDIDConnection;
    }

    private void FillIntPool()
    {
      SutRandomIntPool = new HashSet<int>(INT_COUNT);
      var rows = INT_COUNT;
      while (rows > 0)
      {
        var added = SutRandomIntPool.Add(Rng.RandomInt(0, 10000000));
        if (added) rows--;
      }
    }

    public void FillIntPool(int rows)
    {
      SutRandomIntPool = new HashSet<int>(rows);
      while (rows > 0)
      {
        var added = SutRandomIntPool.Add(Rng.RandomInt(0, 10000000));
        if (added) rows--;
      }
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
        }

        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.

        disposedValue = true;
      }
    }

    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    // ~GDIDFixture()
    // {
    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //   Dispose(false);
    // }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
      // TODO: uncomment the following line if the finalizer is overridden above.
      // GC.SuppressFinalize(this);
    }
    #endregion

  }
}
