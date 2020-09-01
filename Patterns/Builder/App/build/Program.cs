using System;
using build.Models;

namespace build
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("***Builder Pattern Demo***");
      Director director = new Director();
      IBuilder b1 = new Car("Ford");
      IBuilder b2 = new MotorCycle("Honda");
      // Making Car
      director.Construct(b1);
      Product p1 = b1.GetVehicle();
      p1.Show();
      //Making MotorCycle
      director.Construct(b2);
      Product p2 = b2.GetVehicle();
      p2.Show();
      Console.ReadLine();
    }
  }
}
