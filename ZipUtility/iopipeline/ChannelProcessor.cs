﻿using System;
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
  public class ChannelProcessor
  {
    private static ReadOnlySpan<byte> m_NewLineBytes => new[] { (byte)'\r', (byte)'\n' };

    private Stream m_Stream;

    private Channel<string> m_Channel = Channel.CreateUnbounded<string>();

    private readonly ChannelWriter<string> m_Writer;

    public ChannelProcessor()
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
      var stream = new MemoryStream();

      using var sw = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true);
      foreach (var no in Enumerable.Range(1, m_LineCount))
      {
        foreach (var _ in Enumerable.Range(1, m_LineCharMultiplier))
        {
          sw.Write($"ABC{no:D7}");
        }
        sw.WriteLine();
      }
      sw.Flush();
      return stream;
    }

    private void GlobalCleanup()
    {
      m_Stream.Dispose();
    }


    public async Task<string> SingleProduceMultipleConsumers()
    {
      //var channel = Channel.CreateUnbounded<string>();

      // In this example, multiple consumers are needed to keep up with a fast producer

      //var producer1 = new Producer(channel.Writer, 1, 100);
      var consumer1 = new Consumer(m_Channel.Reader);
      var consumer2 = new Consumer(m_Channel.Reader);
      var consumer3 = new Consumer(m_Channel.Reader);

      Task<List<string>> consumerTask1 = consumer1.ConsumeData(); // begin consuming
      Task<List<string>> consumerTask2 = consumer2.ConsumeData(); // begin consuming
      Task<List<string>> consumerTask3 = consumer3.ConsumeData(); // begin consuming

      Task producerTask1 = ReadLineUsingPipelineVer2Async();

      await producerTask1.ContinueWith(_ => m_Channel.Writer.Complete());

      await Task.WhenAll(consumerTask1, consumerTask2, consumerTask3);

      var c1 = consumerTask1.Result.Count;
      var c2 = consumerTask2.Result.Count;
      var c3 = consumerTask3.Result.Count;
      return $"{c1}, {c2}, {c3} = {c1 + c2 +c3}";
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
  }

  //internal class Producer
  //{
  //  private readonly ChannelWriter<string> _writer;
  //  private readonly int _identifier;
  //  private readonly int _delay;

  //  public Producer(ChannelWriter<string> writer, int identifier, int delay)
  //  {
  //    _writer = writer;
  //    _identifier = identifier;
  //    _delay = delay;
  //  }

  //  public async Task BeginProducing()
  //  {
  //    Console.WriteLine($"PRODUCER ({_identifier}): Starting");

  //    for (var i = 0; i < 10; i++)
  //    {
  //      await Task.Delay(_delay); // simulate producer building/fetching some data

  //      var msg = $"P{_identifier} - {DateTime.UtcNow:G}";

  //      Console.WriteLine($"PRODUCER ({_identifier}): Creating {msg}");

  //      await _writer.WriteAsync(msg);
  //    }

  //    Console.WriteLine($"PRODUCER ({_identifier}): Completed");
  //  }
  //}

  internal class Consumer
  {
    private readonly ChannelReader<string> _reader;

    public Consumer(ChannelReader<string> reader)
    {
      _reader = reader;
    }

    public async Task<List<string>> ConsumeData()
    {
      var rv = new List<string>();
      while (await _reader.WaitToReadAsync())
      {
        if (_reader.TryRead(out var value))
        {
          rv.Add(value);
        }
      }
      return rv;
    }
  }
}
