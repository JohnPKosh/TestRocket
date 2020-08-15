using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogApi.Models
{
  public class BackgroundServiceRequest
  {
    public BackgroundServiceSignalType RequestType { get; set; }
  }
}
