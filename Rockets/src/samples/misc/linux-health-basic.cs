using System;
using System.IO;

public class DiskSpaceInfo
{
    public static void CheckDiskSpace(string driveName)
    {
        DriveInfo drive = new DriveInfo(driveName);

        if (drive.IsReady)
        {
            Console.WriteLine("Total size of drive {0} is {1} bytes", drive.Name, drive.TotalSize);
            Console.WriteLine("Available space on drive {0} is {1} bytes", drive.Name, drive.AvailableFreeSpace);
            Console.WriteLine("Used space on drive {0} is {1} bytes", drive.Name, drive.TotalSize - drive.AvailableFreeSpace);
        }
        else
        {
            Console.WriteLine("The drive {0} could not be read", driveName);
        }
    }
}

public class MemoryMetrics
{
    public double Total;
    public double Used;
    public double Free;
}

public class MemoryMetricsService
{
    public MemoryMetrics GetMetrics()
    {
        return GetUnixMetrics();
    }

    private MemoryMetrics GetUnixMetrics()
    {
        var output = "";

        var info = new ProcessStartInfo("free -m");
        info.FileName = "/bin/bash";
        info.Arguments = "-c \"free -m\"";
        info.RedirectStandardOutput = true;

        using (var process = Process.Start(info))
        {
            output = process.StandardOutput.ReadToEnd();
        }

        var lines = output.Split("\n");
        var memory = lines[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

        var metrics = new MemoryMetrics();
        metrics.Total = double.Parse(memory[1]);
        metrics.Used = double.Parse(memory[2]);
        metrics.Free = double.Parse(memory[3]);

        return metrics;
    }
}


public class MemoryInfo
{
    public static void CheckMemory()
    {
        long totalMemory = GC.GetTotalMemory(false);
        Console.WriteLine("Total memory used by the application: {0} bytes", totalMemory);

        // Available memory can be calculated only in Linux
        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
        {
            string[] memoryStatusLines = System.IO.File.ReadAllLines("/proc/meminfo");
            string memTotalLine = memoryStatusLines[0]; // MemTotal line
            string memAvailableLine = memoryStatusLines[2]; // MemAvailable line

            long totalMem = long.Parse(memTotalLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);
            long availableMem = long.Parse(memAvailableLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);
            long usedMem = totalMem - availableMem;

            Console.WriteLine("Total memory in the system: {0} kB", totalMem);
            Console.WriteLine("Available memory in the system: {0} kB", availableMem);
            Console.WriteLine("Used memory in the system: {0} kB", usedMem);
        }
        else
        {
            Console.WriteLine("This feature is available only on Linux.");
        }
    }
}

/*
  PerformanceCounter cpuCounter;
  cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

  public string getCurrentCpuUsage()
  {
      return cpuCounter.NextValue() + "%";
  }
*/