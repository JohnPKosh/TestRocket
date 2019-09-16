using System;
using System.Collections.Generic;
using System.Text;

namespace XunitZipTests
{
  public static class TestConstants
  {
    public const string CANNOT_FIND_FILE_MSG = "Cannot find the file specified:";

    public const string CLEAN_UP_DIR_01 = @"elm";

    /* GZip Compression */

    public const string INPUT_FILE_01_PATH = @"elm\test-file-01.txt";

    public const string INPUT_FILE_01_COPY_PATH = @"elm\test-file-01-copy.txt";

    public const string OUTPUT_FILE_01_PATH = @"elm\test-file-01.gz";

    /* GZip Decompression */

    public const string CONTROL_INPUT_FILE_02_PATH = @"elm\control-file-02.gz";

    public const string CONTROL_OUTPUT_FILE_02_PATH = @"elm\control-file-02.txt";

    public const string INPUT_FILE_02_PATH = @"elm\test-file-02.gz";

    public const string OUTPUT_FILE_02_PATH = @"elm\test-file-02.txt";

    /* Roundtrip */

    public const string INPUT_FILE_03_PATH = @"elm\test-file-03.txt";

    public const string OUTPUT_FILE_03_PATH = @"elm\test-file-03.gz";

    public const string FINAL_FILE_03_PATH = @"elm\test-file-03-final.txt";

    public const string CONTROL_OUTPUT_FILE_03_PATH = @"elm\control-file-03.txt";

    public const string INPUT_FILE_04_PATH = @"elm\test-text-file-04";

    public const string OUTPUT_FILE_04_PATH = @"elm\test-gzip-file-04";

    public const string FINAL_FILE_04_PATH = @"elm\test-file-04-final.txt";

    public const string CONTROL_OUTPUT_FILE_04_PATH = @"elm\control-file-04.txt";
  }
}
