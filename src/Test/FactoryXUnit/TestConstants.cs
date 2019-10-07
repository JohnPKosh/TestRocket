using System;
using System.IO;

namespace FactoryXUnit
{
  public static class TestConstants
  {
    public const string CANNOT_FIND_FILE_MSG = "Cannot find the file specified:";

    //public const string SUT_DB_FILE = @"SampleData.mdf";

    public const string TAB_HEADER_SIMPLE = "country code	postal code	place name	admin name1	admin code1	admin name2	admin code2	admin name3	admin code3	latitude	longitude	accuracy";

    public const string TAB_LINE_SIMPLE = "US	99591	Saint George Island	Alaska	AK	Aleutians West (CA)	016			56.5944	-169.6186	1";

    #region SUT Elements Directory

    public const string SUT_ELM_DIR = @"elm";

    public static string m_ElmDirectoryPath => Path.Combine(Environment.CurrentDirectory, TestConstants.SUT_ELM_DIR);

    public static DirectoryInfo m_ElmDirectoryInfo => new DirectoryInfo(m_ElmDirectoryPath);

    public static string GetSUT_ELM_FilePath(string fileName) => Path.Combine(m_ElmDirectoryPath, fileName);

    public static FileInfo GetSUT_ELM_FileInfo(string fileName) => new FileInfo(Path.Combine(m_ElmDirectoryPath, fileName));

    #endregion

    #region SUT Input Directory

    public const string SUT_INPUT_DIR = @"elm\input";

    public static string m_InputDirectoryPath => Path.Combine(Environment.CurrentDirectory, TestConstants.SUT_INPUT_DIR);

    public static DirectoryInfo m_InputDirectoryInfo => new DirectoryInfo(m_InputDirectoryPath);

    public static string GetSUT_INPUT_FilePath(string fileName) => Path.Combine(m_InputDirectoryPath, fileName);

    public static FileInfo GetSUT_INPUT_FileInfo(string fileName) => new FileInfo(Path.Combine(m_InputDirectoryPath, fileName));

    #endregion

    #region SUT Output Directory

    public const string SUT_OUTPUT_DIR = @"elm\output";

    public static string m_OutputDirectoryPath => Path.Combine(Environment.CurrentDirectory, TestConstants.SUT_OUTPUT_DIR);

    public static DirectoryInfo m_OutputDirectoryInfo => new DirectoryInfo(m_OutputDirectoryPath);

    public static string GetSUT_OUTPUT_FilePath(string fileName) => Path.Combine(m_OutputDirectoryPath, fileName);

    public static FileInfo GetSUT_OUTPUT_FileInfo(string fileName) => new FileInfo(Path.Combine(m_OutputDirectoryPath, fileName));

    #endregion

    #region SUT Data Directory

    public const string SUT_DATA_DIRECTORY = @"App_Data";

    public static string m_DataDirectoryPath => Path.Combine(Environment.CurrentDirectory, TestConstants.SUT_DATA_DIRECTORY);

    public static DirectoryInfo m_DataDirectoryInfo => new DirectoryInfo(m_DataDirectoryPath);

    public static string GetSUT_DATA_FilePath(string fileName) => Path.Combine(m_DataDirectoryPath, fileName);

    public static FileInfo GetSUT_DATA_FileInfo(string fileName) => new FileInfo(Path.Combine(m_DataDirectoryPath, fileName));

    #endregion

  }
}
