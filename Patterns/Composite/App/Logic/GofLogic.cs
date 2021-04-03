using System;

using CompositeLibrary.Infrastructure;
using CompositeLibrary.Infrastructure.GoF;

namespace compose.Logic
{
  public static class GofLogic
  {
    public static void Run()
    {
      hr();
      con("Lets plant a tree to honor our mad scientists.");
      hr();

      // First we will create a new composite root object.
      var root = new Composite("ROOT");

      // Then add some leaves. (leaves cannot have children BTW)
      root.AddChild(new Leaf("Leaf A"));
      root.AddChild(new Leaf("Leaf B"));

      // Next we will add a composite branch that can have children.
      var branch = new Composite("Branch 1");

      // We add it as a child of the root.
      root.AddChild(branch);

      // Since the branch can have children let's add some leaves.
      branch.AddChild(new Leaf("Leaf 1A"));
      branch.AddChild(new Leaf("Leaf 1B"));

      // Let's add some colored leaves next to our branch.
      var brownLeaf = new Leaf("Brown Leaf");
      var greenLeaf = new Leaf("Green Leaf");
      branch.AddChild(brownLeaf);
      branch.AddChild(greenLeaf);

      // Since we want to keep our tree green, let's remone the brown leaf.
      branch.RemoveChild(brownLeaf);

      // Now let's write out our entire tree to JSON
      con(root.ToPrettyJson());

      // Notice - the brown leaf is no longer there.
    }

    // Helpers to make things easier to read above.
    private static void hr() => Console.WriteLine("\n**********************************\n");
    private static void con(string text) => Console.WriteLine(text);
    private static void con(string text, params object[] args) => Console.WriteLine(text, args);
  }
}
