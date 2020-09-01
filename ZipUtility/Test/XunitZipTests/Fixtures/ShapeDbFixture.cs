using System;
using System.IO;

namespace XunitZipTests.Fixtures
{
  public class ShapeDbFixture : IDisposable
  {
    public string sutDataFile { get; private set; }
    public string sutConnectionString { get; private set; }

    public ShapeDbFixture()
    {
      /* Arrange and initialize some common some stuff here */
      sutDataFile = Path.Combine(Environment.CurrentDirectory, TestConstants.SHAPES_DATA_DIRECTORY, TestConstants.SHAPES_DB_FILE);
      sutConnectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={sutDataFile};Integrated Security=True";
    }

    public void Dispose()
    {
      /* clean up the common stuff here */
    }
  }
}
