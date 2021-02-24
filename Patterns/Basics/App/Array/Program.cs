using System;

namespace Array
{
  class Program
  {
    /// <summary>
    /// The main entry point of the console application.
    /// </summary>
    /// <param name="args">This is an array of string objects passed in from the command line arguments</param>
    static void Main(string[] args)  /* Arrays are everywhere! Even in the default console app template. See the "string[]" method property declaration of Program.Main */
    {
      Console.WriteLine("Hello World! You passed in {0} command arguments.", args.Length);

      GatherSpecimens();
    }

    /// <summary>
    /// Creates a single Array for cryo tank management
    /// </summary>
    /// <remarks>
    /// Our astronauts while capturing specimens on a remote world will need to 
    /// collect an array of alien life forms to place in their space module's cryo
    /// tanks (which there are only 3 in each space module). The space module's computer
    /// is capable of storing a name for each alien in a base class object called
    /// an "Array".
    /// 
    /// The space module's Array contains 3 slots for storing the alien names, one for each cryo tank.
    /// The Array object much like the cryo tank needs to be constructed with a set length (number)
    /// of objects it will hold.
    /// </remarks>
    private static void GatherSpecimens()
    {
      hr();
      con("Prepare cryo tanks!");
      hr();

      // Here we create a new Array of strings to store our descriptions. We must supply a type and length (e.g. new string[3]) when initializing an Array.
      string[] aliens = new string[3];

      // Now that we have an Array with the same number as our cryo tanks we can access the Array's member Indexer getters and setters.
      // You access Array objects using an indexer that simply requires you to specify the index number of the item you want to access (zero based)

      con("Add specimens");
      // Let's add some new life to our Array
      aliens[0] = "Fizz";  // Our astronauts add an alien life in the first cryo tank (Arrays are always Zero based)
      aliens[1] = "Buzz";  // Our astronauts find another alien and add it to the second tank

      // Since our astronauts were lazy they only captured 2 new life forms our third crypto tank remains empty (aliens[2] == null)

      hr();
      con("Examine specimens!");
      hr();

      for (int i = 0; i < aliens.Length; i++)
      {
        con($"cryo tank #{i} contains {aliens[i]}");
      }

      // Notice that aliens[2] == null since we never set it's value in the module's memory
    }


    // Helpers to make things easier to read above.
    private static void hr() => Console.WriteLine("\n**********************************\n");
    private static void con(string text) => Console.WriteLine(text);

  }
}
