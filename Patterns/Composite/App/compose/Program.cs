using System;
using System.Collections.Generic;
using System.Linq;
using compose.Models.Concrete;
using compose.Models.Generic;
using compose.Models.GoF;
using compose.Models.Media;

namespace compose
{
  class Program
  {
    static void Main(string[] args)
    {
      //RunGeneric();
      //RunGenericWithNodeBase();
      //RunPlaylist();
      //RunGoF();
      RunRobotTree();
    }

    private static void RunGoF()
    {
      // Create a tree structure
      Composite root = new Composite("root");
      root.Add(new Leaf("Leaf A"));
      root.Add(new Leaf("Leaf B"));

      Composite comp = new Composite("Composite X");
      comp.Add(new Leaf("Leaf XA"));
      comp.Add(new Leaf("Leaf XB"));

      root.Add(comp);
      root.Add(new Leaf("Leaf C"));

      // Add and remove a leaf
      Leaf leaf = new Leaf("Leaf D");
      root.Add(leaf);
      root.Remove(leaf);

      // Recursively display tree
      root.Display(1);

      // Wait for user
      Console.ReadKey();
    }

    private static void RunPlaylist()
    {
      var folder = new CompositeNode("Home");

      var balladsFolder = new CompositeNode("Ballads");
      balladsFolder.Add(new NodeItem("", "Mötley Crue - Without You",1));
      balladsFolder.Add(new NodeItem("", "Napalm Death - Evolved As One",2));
      balladsFolder.Add(new NodeItem("", "Poison - Something To Believe In",3));
      folder.Add(balladsFolder);

      var thrashFolder = new CompositeNode("Thrash");
      thrashFolder.Add(new NodeItem("", "Kreator - Violent Revolution",4));
      thrashFolder.Add(new NodeItem("", "Exodus - War Is My Shepherd",5));
      thrashFolder.Add(new NodeItem("", "Metallica - Whiplash",6));
      folder.Add(thrashFolder);

      Console.WriteLine("\r\nPlaylist:");
      var playlist = new int[] { 1, 3, 6 };
      var items = folder.FindLeafNodes(i => playlist.Contains(i.Id));
      foreach (var item in items)
        Console.WriteLine(item.Name);

      Console.WriteLine("\r\nFolders:");
      var folders = folder.FindCompositeNodes(i => i is CompositeNode);
      foreach (var f in folders)
        Console.WriteLine(f.Name);

      Console.WriteLine(" \r\nPress any key to continue ...");
      Console.ReadKey(true);
    }

    private static void RunGeneric()
    {
      var dad = new CompositeNode<string>
      {
        Value = "John"
      };

      var mom = new CompositeNode<string>
      {
        Value = "Wendy"
      };

      dad.ConnectTo(mom);

      var childNames = new List<string>() { "Parker", "Sierra" };
      //var children = new ChildNodes<string>(childNames);
      mom.CreateNewLeaves(childNames);

      var allNodes = dad.GetDescendents().ToList();
      var findNodes =dad.FindNodes(x => x == x).ToList();
      var findParkerNodes = dad.FindNodes(x => x.Value == "Parker").ToList();
      var compNodes = dad.FindCompositeNodes(x => x == x).ToList();
      var leafNodes = dad.FindLeafNodes(x => x == x).ToList();

      Console.WriteLine("In: {0} / Out {1}", mom.In.Count, mom.Out.Count);
    }

    private static void RunGenericWithNodeBase()
    {
      var dad = new CompositeNode<NodeBase>(new NodeItem(string.Empty, "John", 1));
      var mom = new CompositeNode<NodeBase>(new NodeItem(string.Empty, "Wendy", 2));
      var pc = new LeafNode<NodeBase>(new NodeItem(string.Empty, "Computer", 6));

      dad.ConnectTo(mom);
      dad.ConnectTo(pc);

      mom.ConnectTo(new LeafNode<NodeBase>(new NodeItem(string.Empty, "Parker", 3)));
      mom.ConnectTo(new LeafNode<NodeBase>(new NodeItem(string.Empty, "Sierra", 4)));

      var alien = new CompositeNode<NodeBase>(new NodeItem(string.Empty, "Bsdlkfjewkj", 5));
      dad.ReParent(alien, mom);

      mom.ReParentChildren(dad);
      //mom.ReParent(dad);

      dad.ReParentChildren(alien, x => x.Value.Name == "Computer");
      //dad.ReParent(alien, x => x.Value.Name == "Computer");

      mom.CreateNewLeaf(new NodeItem(string.Empty, "Findley", 7));

      Console.WriteLine("In: {0} / Out {1}", mom.In.Count, mom.Out.Count);
    }


    // TODO: consider mixing the below with the prototype pattern for some interesting code?

    private static void RunRobotTree()
    {
      var root = new RobotContainer("ROOT");

      var autoRobots = new RobotContainer("Fully Autonomous Robots");
      var autoList = new List<Robot>
      {
        new Robot(new RobotChassis() { ArmCount = 3 }, "Larry", "3 stooges"),
        new Robot(new RobotChassis() { ArmCount = 4 }, "Curly", "3 stooges"),
        new Robot(new RobotChassis() { ArmCount = 5 }, "Moe", "3 stooges")
      };
      autoRobots.ConnectTo(autoList);

      var defectRobots = new RobotContainer("Faulty Robots");
      var r4 = new Robot(new RobotChassis() { ArmCount = 13 }, "Shemp", "3 stooges");
      defectRobots.ConnectTo(r4);

      root.ConnectTo(autoRobots);
      root.ConnectTo(defectRobots);


      var robotItems = root.FindLeafNodes(x => x.Meta.DisplayName != "Shemp" && x is Robot); // we would only need to check RobotInstance if we had other polymorphic classes to consider.

      Console.WriteLine("\r\nGetting the more popular stooges.");

      foreach (var r in robotItems)
      {
        // We get an instance name from the metadata.
        // The arm count is a property of our generic T type of Robot.
        // This is where the potential madness begins with
        // all of the possible composite options.
        Console.WriteLine("{0} has {1} arms!",r.Meta.DisplayName, r.Value.ArmCount);
      }

      var faultyContainers = root.FindCompositeNodes(x => x.Meta.DisplayName == "Faulty Robots");

      Console.WriteLine("\r\nGetting the faulty stooges.");
      foreach (var c in faultyContainers)
      {
        // Now we go and lose all sanity...
        var faultyBots = c.FindLeafNodes(x => x is Robot);
        foreach (var f in faultyBots)
        {
          Console.WriteLine("{0} has {1} arms!", f.Meta.DisplayName, f.Value.ArmCount);
        }
      }

      Console.WriteLine("\r\nknuck, knuck, knuck!");
    }
  }
}
