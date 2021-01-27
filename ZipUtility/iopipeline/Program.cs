using System;
using System.Threading.Tasks;

namespace iopipeline
{
  class Program
  {
    static async Task Main(string[] args)
    {
      Console.WriteLine("Hello World!");

      var processor = new FileProcessor();
      var results = await processor.PerformRead().ConfigureAwait(false);

      Console.WriteLine(results);
      Console.ReadKey(true);
    }
  }
}
