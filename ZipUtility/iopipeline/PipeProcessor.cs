using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace iopipeline
{
  // https://medium.com/@joni2nja/evaluating-readline-using-system-io-pipelines-performance-in-c-part-2-b9d22c95254b
  // https://github.com/jo-ninja/ReadLinesBenchmarks/blob/45c548a03a4bafbfc6d2ce4817c446f9577c462d/ReadLinesBenchmarks/Program.cs#L119


  public class PipeProcessor
  {
    private static ReadOnlySpan<byte> NewLine => new[] { (byte)'\r', (byte)'\n' };

    private Stream _stream;

    public int LineNumber { get; set; } = 100000;

    public int LineCharMultiplier { get; set; } = 1;

    public IEnumerable<int> LineCharMultiplierValues => new[] { 1, 2, 8, 1000 };



    private void GlobalSetup()
    {
      _stream = PrepareStream();
    }

    private void GlobalCleanup()
    {
      _stream.Dispose();
    }

    private Stream PrepareStream()
    {
      var stream = new MemoryStream();

      using var sw = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true);
      foreach (var no in Enumerable.Range(1, LineNumber))
      {
        foreach (var _ in Enumerable.Range(1, LineCharMultiplier))
        {
          sw.Write($"ABC{no:D7}");
        }
        sw.WriteLine();
      }
      sw.Flush();
      return stream;
    }


    public async Task<string> PerformRead()
    {
      try
      {
        GlobalSetup();
        var sw = new Stopwatch();
        sw.Start();
        var rv = await ReadLineUsingPipelineVer2Async().ConfigureAwait(false);
        sw.Stop();
        Console.WriteLine($"Run time {sw.ElapsedMilliseconds}");
        return rv;
      }
      finally
      {
        GlobalCleanup();
      }
    }

    private async Task<string> ReadLineUsingPipelineVer2Async()
    {
      _stream.Seek(0, SeekOrigin.Begin);

      var reader = PipeReader.Create(_stream, new StreamPipeReaderOptions(leaveOpen: true));
      string str = string.Empty;

      while (true)
      {
        ReadResult result = await reader.ReadAsync();
        ReadOnlySequence<byte> buffer = result.Buffer;

        str += ProcessLine(ref buffer);

        reader.AdvanceTo(buffer.Start, buffer.End);

        if (result.IsCompleted) break;
      }

      await reader.CompleteAsync();
      return str;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string ProcessLine(ref ReadOnlySequence<byte> buffer)
    {
      string str = null;

      if (buffer.IsSingleSegment)
      {
        var span = buffer.FirstSpan;
        int consumed;
        while (span.Length > 0)
        {
          var newLine = span.IndexOf(NewLine);

          if (newLine == -1) break;

          var line = span.Slice(0, newLine);
          str += Encoding.UTF8.GetString(line) + Environment.NewLine;

          // simulate string processing
          //str = str.AsSpan().Slice(0, 10).ToString();

          consumed = line.Length + NewLine.Length;
          span = span.Slice(consumed);
          buffer = buffer.Slice(consumed);
        }
      }
      else
      {
        var sequenceReader = new SequenceReader<byte>(buffer);

        while (!sequenceReader.End)
        {
          while (sequenceReader.TryReadTo(out ReadOnlySequence<byte> line, NewLine))
          {
            str += Encoding.UTF8.GetString(line) + Environment.NewLine;

            // simulate string processing
            //str = str.AsSpan().Slice(0, 10).ToString();
          }

          buffer = buffer.Slice(sequenceReader.Position);
          sequenceReader.Advance(buffer.Length);
        }
      }

      return str;
    }
  }


}
