using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogApi.Logic
{
  public interface IWorkerPause
  {
    bool IsPaused { get; }
    void Pause();
    void UnPause();
  }
}
