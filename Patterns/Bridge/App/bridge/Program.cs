using System;
using bridge.Models;

namespace bridge
{
  class Program
  {
    static void Main(string[] args)
    {
      /*
        Warning! do not program hungry. Strange results may occur.
      */

      hr();
      con("Prepare for Lunch!");
      hr();

      /*
        We are creating a new instance of some food. We could use DI here
        instead since Food accepts an IDispenser interface.
      */
      var foodDispenser = new FoodDispenser();
      var food = new Food(foodDispenser, "candy bar");

      hr();
      con(food.Vend());
      hr();

      /*
        We are creating a new instance of a drink. We could use DI here
        instead since Food accepts an IDispenser interface.
      */
      var drinkDispenser = new DrinkDispenser();
      var drink = new Drink(drinkDispenser, "Tang");

      hr();
      con(drink.Vend());
      hr();

      /*
        With a pancake we already know it should be dispensed by a
        FoodDispenser and will have a product name of "pancake".
        This allows us to simplify the Pancake concrete implementation
        without modifying the FoodDispenser logic thus creating a bridge
        to the appropriate logic.
      */
      var pancake = new Pancake();

      hr();
      con(pancake.Vend());
      hr();

      /*
        We can do the same for chocolate milk but we equip it with
        a DrinkDispenser implementation instead. Note the use of a
        Lambda operator on the overridden Vend method in ChocolateMilk
        to further simplify our class.
      */
      var chocolateMilk = new ChocolateMilk();

      hr();
      con(chocolateMilk.Vend());
      hr();
    }



    // Helpers to make things easier to read above.
    private static void hr() => Console.WriteLine("\n**********************************\n");
    private static void con(string text) => Console.WriteLine(text);
    private static void con(string text, params object[] args) => Console.WriteLine(text, args);
  }
}
