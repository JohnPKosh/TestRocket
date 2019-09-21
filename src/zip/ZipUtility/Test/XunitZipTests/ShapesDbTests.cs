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

  }
}
