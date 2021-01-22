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
  /// <summary>
  /// A fake wrapper class for Azos.Log Message concrete implementation 
  /// (containing only the sealed Message complex property). 
  /// This class differs from other FakeRow implementors only in the level of 
  /// complexity required to produce an adequately random set of logs data.
  /// </summary>
  public class FakeLogMessage : FakeRow
  {
    private Message m_Instance = new Message();

    public Message Instance
    {
      get
      {
        return m_Instance;
      }
      set
      {
        m_Instance = value;
      }
    }

    public override FakeRow Populate(GDID parentGdid)
    {
      ID = parentGdid;
      Instance.Gdid = ID;

      Director.Construct(Director.GetRandomBuilder(), ref m_Instance);

      return this;
    }
  }

  internal static class Director
  {
    internal static void Construct(IMessageBuilder builder, ref Message message)
    {
      message.Guid = Guid.NewGuid();
      message.RelatedTo = Guid.NewGuid();
      message.App = builder.GetApp(message.Gdid);
      message.Channel = builder.GetChannel(message.Gdid);
      message.Type = builder.GetType(message.Gdid);
      message.Source = builder.GetSource(message.Gdid);
      message.UTCTimeStamp = DateTime.Now.AddHours(Ambient.Random.NextScaledRandomInteger(1, 12)).AddMinutes(Ambient.Random.NextScaledRandomInteger(1, 60));
      message.Host = builder.GetHost(message.Gdid);
      message.From = builder.GetFrom(message.Gdid);
      message.Topic = builder.GetTopic(message.Gdid);
      message.Text = builder.GetText(message.Gdid);
      message.Parameters = builder.GetParameters(message.Gdid);
      message.Exception = builder.GetException(message.Gdid);
      message.ArchiveDimensions = builder.GetArchiveDimensions(message.Gdid);
    }

    internal static Random Rng = new Random();

    internal static int GetRandomInt(int lbound, int ubound)
    {
      return Rng.Next(lbound, ubound);
    }

    internal static IMessageBuilder GetRandomBuilder()
    {
      // TODO: Set up random builder scenarios here!!!!!!!!!

      var choice = GetRandomInt(1, 1000);
      return choice switch
      {
        > 0 and < 100 => new FlowInfoBuilder(),
        > 100 and < 200 => new XmlReqBuilder(),
        > 200 and < 300 => new JsonRequestBuilder(),
        > 300 and < 400 => new JsonResponseBuilder(),
        > 400 and < 500 => new XmlRespBuilder(),



        > 950 and < 975 => new ExceptionBuilder(),
        > 975 and < 1000 => new DeleteFilesBuilder(),
        _ => new DeleteFilesBuilder(),
      };
    }

  }

  // Builders common interface
  interface IMessageBuilder
  {
    Atom GetApp(GDID parentGdid);
    Atom GetChannel(GDID parentGdid);
    MessageType GetType(GDID parentGdid);
    int GetSource(GDID parentGdid);
    string GetHost(GDID parentGdid);
    string GetFrom(GDID parentGdid);
    string GetTopic(GDID parentGdid);
    string GetText(GDID parentGdid);
    string GetParameters(GDID parentGdid);
    Exception GetException(GDID parentGdid);
    string GetArchiveDimensions(GDID parentGdid);
  }


  public abstract class BuilderBase
  {
    public abstract Atom GetApp(GDID parentGdid);
    public virtual string GetArchiveDimensions(GDID parentGdid) => null;
    public abstract Atom GetChannel(GDID parentGdid);
    public virtual Exception GetException(GDID parentGdid) => null;
    public abstract string GetFrom(GDID parentGdid);

    public virtual string GetHost(GDID parentGdid)
    {
      var choice = Ambient.Random.NextScaledRandomInteger(1, 6);
      return choice switch
      {
        1 => "APPSRV-0001",
        2 => "APPSRV-0002",
        3 => "WEBSRV-0001",
        > 3 => "APPSRV-0003",
        _ => "WEBSRV-0001",
      };
    }

    public virtual string GetParameters(GDID parentGdid) => null;

    public virtual int GetSource(GDID parentGdid) => 0;
    public abstract string GetText(GDID parentGdid);
    public abstract string GetTopic(GDID parentGdid);
    public abstract MessageType GetType(GDID parentGdid);
  }

  public class DeleteFilesBuilder : BuilderBase, IMessageBuilder
  {
    public override Atom GetApp(GDID parentGdid) =>
      parentGdid.ID % 3 == 0 ? Atom.Encode("esgov") : Atom.Encode("hub");

    public override Atom GetChannel(GDID parentGdid) => Atom.ZERO;

    public override string GetFrom(GDID parentGdid) => "DeleteFilesJob@134.DoFire";

    public override string GetText(GDID parentGdid) => "Scanned 26 files, 0 dirs; Deleted 0 files, 0 dirs";

    public override string GetTopic(GDID parentGdid) => "Time";

    public override MessageType GetType(GDID parentGdid) => MessageType.Info;
  }

  public class XmlReqBuilder : BuilderBase, IMessageBuilder
  {
    public override Atom GetApp(GDID parentGdid) => Atom.Encode("fake");

    public override Atom GetChannel(GDID parentGdid) => Atom.Encode("oplog");

    public override string GetFrom(GDID parentGdid) => "FakeLogic.ReadRequestMessage";

    public override string GetText(GDID parentGdid) => "Raw xml req";

    public override string GetTopic(GDID parentGdid) => "blogic";

    public override MessageType GetType(GDID parentGdid) => MessageType.TraceC;

    public override string GetArchiveDimensions(GDID parentGdid)
    {
      var choice = Ambient.Random.NextScaledRandomInteger(1, 12);
      return choice switch
      {
        1 => @"{ ""chn"" : ""XXX"", ""clr"" : ""10.0.52.32:53322"" }",
        2 => @"{ ""chn"" : ""YYY"", ""clr"" : ""10.1.28.43:53323"" }",
        3 => @"{ ""chn"" : ""ZZZ"", ""clr"" : ""10.0.88.56:53380"" }",
        > 3 and < 7  => @"{ ""chn"" : ""--any--"", ""clr"" : ""10.1.33.44:53322"" }",
        > 7 and < 9 => @"{ ""chn"" : ""--any--"", ""clr"" : ""10.1.55.66:53322"" }",
        > 9 and < 12 => @"{ ""chn"" : ""--any--"", ""clr"" : ""10.1.77.88:53322"" }",
        _ => @"{ ""chn"" : ""ABCD"", ""clr"" : ""10.2.33.68:53399"" }",
      };
    }

    public override string GetParameters(GDID parentGdid)
    {
      return FakeLogConstants.FAKE_LOG_XML_REQ;
    }
  }

  public class FlowInfoBuilder : BuilderBase, IMessageBuilder
  {
    public override Atom GetApp(GDID parentGdid) => Atom.Encode("fake");

    public override Atom GetChannel(GDID parentGdid) => Atom.Encode("oplog");

    public override string GetFrom(GDID parentGdid) => "FakeLogic.CheckAsync";

    public override string GetText(GDID parentGdid) => "Flow info";

    public override string GetTopic(GDID parentGdid) => "blogic";

    public override MessageType GetType(GDID parentGdid) => MessageType.TraceA;

    public override string GetParameters(GDID parentGdid)
    {
      var choice = Ambient.Random.NextScaledRandomInteger(1, 4);
      return choice switch
      {
        1 => FakeLogConstants.FAKE_FLOW_1,
        2 => FakeLogConstants.FAKE_FLOW_2,
        3 => FakeLogConstants.FAKE_FLOW_3,
        _ => FakeLogConstants.FAKE_FLOW_3
      };
    }

  }

  public class JsonRequestBuilder : BuilderBase, IMessageBuilder
  {
    public override Atom GetApp(GDID parentGdid) => Atom.Encode("fake");

    public override Atom GetChannel(GDID parentGdid) => Atom.Encode("oplog");

    public override string GetFrom(GDID parentGdid) => "FakeLogic.CheckAsync";

    public override string GetText(GDID parentGdid) => "Got Fake request";

    public override string GetTopic(GDID parentGdid) => "blogic";

    public override MessageType GetType(GDID parentGdid) => MessageType.TraceB;

    public override string GetParameters(GDID parentGdid) => FakeLogConstants.FAKE_JSON_REQ;
  }


  public class JsonResponseBuilder : BuilderBase, IMessageBuilder
  {
    public override Atom GetApp(GDID parentGdid) => Atom.Encode("fake");

    public override Atom GetChannel(GDID parentGdid) => Atom.Encode("oplog");

    public override string GetFrom(GDID parentGdid) => "FakeLogic.CheckAsync";

    public override string GetText(GDID parentGdid) => "Fake response";

    public override string GetTopic(GDID parentGdid) => "blogic";

    public override MessageType GetType(GDID parentGdid) => MessageType.TraceD;

    public override string GetParameters(GDID parentGdid) => FakeLogConstants.FAKE_JSON_RESP;
  }


  public class XmlRespBuilder : BuilderBase, IMessageBuilder
  {
    public override Atom GetApp(GDID parentGdid) => Atom.Encode("fake");

    public override Atom GetChannel(GDID parentGdid) => Atom.Encode("oplog");

    public override string GetFrom(GDID parentGdid) => "FakeLogic.WriteResponseMessage";

    public override string GetText(GDID parentGdid) => "Raw xml resp";

    public override string GetTopic(GDID parentGdid) => "blogic";

    public override MessageType GetType(GDID parentGdid) => MessageType.TraceC;

    public override string GetArchiveDimensions(GDID parentGdid)
    {
      var choice = Ambient.Random.NextScaledRandomInteger(1, 12);
      return choice switch
      {
        1 => @"{ ""chn"" : ""XXX"", ""clr"" : ""10.0.52.32:53322"" }",
        2 => @"{ ""chn"" : ""YYY"", ""clr"" : ""10.1.28.43:53323"" }",
        3 => @"{ ""chn"" : ""ZZZ"", ""clr"" : ""10.0.88.56:53380"" }",
        > 3 and < 7 => @"{ ""chn"" : ""--any--"", ""clr"" : ""10.1.33.44:53322"" }",
        > 7 and < 9 => @"{ ""chn"" : ""--any--"", ""clr"" : ""10.1.55.66:53322"" }",
        > 9 and < 12 => @"{ ""chn"" : ""--any--"", ""clr"" : ""10.1.77.88:53322"" }",
        _ => @"{ ""chn"" : ""ABCD"", ""clr"" : ""10.2.33.68:53399"" }",
      };
    }

    public override string GetParameters(GDID parentGdid)
    {
      return FakeLogConstants.FAKE_LOG_XML_RESP;
    }
  }


  public class ExceptionBuilder : BuilderBase, IMessageBuilder
  {
    public override Atom GetApp(GDID parentGdid) => Atom.Encode("fake");

    public override Atom GetChannel(GDID parentGdid) => Atom.Encode("oplog");

    public override string GetFrom(GDID parentGdid) => "FakeLogic.safeGetOneAsync";

    public override string GetText(GDID parentGdid) => "Error leaked: [Fake.Clients.ClientException] API Call eventually failed; 0 endpoints tried; See .InnerException aggregate";

    public override string GetTopic(GDID parentGdid) => "blogic";

    public override MessageType GetType(GDID parentGdid) => MessageType.Error;

    public override Exception GetException(GDID parentGdid)
    {
      return new ArgumentOutOfRangeException("Something bad happened!", new ArgumentNullException("I think we forgot something!"));
    }
  }


}
