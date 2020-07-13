using System;
using System.Collections.Generic;
using System.Linq;
using compose.Models.Concrete;
using compose.Models.GoF;

namespace compose
{
  class Program
  {
    static void Main(string[] args)
    {
      // The traditional GoF simple conceptual example
      // highlighting scalar and composite objects.
      RunGoF();

      ////// The over the top and perhaps a little insane
      ////// advanced version utilizing generics and fair
      ////// bit of abstraction to handle this powerful pattern.
      ////RunRobotGraph();
    }

    // TODO: refactor this to be more clear.

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


    }

    private static void RunRobotGraph()
    {
      /*
        Using generics and some abstraction we can accomplish some very flexible
        and advanced scenarios while working with the composite pattern. Because
        of the combinitory explosion of complexity involved with the composite pattern
        and object graph functionality their is a large amount of scenarios to
        consider. Below are some of the most common use cases, yet it is not exhaustive.
        You may want to review the below several times to fully appreciate all of the
        scenarios possible with this pattern.
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
