using System;
using System.Threading.Tasks;

namespace iopipeline
{
  class Program
  {
    static async Task Main(string[] args)
    {
      Console.WriteLine("Hello World!");

      var piper = new PipeProcessor();
      var results = await piper.PerformRead().ConfigureAwait(false);

      Console.WriteLine(results?.Length);
    }
  }
}
