using System;
using FactoryMethodLogic.Enums;
using FactoryMethodLogic.Logic;
using FactoryMethodLogic.Models;
using FactoryMethodLogic.Models.Interfaces;

namespace FactoryMethodTests.Fixtures
{
  public class FactoryFixture : IDisposable
  {
    public FactoryFixture()
    {
      sut_ExpectedToy = PassengerCreator.Create(PassengerType.Toy);
      sut_ExpectedAstronaut = PassengerCreator.Create(PassengerType.Astronaut);
      sut_ExpectedCosmonaut = PassengerCreator.Create(PassengerType.Cosmonaut);

      sut_ToyFactory = new ToyFactory();
      sut_AstronautFactory = new AstronautFactory();
      sut_CosmonautFactory = new CosmonautFactory();
    }

    public IPassenger sut_ExpectedToy { get; private set; }

    public IPassenger sut_ExpectedAstronaut { get; private set; }

    public IPassenger sut_ExpectedCosmonaut { get; private set; }

    public IPassengerFactory sut_ToyFactory { get; set; }

    public IPassengerFactory sut_AstronautFactory { get; set; }

    public IPassengerFactory sut_CosmonautFactory { get; set; }

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
