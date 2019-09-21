using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using System.Data;
using System.Data.SqlClient;
using XunitZipTests.Fixtures;

namespace XunitZipTests
{
  public class ShapesDbTests : IClassFixture<ShapeDbFixture>
  {

    private readonly ITestOutputHelper output;
    private readonly ShapeDbFixture m_Fixture;

    public ShapesDbTests(ITestOutputHelper output, ShapeDbFixture fixture)
    {
      this.output = output;
      this.m_Fixture = fixture;
    }

    [Fact]
    public void CanTestConnection_True()
    {
      using (var db = new SqlConnection(m_Fixture.sutConnectionString))
      {
        try
        {
          db.Open();
        }
        catch (SqlException ex)
        {
          output.WriteLine(ex.Message);
          throw;
        }
        catch (Exception ex)
        {
          output.WriteLine(ex.Message);
          throw;
        }
        Assert.True(db.State == ConnectionState.Open || db.State == ConnectionState.Connecting);
      }
    }

    [Fact]
    public void HasRows_True()
    {
      var m_CommantText = TestConstants.SELECT_ALL_NOSTRATEGY;
      using (var db = new SqlConnection(m_Fixture.sutConnectionString))
      {
        SqlCommand command = new SqlCommand(m_CommantText, db);
        db.Open();
        SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
        Assert.True(reader.HasRows);
      }
    }

    [Theory]
    [InlineData(TestConstants.SELECT_ALL_NOSTRATEGY, 50)]
    [InlineData(TestConstants.SELECT_ALL_STRATEGY01, 50)]
    [InlineData(TestConstants.SELECT_ALL_STRATEGY01_COLORS, 4)]
    [InlineData(TestConstants.SELECT_ALL_STRATEGY01_SHAPES, 3)]
    public void HasCorrectNumberOfRows_True(string commandText, int expectedCount)
    {
      using (var db = new SqlConnection(m_Fixture.sutConnectionString))
      {
        SqlCommand command = new SqlCommand(commandText, db);
        db.Open();
        SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
        Assert.True(reader.HasRows);
        var m_Cnt = 0;
        while (reader.Read())
        {
          m_Cnt++;
        }
        Assert.True(m_Cnt == expectedCount);
        output.WriteLine($"Query:\r\n\r\n{commandText}\r\n\r\nReturned: {m_Cnt} rows");
      }
    }

    [Fact]
    public void CanCountColors_True()
    {
      var m_CommantText = TestConstants.SELECT_COLOR_COUNT;
      using (var db = new SqlConnection(m_Fixture.sutConnectionString))
      {
        SqlCommand command = new SqlCommand(m_CommantText, db);
        db.Open();
        SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
        while (reader.Read())
        {
          output.WriteLine(String.Format("{0} {1}", reader[0], reader[1]));
        }
      }
    }

    [Theory]
    [InlineData(100)]
    [InlineData(1000)]
    [InlineData(10000)]
    [InlineData(100000)]
    //[InlineData(1000000)]
    public void CanCountColorsSeveralTimes_True(int count)
    {
      var m_Blue = 0;
      var m_Grey = 0;
      var m_Orange = 0;
      var m_Red = 0;

      using (var db = new SqlConnection(m_Fixture.sutConnectionString))
      {
        SqlCommand command = new SqlCommand(TestConstants.SELECT_COLOR_COUNT, db);
        db.Open();

        for (int i = 0; i < count; i++)
        {
          using (SqlDataReader reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              switch (reader[0])
              {
                case "blue":
                  m_Blue += reader.GetInt32(1);
                  break;
                case "grey":
                  m_Grey += reader.GetInt32(1);
                  break;
                case "orange":
                  m_Orange += reader.GetInt32(1);
                  break;
                case "red":
                  m_Red += reader.GetInt32(1);
                  break;
                default:
                  break;
              }
            }
          }
        }
      }
      output.WriteLine($"Blue Shapes: {m_Blue.ToString("N0")} Grey Shapes: {m_Grey.ToString("N0")} Orange Shapes: {m_Orange.ToString("N0")} Red Shapes: {m_Red.ToString("N0")}");
      Assert.True((m_Blue + m_Grey + m_Orange + m_Red) > 0);
    }

  }
}
