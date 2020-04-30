using System;
using AbstractFactoryLogic.Common;
using AbstractFactoryLogic.Enums;
using AbstractFactoryLogic.Logic;
using AbstractFactoryLogic.Models;
using AbstractFactoryLogic.Models.Interfaces;

namespace AbstractFactoryTests.Fixtures
{
  public class AstronautFixture : IDisposable, IModelFixture
  {
    public AstronautFixture()
    {
      sut_Passenger = PassengerCreator.GetAstronaut(GravityType.Normal);
      sut_ZeroGPassenger = PassengerCreator.GetAstronaut(GravityType.Weightless);

      sut_NormalTypeExpected = typeof(Astronaut);
      sut_ZeroGTypeExpected = typeof(WeightlessAstronaut);
    }

    public string sut_SpeakExpected { get; protected set; } = FactoryConstants.AST_SPK;

    public string sut_LaunchExpected { get; protected set; } = FactoryConstants.AST_LAUNCH;

    public string sut_SpeakZeroGExpected { get; protected set; } = FactoryConstants.AST_SPK_ZERO_G;

    public string sut_LaunchZeroGExpected { get; protected set; } = FactoryConstants.AST_LAUNCH_ZERO_G;

    public Type sut_NormalTypeExpected { get; protected set; }

    public Type sut_ZeroGTypeExpected { get; protected set; }

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
