﻿using System;
using System.Collections.Generic;
using System.Text;

namespace build.Models
{
  // "Director"
  class Director
  {
    IBuilder builder;
    // A series of steps-in real life, steps are complex.
    public void Construct(IBuilder builder)
    {
      this.builder = builder;
      builder.StartUpOperations();
      builder.BuildBody();
      builder.InsertWheels();
      builder.AddHeadlights();
      builder.EndOperations();
    }
  }

  // "Product"
  class Product
  {
    // We can use any data structure that you prefer e.g.List<string> etc.
    private LinkedList<string> parts;
    public Product()
    {
      parts = new LinkedList<string>();
    }
    public void Add(string part)
    {
      //Adding parts
      parts.AddLast(part);
    }
    public void Show()
    {
      Console.WriteLine("\nProduct completed as below :");
      foreach (string part in parts)
        Console.WriteLine(part);
    }
  }

  // Builders common interface
  interface IBuilder
  {
    void StartUpOperations();
    void BuildBody();
    void InsertWheels();
    void AddHeadlights();
    void EndOperations();
    Product GetVehicle();
  }


  // ConcreteBuilder: Car
  class Car : IBuilder
  {
    private string brandName;
    private Product product;
    public Car(string brand)
    {
      product = new Product();
      this.brandName = brand;
    }
    public void StartUpOperations()
    { //Starting with brandname
      product.Add(string.Format("Car Model name :{0}", this.
      brandName));
    }
    public void BuildBody()
    {
      product.Add("This is a body of a Car");
    }
    public void InsertWheels()
    {
      product.Add("4 wheels are added");
    }
    public void AddHeadlights()
    {
      product.Add("2 Headlights are added");
    }
    public void EndOperations()
    { //Nothing in this case
    }
    public Product GetVehicle()
    {
      return product;
    }
  }

  // ConcreteBuilder:Motorcycle
  class MotorCycle : IBuilder
  {
    private string brandName;
    private Product product;
    public MotorCycle(string brand)
    {
      product = new Product();
      this.brandName = brand;
    }
    public void StartUpOperations()
    { //Nothing in this case
    }
    public void BuildBody()
    {
      product.Add("This is a body of a Motorcycle");
    }
    public void InsertWheels()
    {
      product.Add("2 wheels are added");
    }
    public void AddHeadlights()
    {
      product.Add("1 Headlights are added");
    }
    public void EndOperations()
    {
      //Finishing up with brandname
      product.Add(string.Format("Motorcycle Model name :{0}",
      this.brandName));
    }
    public Product GetVehicle()
    {
      return product;
    }
  }


}
