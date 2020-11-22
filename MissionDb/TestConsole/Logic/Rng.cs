using System;
using System.Collections.Generic;
using System.Text;

namespace TestConsole.Logic
{
  public static class Rng
  {
    private static Random m_Rng = new Random();

    public static int RandomInt(int lbound = 0, int ubound = 99)
    {
      return m_Rng.Next(lbound, ubound);
    }

    public static int[] RandomInts(int length, int lbound = 0, int ubound = 99)
    {
      var arr = new int[length];
      for (int i = 0; i < length; i++)
      {
        arr[i] = m_Rng.Next(lbound, ubound);
      }
      return arr;
    }
  }
}
