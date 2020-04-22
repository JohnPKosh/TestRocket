﻿using factorymethod.Models.Interfaces;

namespace factorymethod.Logic
{
  /// <summary>
  /// Factory method lets a class defer instantiation to subclasses.
  /// The following method is acting like a factory (of creation).</summary>
  public abstract class IPassengerFactory
  {
    public IPassenger NewPassenger()
    {
      return CreatePassenger();
    }

    /// <summary>
    /// The abstract factory method that our IPassengerFactory concrete classes
    /// must implement to create the subordinated factory method IPassenger result.
    /// </summary>
    protected abstract IPassenger CreatePassenger();
  }
}
