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

      Director.Construct(GetRandomBuilder(), ref m_Instance);

      return this;
    }

    private static IMessageBuilder GetRandomBuilder()
    {
      // TODO: Set up random builder scenarios here!!!!!!!!!

      return new DeleteFilesBuilder();
    }
  }

  internal static class Director
  {
    public static void Construct(IMessageBuilder builder, ref Message message)
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
    public string GetArchiveDimensions(GDID parentGdid) => null;
    public abstract Atom GetChannel(GDID parentGdid);
    public Exception GetException(GDID parentGdid) => null;
    public abstract string GetFrom(GDID parentGdid);

    public string GetHost(GDID parentGdid)
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

    public string GetParameters(GDID parentGdid) => null;

    public int GetSource(GDID parentGdid) => 0;
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
}
