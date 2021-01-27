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
using System.Threading.Channels;

namespace iopipeline
{
  // https://devblogs.microsoft.com/dotnet/an-introduction-to-system-threading-channels/
  // https://www.stevejgordon.co.uk/an-introduction-to-system-threading-channels
  // https://github.com/stevejgordon/ChannelSample/blob/master/ChannelSample/Program.cs

  public class FileProcessor
  {
    private const int CONSUMER_CNT = 3;

    private static ReadOnlySpan<byte> m_NewLineBytes => new[] { (byte)'\r', (byte)'\n' };

    private Stream m_Stream;

    private Channel<string> m_Channel = Channel.CreateUnbounded<string>();

    private readonly ChannelWriter<string> m_Writer;

    public FileProcessor()
    {
      m_Writer = m_Channel.Writer;
    }

    private int m_LineCount { get; set; } = 1_000_000;

    private int m_LineCharMultiplier { get; set; } = 1;

    private void GlobalSetup()
    {
      m_Stream = PrepareStream();
    }

    private Stream PrepareStream()
    {
      return new FileStream("test-file-02.txt", FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan);
    }

    private void GlobalCleanup()
    {
      m_Stream.Dispose();
    }

    public async Task<string> PerformRead()
    {
      try
      {
        GlobalSetup();
        var sw = new Stopwatch();
        sw.Start();
        var rv = await SingleProduceMultipleConsumers().ConfigureAwait(false);
        sw.Stop();
        Console.WriteLine($"Run time {sw.ElapsedMilliseconds}");
        return rv;
      }
      finally
      {
        GlobalCleanup();
      }
    }

    public async Task<string> SingleProduceMultipleConsumers()
    {
      // In this example, multiple consumers are needed to keep up with a fast producer

      var consumers = new Task<List<string>>[CONSUMER_CNT];
      for (int i = 0; i < CONSUMER_CNT; i++)
      {
        consumers[i] = ConsumeData();
      }

      Task producerTask1 = ReadLineUsingPipelineVer2Async();   // begin producing
      await producerTask1.ContinueWith(_ => m_Channel.Writer.Complete());
      await Task.WhenAll(consumers);

      var total = 0;
      var rv = string.Empty;
      foreach (var c in consumers)
      {
        total += c.Result.Count;
        rv += $"[{c.Result.Count}] ";
      }

      return $"{rv} = {total}";
    }

    private async Task ReadLineUsingPipelineVer2Async()
    {
      m_Stream.Seek(0, SeekOrigin.Begin);

      var reader = PipeReader.Create(m_Stream, new StreamPipeReaderOptions(leaveOpen: true));

      while (true)
      {
        ReadResult result = await reader.ReadAsync();
        ReadOnlySequence<byte> buffer = result.Buffer;

        ProcessLine(ref buffer);

        reader.AdvanceTo(buffer.Start, buffer.End);
        if (result.IsCompleted) break;
      }
      await reader.CompleteAsync();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ProcessLine(ref ReadOnlySequence<byte> buffer)
    {
      if (buffer.IsSingleSegment)
      {
        var span = buffer.FirstSpan;
        int consumed;
        while (span.Length > 0)
        {
          var newLine = span.IndexOf(m_NewLineBytes);
          if (newLine == -1) break;

          var line = span.Slice(0, newLine);
          while (!m_Writer.TryWrite(Encoding.UTF8.GetString(line)))
          {
            Task.Delay(20);
          }

          consumed = line.Length + m_NewLineBytes.Length;
          span = span.Slice(consumed);
          buffer = buffer.Slice(consumed);
        }
      }
      else
      {
        var sequenceReader = new SequenceReader<byte>(buffer);
        while (!sequenceReader.End)
        {
          while (sequenceReader.TryReadTo(out ReadOnlySequence<byte> line, m_NewLineBytes))
          {
            while (!m_Writer.TryWrite(Encoding.UTF8.GetString(line)))
            {
              Task.Delay(20);
            }
          }
          buffer = buffer.Slice(sequenceReader.Position);
          sequenceReader.Advance(buffer.Length);
        }
      }
    }

    public async Task<List<string>> ConsumeData()
    {
      var rv = new List<string>();
      while (await m_Channel.Reader.WaitToReadAsync())
      {
        if (m_Channel.Reader.TryRead(out var value))
        {
          rv.Add(value);
        }
      }
      return rv;
    }



  }


}
