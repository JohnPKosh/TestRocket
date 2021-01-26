﻿using System;

using Azos;
using Azos.Data;
using Azos.Text;
using Azos.Serialization.Bix;

namespace ArchiveData.Models
{
  /// <summary>
  /// Public enum for identifying the known or unknown sex of an individual
  /// </summary>
  public enum Sex { Male, Female, Unspecified };


  /// <summary>
  /// An abstract model to inherit from for fake sample data used for archival testing
  /// </summary>
  public abstract class FakeRow : TypedDoc
  {
    public FakeRow() { }

    [Field(backendName: "_id")] public GDID ID { get; set; }

    public abstract FakeRow Populate(GDID parentGdid);

    #region private methods

    protected static string[] getFakeTags()
    {
      var tags = Ambient.Random.NextRandomInteger > 0 ? new string[Ambient.Random.NextScaledRandomInteger(1, 20)] : null;
      if (tags != null)
        for (int i = 0; i < tags.Length; i++)
        {
          tags[i] = ((char)('a' + i)) + "tag";
        }

      return tags;
    }

    protected static int getFakeAge(GDID parentGdid)
    {
      return (int)(parentGdid.ID % 99);
    } 

    #endregion
  }

  #region Concrete FakeRow Models

  /// <summary>
  /// A fake person concrete implementation
  /// </summary>
  [Bix("DCAE2B17-FE63-46D4-AEDD-A47C9ECBADC3")]
  public class PersonRow : FakeRow
  {
    public PersonRow() : base() { }

    [Field(backendName: "nm")] public string Name { get; set; }
    [Field(backendName: "age")] public int Age { get; set; }
    [Field(backendName: "dob")] public DateTime DOB { get; set; }
    [Field(backendName: "sex")] public Sex Sex { get; set; }
    [Field(backendName: "inc")] public decimal Income { get; set; }
    [Field(backendName: "deb")] public decimal Debt { get; set; }
    [Field(backendName: "rat")] public double? Rating { get; set; }
    [Field(backendName: "n")] public string Notes { get; set; }
    [Field(backendName: "vot")] public bool? Voter { get; set; }
    [Field(backendName: "mil")] public bool? MilitarySvc { get; set; }
    [Field(backendName: "a1")] public string Address1 { get; set; }
    [Field(backendName: "a2")] public string Address2 { get; set; }
    [Field(backendName: "ac")] public string City { get; set; }
    [Field(backendName: "as")] public string State { get; set; }
    [Field(backendName: "az")] public string Zip { get; set; }
    [Field(backendName: "p1")] public string Phone1 { get; set; }
    [Field(backendName: "p2")] public string Phone2 { get; set; }
    [Field(backendName: "e1")] public string Email1 { get; set; }
    [Field(backendName: "e2")] public string Email2 { get; set; }
    [Field(backendName: "url")] public string URL { get; set; }
    [Field(backendName: "tg")] public string[] Tags { get; set; }

    public override PersonRow Populate(GDID parentGdid)
    {
      int age = getFakeAge(parentGdid);

      ID = parentGdid;
      Name = NaturalTextGenerator.GenerateFullName(true);
      Age = age;
      DOB = DateTime.Now.AddYears(-age);
      Sex = (parentGdid.ID % 2) == 0 ? Sex.Male : Sex.Female;
      Income = (parentGdid.ID % 79) * 1000;
      Debt = (parentGdid.ID % 11) * 1000;
      Rating = (parentGdid.ID % 2) == 0 ? (double?)null : 3.25;
      Notes = parentGdid.ToString();
      Voter = (parentGdid.ID % 2) == 0 ? (bool?)null : true;
      MilitarySvc = (parentGdid.ID % 2) == 0 ? (bool?)null : false;
      Address1 = NaturalTextGenerator.GenerateAddressLine();
      Address2 = (parentGdid.ID % 7) == 0 ? NaturalTextGenerator.GenerateAddressLine() : null;
      City = NaturalTextGenerator.GenerateCityName();
      State = "OH";
      Zip = "44000" + (parentGdid.ID % 999);
      Phone1 = "(555) 222-3222";
      Phone2 = (parentGdid.ID % 3) == 0 ? "(555) 737-9789" : null;
      Email1 = NaturalTextGenerator.GenerateEMail();
      Email2 = (parentGdid.ID % 5) == 0 ? NaturalTextGenerator.GenerateEMail() : null;
      URL = (parentGdid.ID % 2) == 0 ? "https://ibm.com/products/" + parentGdid.ID : null;
      Tags = getFakeTags();
      return this;
    }
  }


  /// <summary>
  /// A fake email message concrete implementation
  /// </summary>
  [Bix("A25C82BC-137B-4D70-ADB0-248D77740044")]
  public class EmailRow : FakeRow
  {
    public EmailRow() : base() { }

    [Field(backendName: "dn")] public string DisplayName { get; set; }
    [Field(backendName: "frm")] public string FromEmail { get; set; }
    [Field(backendName: "to")] public string ToEmail { get; set; }
    [Field(backendName: "cc")] public string CcEmail { get; set; }
    [Field(backendName: "bcc")] public string BccEmail { get; set; }
    [Field(backendName: "sd")] public DateTime SentDate { get; set; }
    [Field(backendName: "hst")] public string Host { get; set; }
    [Field(backendName: "sb")] public string Subject { get; set; }
    [Field(backendName: "body")] public string Body { get; set; }
    [Field(backendName: "tg")] public string[] Tags { get; set; }

    public override EmailRow Populate(GDID parentGdid)
    {
      ID = parentGdid;
      DisplayName = NaturalTextGenerator.GenerateFullName(true);
      FromEmail = NaturalTextGenerator.GenerateEMail();
      ToEmail = NaturalTextGenerator.GenerateEMail();
      CcEmail = (parentGdid.ID % 5) == 0 ? NaturalTextGenerator.GenerateEMail() : null;
      BccEmail = (parentGdid.ID % 10) == 0 ? NaturalTextGenerator.GenerateEMail() : null;
      Host = $"127.0.0.{Ambient.Random.NextScaledRandomInteger(1, 20)}";
      Tags = getFakeTags();
      SentDate = DateTime.Now.AddHours(Ambient.Random.NextScaledRandomInteger(1, 12)).AddMinutes(Ambient.Random.NextScaledRandomInteger(1, 60));
      Subject = NaturalTextGenerator.Generate(50);
      Body = NaturalTextGenerator.Generate(0);
      return this;
    }
  }



  /// <summary>
  /// A fake SMS message concrete implementation
  /// </summary>
  [Bix("20B739E5-9FD0-4F49-87A7-F94F8F58021F")]
  public class SmsRow : FakeRow
  {
    public SmsRow() : base() { }

    [Field(backendName: "frm")] public string From { get; set; }
    [Field(backendName: "to")] public string To { get; set; }
    [Field(backendName: "sd")] public DateTime SentDate { get; set; }
    [Field(backendName: "hst")] public string Host { get; set; }
    [Field(backendName: "body")] public string Body { get; set; }

    public override SmsRow Populate(GDID parentGdid)
    {
      ID = parentGdid;
      From = $"{Ambient.Random.NextScaledRandomInteger(200, 999)}{Ambient.Random.NextScaledRandomInteger(100, 999)}{Ambient.Random.NextScaledRandomInteger(0, 9999):D4}";
      To = $"{Ambient.Random.NextScaledRandomInteger(200, 999)}{Ambient.Random.NextScaledRandomInteger(100, 999)}{Ambient.Random.NextScaledRandomInteger(0, 9999):D4}";
      Host = $"127.0.0.{Ambient.Random.NextScaledRandomInteger(1, 20)}";
      Body = NaturalTextGenerator.Generate(50);
      SentDate = DateTime.Now.AddHours(Ambient.Random.NextScaledRandomInteger(1, 12)).AddMinutes(Ambient.Random.NextScaledRandomInteger(1, 60));
      return this;
    }
  }



  /// <summary>
  /// A fake file target concrete implementation
  /// </summary>
  [Bix("F5F20C69-0BC0-4764-878C-32197AC6AAA3")]
  public class FileTargetRow : FakeRow
  {
    public FileTargetRow() : base() { }

    [Field(backendName: "nm")] public string Name { get; set; }
    [Field(backendName: "fnm")] public string FullName { get; set; }
    [Field(backendName: "cd")] public DateTime CreateDate { get; set; }
    [Field(backendName: "hst")] public string Host { get; set; }
    [Field(backendName: "fid")] public Guid FileId { get; set; }
    [Field(backendName: "len")] public ulong Length { get; set; }

    public override FileTargetRow Populate(GDID parentGdid)
    {
      ID = parentGdid;
      Name = NaturalTextGenerator.GenerateWord(5, 8) + NaturalTextGenerator.GenerateWord(5, 8) + ".txt";
      FullName = $@"c:\{NaturalTextGenerator.GenerateWord(5, 12)}\{NaturalTextGenerator.GenerateWord(3, 8)}\{Name}";
      Host = $"127.0.0.{Ambient.Random.NextScaledRandomInteger(1, 20)}";
      FileId = Guid.NewGuid();
      CreateDate = DateTime.Now.AddHours(Ambient.Random.NextScaledRandomInteger(1, 12)).AddMinutes(Ambient.Random.NextScaledRandomInteger(1, 60));
      Length = Ambient.Random.NextRandomUnsignedLong;
      return this;
    }
  }


  /// <summary>
  /// A fake device location concrete implementation
  /// </summary>
  [Bix("E4E4BE1F-C0D5-4115-9B36-D97894E6D983")]
  public class DeviceLocationRow : FakeRow
  {
    public DeviceLocationRow() : base() { }

    [Field(backendName: "id")] public long DeviceId { get; set; }
    [Field(backendName: "lt")] public double Latitude { get; set; }
    [Field(backendName: "ln")] public double Longitude { get; set; }
    [Field(backendName: "cd")] public DateTime CreateDate { get; set; }


    public override DeviceLocationRow Populate(GDID parentGdid)
    {
      ID = parentGdid;
      DeviceId = Ambient.Random.NextScaledRandomInteger(1, 10_000);
      Latitude = Math.Round(Ambient.Random.NextScaledRandomDouble(-80, 80), 4);
      Longitude = Math.Round(Ambient.Random.NextScaledRandomDouble(-180, 180), 4);
      CreateDate = DateTime.Now.AddHours(Ambient.Random.NextScaledRandomInteger(1, 12)).AddMinutes(Ambient.Random.NextScaledRandomInteger(1, 60));

      return this;
    }
  }


  /// <summary>
  /// A fake EOB concrete implementation
  /// </summary>
  [Bix("7AC0299E-3E35-4A65-9896-CC8C7EAEEFC3")]
  public class EobRow : FakeRow
  {
    public EobRow() : base() { }

    [Field(backendName: "CARRIER")] public string CARRIER { get; set; }
    [Field(backendName: "MEMBERID")] public string MEMBERID { get; set; }
    [Field(backendName: "PERSONCODE")] public short PERSONCODE { get; set; }
    [Field(backendName: "FILLDATE")] public DateTime? FILLDATE { get; set; }
    [Field(backendName: "QUANTITY")] public decimal? QUANTITY { get; set; }
    [Field(backendName: "MPAMARK")] public string MPAMARK { get; set; }
    [Field(backendName: "GROUPID")] public string GROUPID { get; set; }
    [Field(backendName: "COPAY")] public double? COPAY { get; set; }
    [Field(backendName: "ORIGINALCOPAY")] public double? ORIGINALCOPAY { get; set; }
    [Field(backendName: "ENTEREDDATE")] public DateTime? ENTEREDDATE { get; set; }
    [Field(backendName: "LICSLEVEL")] public string LICSLEVEL { get; set; }
    [Field(backendName: "LICS")] public double? LICS { get; set; }
    [Field(backendName: "MONY")] public string MONY { get; set; }
    [Field(backendName: "BILLDISPFEE")] public double? BILLDISPFEE { get; set; }
    [Field(backendName: "BILLCOST")] public double? BILLCOST { get; set; }
    [Field(backendName: "BILLTAX")] public double? BILLTAX { get; set; }
    [Field(backendName: "SDC")] public string SDC { get; set; }
    [Field(backendName: "PPP")] public decimal? PPP { get; set; }
    [Field(backendName: "PTR")] public decimal? PTR { get; set; }
    [Field(backendName: "GROUPBILLCOPAY")] public double? GROUPBILLCOPAY { get; set; }
    [Field(backendName: "NDC")] public string NDC { get; set; }
    [Field(backendName: "DRUGCRITERIA")] public string DRUGCRITERIA { get; set; }
    [Field(backendName: "AUTHNUMBER")] public string AUTHNUMBER { get; set; }
    [Field(backendName: "REVERSEDAUTH")] public string REVERSEDAUTH { get; set; }
    [Field(backendName: "TIER")] public string TIER { get; set; }
    [Field(backendName: "COPAYDED")] public double? COPAYDED { get; set; }
    [Field(backendName: "COPAYUG")] public double? COPAYUG { get; set; }
    [Field(backendName: "COPAYIG")] public double? COPAYIG { get; set; }
    [Field(backendName: "COPAYOG")] public double? COPAYOG { get; set; }
    [Field(backendName: "LICSDED")] public double? LICSDED { get; set; }
    [Field(backendName: "LICSUG")] public double? LICSUG { get; set; }
    [Field(backendName: "LICSIG")] public double? LICSIG { get; set; }
    [Field(backendName: "LICSOG")] public double? LICSOG { get; set; }
    [Field(backendName: "PLANPAYDED")] public double? PLANPAYDED { get; set; }
    [Field(backendName: "PLANPAYUG")] public double? PLANPAYUG { get; set; }
    [Field(backendName: "PLANPAYIG")] public double? PLANPAYIG { get; set; }
    [Field(backendName: "PLANPAYOG")] public double? PLANPAYOG { get; set; }
    [Field(backendName: "NONTRCOPDED")] public double? NONTRCOPDED { get; set; }
    [Field(backendName: "NONTRCOPUG")] public double? NONTRCOPUG { get; set; }
    [Field(backendName: "NONTRCOPIG")] public double? NONTRCOPIG { get; set; }
    [Field(backendName: "NONTRCOPOG")] public double? NONTRCOPOG { get; set; }
    [Field(backendName: "RXNUMBER")] public long? RXNUMBER { get; set; }
    [Field(backendName: "NABP")] public string NABP { get; set; }
    [Field(backendName: "OTHERTROOP")] public double? OTHERTROOP { get; set; }
    [Field(backendName: "F406")] public decimal? F406 { get; set; }
    [Field(backendName: "SCRIPTTAG")] public string SCRIPTTAG { get; set; }
    [Field(backendName: "UT004")] public string UT004 { get; set; }
    [Field(backendName: "UT024")] public string UT024 { get; set; }
    [Field(backendName: "UM028")] public double? UM028 { get; set; }
    [Field(backendName: "SUBGROUP")] public string SUBGROUP { get; set; }
    [Field(backendName: "SUBGROUP")] public string FORMULARYID { get; set; }
    [Field(backendName: "SUBGROUP")] public string F107 { get; set; }

    public override EobRow Populate(GDID parentGdid)
    {
      var dt = DateTime.Now.AddHours(Ambient.Random.NextScaledRandomInteger(1, 12)).AddMinutes(Ambient.Random.NextScaledRandomInteger(1, 60));

      CARRIER = $"R{Ambient.Random.NextScaledRandomInteger(100, 300)}";
      MEMBERID = $"{Ambient.Random.NextScaledRandomInteger(10_0000, 2_000_000) }*{NaturalTextGenerator.GenerateWord(3, 6)}";
      PERSONCODE = (parentGdid.ID % 2) == 0 ? 1 : (short)Ambient.Random.NextScaledRandomInteger(1, 4);
      FILLDATE = dt;
      QUANTITY = (parentGdid.ID % 2) == 0 ? 30 : (parentGdid.ID % 3) == 0 ? 90 : 60;
      MPAMARK = (parentGdid.ID % 4) == 0 ? "X" : null;
      GROUPID = Ambient.Random.NextScaledRandomInteger(10_0000, 20_000).ToString();

      COPAYDED = (parentGdid.ID % 2) == 0 ? Math.Round(Ambient.Random.NextScaledRandomDouble(1, 5_000), 2) : 0;
      COPAYIG = (parentGdid.ID % 11) == 0 ? Math.Round(Ambient.Random.NextScaledRandomDouble(1, 5_000), 2) : 0;
      COPAYOG = (parentGdid.ID % 10) == 0 ? Math.Round(Ambient.Random.NextScaledRandomDouble(1, 5_000), 2) : 0;
      COPAYUG = (parentGdid.ID % 9) == 0 ? Math.Round(Ambient.Random.NextScaledRandomDouble(1, 5_000), 2) : 0;

      COPAY = COPAYDED + COPAYIG + COPAYOG + COPAYUG;
      ORIGINALCOPAY = COPAY;
      ENTEREDDATE = dt;
      LICS = Ambient.Random.NextScaledRandomInteger(0, 5);
      LICSLEVEL = LICS.ToString();
      MONY = (parentGdid.ID % 5) == 0 ? "M" : null;
      BILLDISPFEE = COPAY > 50 ? Math.Round(Ambient.Random.NextScaledRandomDouble(1, 5), 2) : null;
      BILLCOST = COPAY.HasValue ? Math.Round(Ambient.Random.NextScaledRandomDouble(1, COPAY.Value), 2) : null;
      BILLTAX = (parentGdid.ID % 20) == 0 ? Math.Round(Ambient.Random.NextScaledRandomDouble(1, 2), 2) : null;
      SDC = (parentGdid.ID % 50) == 0 ? Ambient.Random.NextScaledRandomInteger(0, 5) > 2 ? "xxxxxxxxxxxxxxx" : "zzzzzz" : null;
      PPP = SDC.IsNotNullOrWhiteSpace() ? (decimal)Math.Round(Ambient.Random.NextScaledRandomDouble(0, 2), 2) : null;
      PTR = PPP;
      GROUPBILLCOPAY = QUANTITY == 90 ? BILLCOST : null;
      NDC = Math.Round(Ambient.Random.NextScaledRandomDouble(0, 10_000_000_000), 0).ToString("00000000000");
      DRUGCRITERIA = (parentGdid.ID % 12) == 0 ? "Y" : null;
      AUTHNUMBER = Math.Round(Ambient.Random.NextScaledRandomDouble(0, 10_000_000_000), 0).ToString("000000000000000");
      REVERSEDAUTH = (parentGdid.ID % 5000) == 0 ? "R" + Math.Round(Ambient.Random.NextScaledRandomDouble(0, 10_000_000_000), 0).ToString("000000000000000") : null;
      TIER = Ambient.Random.NextScaledRandomInteger(1, 6).ToString();
      LICSDED = (parentGdid.ID % 12) == 0 ? Math.Round(Ambient.Random.NextScaledRandomDouble(1, 5_000), 2) : null;
      LICSIG = 0;
      LICSOG = 0;
      LICSUG = 0;
      PLANPAYDED = COPAY.HasValue && COPAY.Value > 500 ? Math.Round(Ambient.Random.NextScaledRandomDouble(1, 500), 2) : COPAYDED;
      PLANPAYUG = COPAYUG;
      PLANPAYIG = COPAYIG;
      PLANPAYOG = COPAYOG;
      RXNUMBER = Math.Abs((long)Ambient.Random.NextRandomUnsignedLong);
      NABP = Math.Round(Ambient.Random.NextScaledRandomDouble(0, 10_000_000_000), 0).ToString("00000000000");
      OTHERTROOP = 0;
      //F406 = null;
      SCRIPTTAG = (parentGdid.ID % 200) == 0 ? "xxxx" : null;
      //UT004 = null;
      //UT024 = null;
      //UM028 = null;
      SUBGROUP = GROUPID;
      FORMULARYID = Ambient.Random.NextScaledRandomInteger(2021000, 2021999).ToString();
      //F107 = null;

      ID = parentGdid;

      return this;
    }
  }

  #endregion
}
