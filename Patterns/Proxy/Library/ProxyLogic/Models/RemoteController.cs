using ProxyLogic.Common;

namespace ProxyLogic.Models
{

  /* Imagine that this class is calling some kind of remote controlled service that does not exist in this code */

  public class RemoteController
  {
    /// <summary>Say some clever phrase here.</summary>
    public string Babble()
    {
      return "start of transmission-- \"" + FactoryConstants.TOY_SPK + "\" --end of transmission";
    }

    /// <summary>Begin launch command instruction.</summary>
    public string SendCommand()
    {
      return FactoryConstants.TOY_LAUNCH + " --signal sent";
    }
  }
}
