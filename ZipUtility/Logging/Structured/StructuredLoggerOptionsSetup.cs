using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging.Configuration;

namespace SRF.FileLogging.Structured
{
  /// <summary>
  /// Configures a StructuredLoggerOptions instance by using ConfigurationBinder.Bind against an IConfiguration.
  /// <para>This class essentially binds a StructuredLoggerOptions instance with a section in the appsettings.json file.</para>
  /// </summary>
  internal class StructuredLoggerOptionsSetup : ConfigureFromConfigurationOptions<StructuredLoggerOptions>
  {
    /// <summary>Constructor that takes the IConfiguration instance to bind against.</summary>
    public StructuredLoggerOptionsSetup(ILoggerProviderConfiguration<StructuredLoggerProvider> providerConfiguration)
        : base(providerConfiguration.Configuration) { }
  }
}
