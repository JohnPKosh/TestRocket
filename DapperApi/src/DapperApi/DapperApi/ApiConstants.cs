using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperApi
{
  public static class ApiConstants
  {
    public const string TEST_CONNECT_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=junk;Integrated Security=True;";

    public const string MASTER_REF_CONNECT_STRING = @"Data Source=.\EXP2017;Initial Catalog=MasterRef;Integrated Security=True;";
  }
}
