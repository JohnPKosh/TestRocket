using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogApi.Models
{
  public class WorkerPauseResult
  {
    public PauseRequestType RequestType { get; set; }

    public bool IsPaused { get; set; }
  }

  public enum PauseRequestType
  {
    Status,
    Pause,
    UnPause
  }
}
