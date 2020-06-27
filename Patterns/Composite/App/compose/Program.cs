using System;
using System.Collections.Generic;
using System.Linq;
using compose.Models.Generic;
using compose.Models.Media;

namespace compose
{
  class Program
  {
    static void Main(string[] args)
    {
      //RunGeneric();

      RunPlaylist();
    }

    private static void RunPlaylist()
    {
      var folder = new NodeLeaf("Home");

      var balladsFolder = new NodeLeaf("Ballads");
      balladsFolder.Add(new NodeItem("", "Mötley Crue - Without You") { Id = 1 });
      balladsFolder.Add(new NodeItem("", "Napalm Death - Evolved As One") { Id = 2 });
      balladsFolder.Add(new NodeItem("", "Poison - Something To Believe In") { Id = 3 });
      folder.Add(balladsFolder);

      var thrashFolder = new NodeLeaf("Thrash");
      thrashFolder.Add(new NodeItem("", "Kreator - Violent Revolution") { Id = 4 });
      thrashFolder.Add(new NodeItem("", "Exodus - War Is My Shepherd") { Id = 5 });
      thrashFolder.Add(new NodeItem("", "Metallica - Whiplash") { Id = 6 });
      folder.Add(thrashFolder);

      Console.WriteLine("\r\nPlaylist:");
      var playlist = new int[] { 1, 3, 6 };
      var items = folder.Find(i => playlist.Contains(i.Id) && !i.IsLeaf);
      foreach (var item in items)
        Console.WriteLine(item.Name);

      Console.WriteLine(" \r\nPress any key to continue ...");
      Console.ReadKey(true);
    }

    private static void RunGeneric()
    {
      var dad = new Node<string>
      {
        Value = "John"
      };

      var mom = new Node<string>
      {
        Value = "Wendy"
      };

      dad.ConnectTo(mom);

      //var children = new NodeLayer<string>(2);
      //mom.ConnectTo(children);

      var childNames = new List<string>() { "Parker", "Sierra" };
      var children = new ChildNodes<string>(childNames);
      mom.ConnectTo(children);

      Console.WriteLine("In: {0} / Out {1}", mom.In.Count, mom.Out.Count);
    }
  }
}
