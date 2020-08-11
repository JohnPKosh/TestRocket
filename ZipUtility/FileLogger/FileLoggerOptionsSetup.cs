using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging.Configuration;

namespace SRF.FileLogger
{
  /// <summary>
  /// Configures a FileLoggerOptions instance by using ConfigurationBinder.Bind against an IConfiguration.
  /// <para>This class essentially binds a FileLoggerOptions instance with a section in the appsettings.json file.</para>
  /// </summary>
  internal class FileLoggerOptionsSetup : ConfigureFromConfigurationOptions<FileLoggerOptions>
  {
    /// <summary>Constructor that takes the IConfiguration instance to bind against.</summary>
    public FileLoggerOptionsSetup(ILoggerProviderConfiguration<FileLoggerProvider> providerConfiguration)
        : base(providerConfiguration.Configuration){ }
  }
}
