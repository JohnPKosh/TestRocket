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
      //RunGoF();
      //RunGeneric();
      //RunGenericWithNodeBase();
      //RunPlaylist();
      RunRobotTree();
    }

    private static void RunGoF()
    {
      // Create a tree structure
      var root = new Composite("root");
      root.Add(new Leaf("Leaf A"));
      root.Add(new Leaf("Leaf B"));

      var comp = new Composite("Composite X");
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

      dad.AddChildren(mom);

      var childNames = new List<string>() { "Parker", "Sierra" };
      //var children = new ChildNodes<string>(childNames);
      mom.CreateNewLeaves(childNames);

      var allNodes = dad.GetDescendents().ToList();
      var findNodes =dad.FindNodes(x => x == x).ToList();
      var findParkerNodes = dad.FindNodes(x => x.Value == "Parker").ToList();
      var compNodes = dad.FindCompositeNodes(x => x == x).ToList();
      var leafNodes = dad.FindLeafNodes(x => x == x).ToList();

      Console.WriteLine("In: {0} / Children {1}", mom.Parents.Count, mom.Children.Count);
    }

    private static void RunGenericWithNodeBase()
    {
      var dad = new CompositeNode<NodeBase>(new NodeItem(string.Empty, "John", 1));
      var mom = new CompositeNode<NodeBase>(new NodeItem(string.Empty, "Wendy", 2));
      var pc = new LeafNode<NodeBase>(new NodeItem(string.Empty, "Computer", 6));

      dad.AddChildren(mom);
      dad.AddChildren(pc);

      mom.AddChildren(new LeafNode<NodeBase>(new NodeItem(string.Empty, "Parker", 3)));
      mom.AddChildren(new LeafNode<NodeBase>(new NodeItem(string.Empty, "Sierra", 4)));

      var alien = new CompositeNode<NodeBase>(new NodeItem(string.Empty, "Bsdlkfjewkj", 5));
      dad.ReParentChildren(alien, mom);

      mom.ReParentChildren(dad);
      //mom.ReParent(dad);

      dad.ReParentChildrenWhere(alien, x => x.Value.Name == "Computer");
      //dad.ReParent(alien, x => x.Value.Name == "Computer");

      mom.CreateNewLeaf(new NodeItem(string.Empty, "Findley", 7));

      Console.WriteLine("In: {0} / Children {1}", mom.Parents.Count, mom.Children.Count);
    }


    private static void RunRobotTree()
    {
      /*
        Using generics and some abstraction we can accomplish some very flexible
        scenarios while working with the composite pattern.
      */

      hr();
      con("Hello Mr. Roboto!");
      hr();

      /* ================================================== */

      // First we will create our root node and add a child container for autonomous robots.
      var root = new RobotContainer("ROOT");
      var autoRobots = new RobotContainer("Fully Autonomous Robots");
      // Then we will add some children to the autoRobots container and add as a child to the root.
      var autoList = new List<Robot>
      {
        new Robot(new RobotChassis() { ArmCount = 3 }, "Larry", "3 stooges"),
        new Robot(new RobotChassis() { ArmCount = 4 }, "Curly", "3 stooges"),
        new Robot(new RobotChassis() { ArmCount = 5 }, "Moe", "3 stooges")
      };
      autoRobots.AddChildren(autoList);
      root.AddChildren(autoRobots);

      hr();
      con("We have created exactly {0} stooges as children of the autoRobots container.", autoRobots.GetDescendents().Count());

      /* ================================================== */

      hr();
      con("Now let's add some more complex branching...");

      // *** Next we will 2 levels of faulty robots and some spare parts.
      var defectRobots = new RobotContainer("Faulty Robots");
      var r4 = new Robot(new RobotChassis() { ArmCount = 13 }, "Shemp", "3 stooges");
      defectRobots.AddChildren(r4);
      // Creating our spare parts container now...
      var spareParts = new RobotContainer("Spare Parts"); // This will be a child of the defective robots.
      spareParts.AddChildren(new Robot(new RobotChassis() { ArmCount = 2 }, "C3PO", "Star Wars"));
      defectRobots.AddChildren(spareParts);
      // Add it all as a child of the root.
      root.AddChildren(defectRobots);

      hr();
      con("The entire graph now has {0} descendents.", root.GetDescendents().Count());

      /* ================================================== */

      hr();
      con("Finding all of the robots except Shemp:");

      // Now we can navigate the tree to find descendents matching a particular condition
      var robotItems = root.FindLeafNodes(x => x.Meta.DisplayName != "Shemp" && x is Robot);
      foreach (var r in robotItems)
      {
        // We get an instance name from the metadata.
        // The arm count is a property of our generic T type of Robot.
        // This is where the potential madness begins with
        // all of the possible composite options.
        con("{0} has {1} arms!",r.Meta.DisplayName, r.Value.ArmCount);
      }

      /* ================================================== */

      hr();
      con("Getting the faulty stooges next:");

      var faultyContainers = root.FindCompositeNodes(x => x.Meta.DisplayName == "Faulty Robots");
      foreach (var c in faultyContainers)
      {
        var faultyBots = c.FindLeafNodes(x => x is Robot); // Now we go and lose all sanity...
        foreach (var f in faultyBots)
        {
          con("{0} has {1} arms!", f.Meta.DisplayName, f.Value.ArmCount);
        }
      }

      /* ================================================== */

      hr();
      con("For giggles we can change the parent of the defect robots to the autonomous robots.");
      defectRobots.ReParent(autoRobots); // debug to see the parent value change.

      /* ================================================== */

      hr();
      con("Finally we want to get Larry and find all of his leaf node siblings on the same level.");

      var larry = autoRobots.FindNodes(x => x.Meta.DisplayName == "Larry").FirstOrDefault();
      hr();
      con("We found {0}, who has several other siblings:", larry.Meta.DisplayName);
      foreach (var s in autoRobots.Children[0].Siblings.Where(x=> x.IsLeaf))
      {
        con("{0} is a sibling of {1}", s.Meta.DisplayName, larry.Meta.DisplayName);
      }

      /* ================================================== */

      hr();
      con("\r\nknuck, knuck, knuck!");
    }


    // Helpers to make things easier to read above.
    private static void hr() => Console.WriteLine("\n**********************************\n");
    private static void con(string text) => Console.WriteLine(text);
    private static void con(string text, params object[] args) => Console.WriteLine(text, args);
  }
}
