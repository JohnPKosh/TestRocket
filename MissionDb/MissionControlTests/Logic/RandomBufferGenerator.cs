using System;
using System.Collections.Generic;
using System.Text;

namespace MissionControlTests.Logic
{
  public class RandomBufferGenerator
  {
    public Random m_Rng = new Random();

    public IEnumerable<byte[]> GetRandomGdids(int rows = 1000)
    {
      Span<byte> idBuffer = new Span<byte>(new byte[8]);
      Span<byte> eraBuffer = new Span<byte>(new byte[4]);
      Span<byte> gdidBuffer = new Span<byte>(new byte[12]);
      eraBuffer = BitConverter.GetBytes(0U);

      var ids = new HashSet<byte[]>();
      while (rows > 0)
      {
        FillBuffer(idBuffer, eraBuffer, gdidBuffer);
        var added = ids.Add(gdidBuffer.ToArray());
        if (added) rows--;
      }
      return ids;

      // local fill buffer function
      void FillBuffer(Span<byte> idBuffer, Span<byte> eraBuffer, Span<byte> gdidBuffer)
      {
        var authority = BitConverter.GetBytes(m_Rng.Next(0, 65535) >> 12)[0];
        m_Rng.NextBytes(idBuffer);
        idBuffer[0] = authority;
        eraBuffer.CopyTo(gdidBuffer);
        for (int i = 0; i < idBuffer.Length; i++)
        {
          gdidBuffer[i + 4] = idBuffer[i];
        }
      }
    }
  }
}
