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
      GatherMultiDimensionalSpecimens();
      GatherInitializedMultiDimensionalSpecimens();
      GatherArrayOfArrays();
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
    /// The space module's Array contains 3 tanks for storing the alien names, one for each cryo tank.
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
        if (aliens[i] != null) // Check for null since the alien Array has a Length, but the objects may be null
        {
          con($"cryo tank #{i} contains {aliens[i]}");
        }
        else
        {
          con($"cryo tank #{i} contains NO SPECIMEN!");
        }
      }
      // Notice that aliens[2] == null since we never set it's value in the module's memory
    }

    /// <summary>
    /// Creates a multidimensional Array for cryo tank management
    /// </summary>
    /// <remarks>
    /// Our astronauts while capturing specimens on a remote world will need to 
    /// collect an array of alien life forms to place in their space module's cryo
    /// tanks (which there are only 3, each with 2 inner containers *** Room for 6 total aliens ***). 
    /// The space module's computer is capable of storing a name for each alien in a base 
    /// class object called an "Array" with 2 dimensions.
    /// </remarks>
    private static void GatherMultiDimensionalSpecimens()
    {
      hr();
      con("Prepare Multidimensional cryo tanks!");
      hr();

      con("Add specimens");

      // *** Declare a two dimensional array. ***
      // ****************************************

      string[,] aliens = new string[3, 2];
      aliens[0, 0] = "Fizz 1";
      aliens[0, 1] = "Fizz 2";
      aliens[1, 0] = "Buzz 1";
      aliens[1, 1] = "Buzz 2";

      // ****************************************

      hr();
      con("Examine Multidimensional specimens!");
      hr();
      examineTwoDimensionalArray(aliens);  // See helper method below
    }

    /// <summary>
    /// Creates a multidimensional Array for cryo tank management *** Created with object initializer syntax
    /// </summary>
    /// <remarks>
    /// Our astronauts while capturing specimens on a remote world will need to 
    /// collect an array of alien life forms to place in their space module's cryo
    /// tanks (which there are only 3, each with 2 inner containers *** Room for 6 total aliens ***). 
    /// The space module's computer is capable of storing a name for each alien in a base 
    /// class object called an "Array" with 2 dimensions.
    /// </remarks>
    private static void GatherInitializedMultiDimensionalSpecimens()
    {
      hr();
      con("Prepare Multidimensional cryo tanks with object initilizer syntax!");
      hr();

      con("Add specimens");

      // *** Declare and set array element values. Note - the Array will be automatically sized correctly based on initial values. ***
      // ****************************************

      string[,] aliens = { { "Fizz 1", "Fizz 2" }, { "Buzz 1", "Buzz 2" }, { "Cattywampus 1", "Cattywampus 2" } };

      // ****************************************

      hr();
      con("Examine Multidimensional specimens!");
      hr();
      examineTwoDimensionalArray(aliens); // See helper method below
    }

    /// <summary>
    /// Private exam method used by *** GatherMultiDimensionalSpecimens() and GatherInitializedMultiDimensionalSpecimens() ***
    /// </summary>
    /// <param name="aliens">A two dimensional Array of strings</param>
    private static void examineTwoDimensionalArray(string[,] aliens)
    {
      // First we get the length of dimension 0 and begin iterating.
      for (int i = 0; i < aliens.GetLength(0); i++)
      {
        // Next we get the length of dimension 1 and iterating our individual values.
        for (int j = 0; j < aliens.GetLength(1); j++)
        {
          // HACK: Using double Elvis operator syntactic sugar here (The null-coalescing operator)
          con($"cryo tank #{i}:{j} contains {aliens[i, j] ?? "NO SPECIMEN!"}");
        }
      }
    }

    /// <summary>
    /// This method creates a Jagged Array (or Array of Arrays)
    /// </summary>
    /// <remarks>
    /// Our astronauts while capturing specimens on a remote world will need to 
    /// collect an array of alien life forms to place in their space module's cryo
    /// tanks (which there are only 3 in each space module). The space module's computer
    /// is capable of storing an Array of names for each alien in each of the 3 cryo tanks.
    /// 
    /// Using a Jagged Array we may not know how many names will fit in each cryo tank.
    /// Perhaps some cryo tanks can hold more aliens than others since aliens may come in
    /// different sizes?
    /// </remarks>
    public static void GatherArrayOfArrays()
    {
      hr();
      con("Prepare Jagged Array of cryo tanks with object initilizer syntax!");
      hr();

      con("Add specimens");

      // *** Declare a Jagged Array. ***
      // ****************************************

      // We can only set the length on the first Array, since the child Array objects can be of any length
      string[][] aliens = new string[3][];
      aliens[0] = new string[] { "Fizz 1", "Fizz 2" };
      aliens[1] = new string[] { "Buzz 1", "Buzz 2" };

      /*
        Since we created each of the above arrays in our "Jagged Array" with a consistent length (only enforced by our logic)
        this could also be referred to as a "Rectangular Array".  
      */

      hr();
      con("Examine specimens!");
      hr();

      for (int i = 0; i < aliens.Length; i++)
      {
        if (aliens[i] != null) // Check for null since the alien Array has a Length, but the objects may be null
        {
          con($"cryo tank #{i} contains {aliens[i].Length} aliens"); // If we want to display the string values we need to iterate again here
        }
        else
        {
          con($"cryo tank #{i} contains NO SPECIMENS!");
        }
      }
      // Notice that aliens[2] == null since we never set it's value in the module's memory
    }

    #region Private Helpers

    // Helpers to make things easier to read above.
    private static void hr() => Console.WriteLine("\n**********************************\n");
    private static void con(string text) => Console.WriteLine(text);

    #endregion
  }
}