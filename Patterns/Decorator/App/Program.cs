using System;
using decorate.Models;

namespace decorate
{
  class Program
  {
    static void Main(string[] args)
    {
      hr();
      con("Trick out our ride!");
      hr();

      /*
        We have an abstract Decorator class that allows us to Apply() some decorations
        to a particular Location on our rocket. We have both a concrete implementation
        that will Paint or add a Sticker.
      */

      var paint = new Paint
      {
        Location = "Nose Cone",
        Color = "cerullian blue"
      };

      hr();
      con(paint.Apply());
      hr();

      var sticker = new Sticker()
      {
        Location = "rocket engine",
        StickerShape = "square"
      };

      hr();
      con(sticker.Apply());
      hr();

      /*
        Additionally we have added an additional Dynamic Decorator with the CustomSticker
        class that will allow us to decorate our sticker with a custom sticker type.
      */

      var bumperSticker = new CustomSticker(sticker, "bumper");
      hr();
      con(bumperSticker.Apply());
      hr();

      /*
        Finally we have added a Generic Static Module Decorator class that can take any Decorator of T
        that allows us some additional capabilities to decorate our capsule in additional ways.
      */

      var capsulePaintDecorator = new ModuleDecorator<Paint>();
      hr();
      con(capsulePaintDecorator.Apply());
      hr();

      var controlPanelDecorator = new ModuleDecorator<Sticker>("control panel");
      hr();
      con(controlPanelDecorator.Apply());
      hr();

      var rearWindowDecorator = new ModuleDecorator<CustomSticker>("rear window");
      hr();
      con(rearWindowDecorator.Apply());
      hr();

      con("Press any key to abort mission!");
      Console.ReadKey(true);
    }



    // Helpers to make things easier to read above.
    private static void hr() => Console.WriteLine("\n**********************************\n");
    private static void con(string text) => Console.WriteLine(text);
  }
}
