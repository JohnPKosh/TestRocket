using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azos;
using Azos.Data;
using Azos.Log;
using Azos.Text;

namespace ArchiveData.Models
{
  public class FakeLogMessage : FakeRow
  {
    public Message Instance { get; set; } = new Message();

    public override FakeRow Populate(GDID parentGdid)
    {
      ID = parentGdid;

      Instance.Gdid = ID;
      Instance.Guid = Guid.NewGuid();
      Instance.RelatedTo = Guid.NewGuid();
      Instance.App = Atom.Encode("tzt");
      Instance.Channel = Atom.Encode("tezt");
      Instance.Type = MessageType.Info;
      Instance.Source = 0;
      Instance.UTCTimeStamp = DateTime.Now.AddHours(Ambient.Random.NextScaledRandomInteger(1, 12)).AddMinutes(Ambient.Random.NextScaledRandomInteger(1, 60));
      Instance.Host = "127.0.0.0";
      Instance.From = "walla walla";
      Instance.Topic = "Test";
      Instance.Text = "blah";
      Instance.Parameters = @"{""node"": 1}";
      Instance.Exception = new Exception("hello");
      Instance.ArchiveDimensions = "some dim data";


      //From = $"{Ambient.Random.NextScaledRandomInteger(200, 999)}{Ambient.Random.NextScaledRandomInteger(100, 999)}{Ambient.Random.NextScaledRandomInteger(0, 9999):D4}";
      //To = $"{Ambient.Random.NextScaledRandomInteger(200, 999)}{Ambient.Random.NextScaledRandomInteger(100, 999)}{Ambient.Random.NextScaledRandomInteger(0, 9999):D4}";
      //Host = $"127.0.0.{Ambient.Random.NextScaledRandomInteger(1, 20)}";
      //Body = NaturalTextGenerator.Generate(50);
      //SentDate = DateTime.Now.AddHours(Ambient.Random.NextScaledRandomInteger(1, 12)).AddMinutes(Ambient.Random.NextScaledRandomInteger(1, 60));
      return this;
    }
  }
}
