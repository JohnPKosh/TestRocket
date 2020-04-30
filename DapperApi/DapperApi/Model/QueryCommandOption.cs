using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperApi.Model
{
  public class QueryCommandOption
  {
    public string CommandText { get; set; }

    public int CommandTimeout { get; set; } = 60;

    public System.Data.CommandType CommandType { get; set; } = System.Data.CommandType.Text;

    public Dictionary<string, object> InputParams { get; set; } = new Dictionary<string, object>();
  }
}
