using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Azos.Data;

using ArchiveData.Models;

namespace ArchiveLogic.Generate
{
  public static class PersonBuilder
  {
    /// <summary>
    /// Creates and returns many random PersonRows with GDID global Ids based on
    /// the supplied era, authority, count (number to produce), and offset (starting point of GDID counter value)
    /// </summary>
    public static IEnumerable<PersonRow> GenerateMany(uint era, int authority, ulong count, ulong offset = 0)
    {
      for (ulong i = offset; i < count; i++)
      {
        yield return PersonRow.MakeFake(new GDID(era, authority, i));
      }
    }
  }
}
