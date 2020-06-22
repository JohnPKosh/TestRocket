namespace decorate.Models
{
  interface IDecorate
  {
    string Apply();

    string Location { get; set; }
  }
}
