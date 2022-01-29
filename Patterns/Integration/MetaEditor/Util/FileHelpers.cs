using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaEditor.Util
{
  public static class FileHelpers
  {

    /// <summary>
    /// The AppContext.BaseDirectory used by the assembly resolver. (usefull when PublishSingleFile "-p:IncludeAllContentForSelfExtract=true")
    /// </summary>
    public static DirectoryInfo AppContextBaseDir => new DirectoryInfo(AppContext.BaseDirectory) ?? ProcessExeDir;

    public static FileInfo ProcessExe => new FileInfo(Environment.ProcessPath ?? null);

    public static DirectoryInfo ProcessExeDir => ProcessExe != null && ProcessExe.Exists ? ProcessExe.Directory : null;

    public static string AppContextPathUri => $"file:///{AppContextBaseDir.FullName.Replace("\\", "/")}";

    public static string ProcessExeDirPathUri => $"file:///{ProcessExeDir.FullName.Replace("\\", "/")}";

    public static string MakeAppContextRelativeUri(string relativeFileName)
      => $"{AppContextPathUri + "/" + relativeFileName.Replace("//", "/")}";

    public static string MakeProcessExeRelativeUri(string relativeFileName)
  => $"{ProcessExeDirPathUri + "/" + relativeFileName.Replace("//", "/")}";
  }
}
