using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogApi.Logic
{
  public class WorkerPause : IWorkerPause
  {
    public bool IsPaused { get; private set; } = true;

    public void Pause()
    {
      IsPaused = true;
    }

    public void UnPause()
    {
      IsPaused = false;
    }
  }
}
