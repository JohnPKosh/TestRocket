using System;
using AbstractFactoryLogic.Common;
using AbstractFactoryLogic.Enums;
using AbstractFactoryLogic.Logic;
using AbstractFactoryLogic.Models;
using AbstractFactoryLogic.Models.Interfaces;

namespace AbstractFactoryTests.Fixtures
{
  public class ToyFixture : IDisposable
  {
    public ToyFixture()
    {
      sut_Passenger = PassengerCreator.GetToy(GravityType.Normal);
      sut_ZeroGPassenger = PassengerCreator.GetToy(GravityType.Weightless);

      sut_NormalTypeExpected = typeof(Toy);
      sut_ZeroGTypeExpected = typeof(WeightlessToy);
    }

    public readonly string sut_SpeakExpected = FactoryConstants.TOY_SPK;

    public readonly string sut_LaunchExpected = FactoryConstants.TOY_LAUNCH;

    public readonly string sut_SpeakZeroGExpected = FactoryConstants.TOY_SPK_ZERO_G;

    public readonly string sut_LaunchZeroGExpected = FactoryConstants.TOY_LAUNCH_ZERO_G;

    public readonly Type sut_NormalTypeExpected;

    public readonly Type sut_ZeroGTypeExpected;

    public IPassenger sut_Passenger { get; set; }

    public IPassenger sut_ZeroGPassenger { get; set; }


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
