using System;

using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace SRF.FileLogger
{
  /// <summary>
  /// A base logger provider class.
  /// <para>A logger provider essentialy represents the medium where log information is saved.</para>
  /// <para>This class may serve as base class in writing a file or database logger provider.</para>
  /// </summary>
  public abstract class LoggerProvider : IDisposable, ILoggerProvider, ISupportExternalScope
  {
    #region Fields and Properties

    ConcurrentDictionary<string, Logger> m_Loggers = new ConcurrentDictionary<string, Logger>();

    IExternalScopeProvider m_ScopeProvider;

    /// <summary>
    /// A change token is a general-purpose, low-level building block used to track state changes
    /// see: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/change-tokens?view=aspnetcore-3.1
    /// </summary>
    protected IDisposable SettingsChangeToken;

    /// <summary>
    /// Returns the scope provider.
    /// <para>Called by logger instances created by this provider.</para>
    /// </summary>
    internal IExternalScopeProvider ScopeProvider
    {
      get
      {
        if (m_ScopeProvider == null)
          m_ScopeProvider = new LoggerExternalScopeProvider();
        return m_ScopeProvider;
      }
    }

    #endregion


    #region Abstract Methods

    /// <summary>
    /// Returns true if a specified log level is enabled.
    /// <para>Called by logger instances created by this provider.</para>
    /// </summary>
    public abstract bool IsEnabled(LogLevel logLevel);

    /// <summary>
    /// The m_Loggers do not actually log the information in any medium.
    /// Instead the call their provider WriteLog() method, passing the gathered log information.
    /// </summary>
    public abstract void WriteLog(LogEntry Info);

    #endregion


    #region ISupportExternalScope Implementation

    /// <summary>
    /// Called by the logging framework in order to set external scope information source for the logger provider.
    /// <para>ISupportExternalScope implementation</para>
    /// </summary>
    void ISupportExternalScope.SetScopeProvider(IExternalScopeProvider scopeProvider)
    {
      m_ScopeProvider = scopeProvider;
    }

    #endregion


    #region ILoggerProvider Implementation

    /// <summary>
    /// Returns an ILogger instance to serve a specified category.
    /// <para>The category is usually the fully qualified class name of a class asking for a logger, e.g. MyNamespace.MyClass </para>
    /// </summary>
    ILogger ILoggerProvider.CreateLogger(string Category)
    {
      return m_Loggers.GetOrAdd(Category,
      (category) =>
      {
        return new Logger(this, category);
      });
    }

    #endregion


    #region IDisposable Implementation

    /// <summary>Returns true when this instance is disposed.</summary>
    public bool IsDisposed { get; protected set; }


    /// <summary>Disposes this instance</summary>
    void IDisposable.Dispose()
    {
      if (!this.IsDisposed)
      {
        try
        {
          Dispose(true);
        }
        catch
        {
        }
        this.IsDisposed = true;
        GC.SuppressFinalize(this);  // instructs GC not bother to call the destructor
      }
    }


    /// <summary>Disposes the options change toker. IDisposable pattern implementation.</summary>
    protected virtual void Dispose(bool disposing)
    {
      if (SettingsChangeToken != null)
      {
        SettingsChangeToken.Dispose();
        SettingsChangeToken = null;
      }
    }

    /// <summary>Destructor.</summary>
    ~LoggerProvider()
    {
      if (!this.IsDisposed)
      {
        Dispose(false);
      }
    }

    #endregion

  }


}
