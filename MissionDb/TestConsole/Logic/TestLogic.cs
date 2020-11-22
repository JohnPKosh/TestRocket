using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Logic
{
  public class TestLogic
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

    public TestLogic()
    {
      //FillIntPool();
      SutROWIDConnection = new SqlConnection(CONN_STR);
      SutGDIDConnection = new SqlConnection(CONN_STR);

      SutROWIDCommand = new SqlCommand(ROWID_CMDTEXT, SutROWIDConnection);
      SutGDIDCommand = new SqlCommand(GDID_CMDTEXT, SutGDIDConnection);

      //SutROWIDCommand = new SqlCommand(ROWID_REL_CMDTEXT, SutROWIDConnection);
      //SutGDIDCommand = new SqlCommand(GDID_REL_CMDTEXT, SutGDIDConnection);
    }

    #endregion

    #region Fields and Props

    public string SutConnectionString => CONN_STR;

    public Stopwatch SutROWIDStopwatch { get; set; } = new Stopwatch();

    public Stopwatch SutGDIDStopwatch { get; set; } = new Stopwatch();

    public HashSet<int> SutRandomIntPool { get; set; }

    public SqlConnection SutROWIDConnection { get; set; }

    public SqlConnection SutGDIDConnection { get; set; }

    public SqlCommand SutROWIDCommand { get; set; }

    public SqlCommand SutGDIDCommand { get; set; }

    #endregion

    #region Public Methods

    public void CanQueryParallelNTimes(int nTimes)
    {
      FillIntPool(nTimes);
      var ROWIDs = SutRandomIntPool.ToArray();
      var GDIDs = new byte[nTimes][];
      EnsureROWIDConnection(SutROWIDCommand);
      EnsureGDIDConnection(SutGDIDCommand);

      ArrangeIDs(nTimes, ROWIDs, GDIDs);

      List<Task> tasks = new List<Task>();
      Task gdidTask = Task.Factory.StartNew(() => SelectGDIDs(nTimes, GDIDs));
      tasks.Add(gdidTask);
      Task rowTask = Task.Factory.StartNew(() => SelectROWIDs(nTimes, ROWIDs));
      tasks.Add(rowTask);
      Task.WaitAll(tasks.ToArray());
    }

    private void ArrangeIDs(int nTimes, int[] ROWIDs, byte[][] GDIDs)
    {
      // First pass needed to just fill the buffer of GDIDs.
      var paramROWID = new SqlParameter("ROWID", System.Data.SqlDbType.BigInt);
      var ridRow = new object[3];
      for (int i = 0; i < nTimes; i++)
      {
        paramROWID.Value = ROWIDs[i];
        SutROWIDCommand.Parameters.Clear();
        SutROWIDCommand.Parameters.Add(paramROWID);
        var reader = SutROWIDCommand.ExecuteReader(System.Data.CommandBehavior.Default);
        if (reader.HasRows)
        {
          while (reader.Read())
          {
            reader.GetValues(ridRow);
            if (i < 10) Console.WriteLine(ridRow[1].ToString());
            GDIDs[i] = (byte[])ridRow[0];
          }
        }
        else
        {
          Console.WriteLine("No rows found.");
        }
        reader.Close();
      }
    }

    private void SelectROWIDs(int nTimes, int[] ROWIDs)
    {
      var paramROWID = new SqlParameter("ROWID", System.Data.SqlDbType.BigInt);
      var ridRow = new object[3];
      var nTotalROWIDs = 0L;
      var nTotalBytesROWIDs = 0L;
      SutROWIDStopwatch.Restart();
      for (int i = 0; i < nTimes; i++)
      {
        paramROWID.Value = ROWIDs[i];
        SutROWIDCommand.Parameters.Clear();
        SutROWIDCommand.Parameters.Add(paramROWID);
        var reader = SutROWIDCommand.ExecuteReader(System.Data.CommandBehavior.Default);
        if (reader.HasRows)
        {
          while (reader.Read())
          {
            reader.GetValues(ridRow);
            nTotalROWIDs++;
            nTotalBytesROWIDs += (((byte[])ridRow[0]).Length + BitConverter.GetBytes((long)ridRow[1]).Length + Encoding.Default.GetBytes((string)ridRow[2]).Length);
          }
        }
        else
        {
          Console.WriteLine("No rows found.");
        }
        reader.Close();
      }
      SutROWIDStopwatch.Stop();
      Console.WriteLine("- Total ROWID rows read: **{0}** / Total Bytes read: **{1}** / Total ms: **{2}**", nTotalROWIDs, nTotalBytesROWIDs, SutROWIDStopwatch.ElapsedMilliseconds);
    }

    private void SelectGDIDs(int nTimes, byte[][] GDIDs)
    {
      var gidRow = new object[3];
      var paramGDID = new SqlParameter("GDID", System.Data.SqlDbType.Binary, 12);
      var nTotalGDIDIDs = 0L;
      var nTotalBytesGDIDs = 0L;
      SutGDIDStopwatch.Restart();
      for (int i = 0; i < nTimes; i++)
      {
        paramGDID.Value = GDIDs[i];
        SutGDIDCommand.Parameters.Clear();
        SutGDIDCommand.Parameters.Add(paramGDID);
        //SutGDIDCommand.Parameters.AddWithValue("GDID", GDIDs[i]);
        var reader = SutGDIDCommand.ExecuteReader(System.Data.CommandBehavior.Default);
        if (reader.HasRows)
        {
          while (reader.Read())
          {
            reader.GetValues(gidRow);
            nTotalGDIDIDs++;
            nTotalBytesGDIDs += (((byte[])gidRow[0]).Length + BitConverter.GetBytes((long)gidRow[1]).Length + Encoding.Default.GetBytes((string)gidRow[2]).Length);
          }
        }
        else
        {
          Console.WriteLine("No rows found.");
        }
        reader.Close();
      }
      SutGDIDStopwatch.Stop();
      Console.WriteLine("\r\n- Total GDID rows read: **{0}** / Total Bytes read: **{1}** / Total ms: **{2}**", nTotalGDIDIDs, nTotalBytesGDIDs, SutGDIDStopwatch.ElapsedMilliseconds);
    }

    #endregion

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
  }
}
