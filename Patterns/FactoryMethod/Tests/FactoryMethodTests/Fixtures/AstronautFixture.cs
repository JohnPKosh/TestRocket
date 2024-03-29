﻿using System;
using FactoryMethodLogic.Common;
using FactoryMethodLogic.Enums;
using FactoryMethodLogic.Logic;
using FactoryMethodLogic.Models.Interfaces;

namespace FactoryMethodTests.Fixtures
{
  public class AstronautFixture : IDisposable
  {
    public AstronautFixture()
    {
      sut_Passenger = PassengerCreator.Create(PassengerType.Astronaut);
    }

    public readonly string sut_SpeakExpected = FactoryConstants.AST_SPK;

    public readonly string sut_LaunchExpected = FactoryConstants.AST_LAUNCH;

    public IPassenger sut_Passenger {get;set;}

    #region IDisposable Support

    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          // TODO: dispose managed state (managed objects).
        }

        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.

        disposedValue = true;
      }
    }

    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    // ~ToyFixture()
    // {
    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //   Dispose(false);
    // }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
      // TODO: uncomment the following line if the finalizer is overridden above.
      // GC.SuppressFinalize(this);
    }

    #endregion
  }
}
