using Microsoft.Data.SqlClient;
using MissionControlTests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MissionControlTests
{
  public class GDIDBodyTests : IClassFixture<GDIDFixture>
  {
    private readonly ITestOutputHelper output;
    private readonly GDIDFixture m_Fixture;

    public GDIDBodyTests(ITestOutputHelper output, GDIDFixture fixture)
    {
      this.output = output;
      m_Fixture = fixture;
    }

    [Fact]
    public void FixtureHasRandomDistinctInts()
    {
      var expected = GDIDFixture.INT_COUNT;
      var got = m_Fixture.SutRandomIntPool.Count;
      Assert.True(got > 0);
      Assert.Equal(expected, got);
    }

    [Theory]
    [InlineData(10000)]
    //[InlineData(100000)]
    //[InlineData(1000000)]
    //[InlineData(10000000)]
    public void CanQueryNTimes(int nTimes)
    {
      m_Fixture.FillIntPool(nTimes);
      var ROWIDs = m_Fixture.SutRandomIntPool.ToArray();
      var GDIDs = new byte[nTimes][];
      m_Fixture.EnsureROWIDConnection(m_Fixture.SutROWIDCommand);
      m_Fixture.EnsureGDIDConnection(m_Fixture.SutGDIDCommand);

      ArrangeIDs(nTimes, ROWIDs, GDIDs);

      // Second Pass to collect performace vectors for ROWID data.
      SelectROWIDs(nTimes, ROWIDs);

      // Third Pass to collect performace vectors for GDID data.
      SelectGDIDs(nTimes, GDIDs);

    }


    [Theory]
    //[InlineData(10000)]
    [InlineData(100000)]
    [InlineData(1000000)]
    //[InlineData(10000000)]
    public void CanQueryParallelNTimes(int nTimes)
    {
      m_Fixture.FillIntPool(nTimes);
      var ROWIDs = m_Fixture.SutRandomIntPool.ToArray();
      var GDIDs = new byte[nTimes][];
      m_Fixture.EnsureROWIDConnection(m_Fixture.SutROWIDCommand);
      m_Fixture.EnsureGDIDConnection(m_Fixture.SutGDIDCommand);

      ArrangeIDs(nTimes, ROWIDs, GDIDs);

      List<Task> tasks = new List<Task>();
      Task rowTask = Task.Factory.StartNew(() => SelectROWIDs(nTimes, ROWIDs));
      tasks.Add(rowTask);
      Task gdidTask = Task.Factory.StartNew(() => SelectGDIDs(nTimes, GDIDs));
      tasks.Add(gdidTask);
      Task.WaitAll(tasks.ToArray());
    }

    private void ArrangeIDs(int nTimes, int[] ROWIDs, byte[][] GDIDs)
    {
      // First pass needed to just fill the buffer of GDIDs.
      var paramROWID = new SqlParameter("ROWID", System.Data.SqlDbType.Int);
      var ridRow = new object[3];
      for (int i = 0; i < nTimes; i++)
      {
        paramROWID.Value = ROWIDs[i];
        m_Fixture.SutROWIDCommand.Parameters.Clear();
        m_Fixture.SutROWIDCommand.Parameters.Add(paramROWID);
        var reader = m_Fixture.SutROWIDCommand.ExecuteReader(System.Data.CommandBehavior.Default);
        if (reader.HasRows)
        {
          while (reader.Read())
          {
            reader.GetValues(ridRow);
            if (i < 10) output.WriteLine(ridRow[1].ToString());
            GDIDs[i] = (byte[])ridRow[0];
          }
        }
        else
        {
          output.WriteLine("No rows found.");
        }
        reader.Close();
      }
    }

    private void SelectROWIDs(int nTimes, int[] ROWIDs)
    {
      var paramROWID = new SqlParameter("ROWID", System.Data.SqlDbType.Int);
      var ridRow = new object[3];
      var nTotalROWIDs = 0L;
      var nTotalBytesROWIDs = 0L;
      m_Fixture.SutROWIDStopwatch.Restart();
      for (int i = 0; i < nTimes; i++)
      {
        paramROWID.Value = ROWIDs[i];
        m_Fixture.SutROWIDCommand.Parameters.Clear();
        m_Fixture.SutROWIDCommand.Parameters.Add(paramROWID);
        var reader = m_Fixture.SutROWIDCommand.ExecuteReader(System.Data.CommandBehavior.Default);
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
          output.WriteLine("No rows found.");
        }
        reader.Close();
      }
      m_Fixture.SutROWIDStopwatch.Stop();
      output.WriteLine("- Total ROWID rows read: **{0}** / Total Bytes read: **{1}** / Total ms: **{2}**", nTotalROWIDs, nTotalBytesROWIDs, m_Fixture.SutROWIDStopwatch.ElapsedMilliseconds);
    }

    private void SelectGDIDs(int nTimes, byte[][] GDIDs)
    {
      var gidRow = new object[3];
      var paramGDID = new SqlParameter("GDID", System.Data.SqlDbType.Binary, 12);
      var nTotalGDIDIDs = 0L;
      var nTotalBytesGDIDs = 0L;
      m_Fixture.SutGDIDStopwatch.Restart();
      for (int i = 0; i < nTimes; i++)
      {
        paramGDID.Value = GDIDs[i];
        m_Fixture.SutGDIDCommand.Parameters.Clear();
        m_Fixture.SutGDIDCommand.Parameters.Add(paramGDID);
        var reader = m_Fixture.SutGDIDCommand.ExecuteReader(System.Data.CommandBehavior.Default);
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
          output.WriteLine("No rows found.");
        }
        reader.Close();
      }
      m_Fixture.SutGDIDStopwatch.Stop();
      output.WriteLine("\r\n- Total GDID rows read: **{0}** / Total Bytes read: **{1}** / Total ms: **{2}**", nTotalGDIDIDs, nTotalBytesGDIDs, m_Fixture.SutGDIDStopwatch.ElapsedMilliseconds);
    }
  }
}
