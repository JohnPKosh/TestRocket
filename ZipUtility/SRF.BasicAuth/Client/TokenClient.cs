using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SRF.BasicAuth.Models;

namespace SRF.BasicAuth.Client
{
  public class TokenClient
  {
    private readonly ILogger<TokenClient> _logger;

    public TokenClient(ILogger<TokenClient> logger)
    {
      _logger = logger;
    }

    /// <summary>
    /// The JWT token used in client side call credentials for authorization.
    /// </summary>
    public string Token { get; set; } // TODO: Determine if this should needs to be public?

    /// <summary>
    /// The authenticate asynchronous method used to call the specified token authentication endpoint
    /// </summary>
    /// <param name="tokenEndpoint">string</param>
    /// <param name="tokenRequest">TokenRequest</param>
    /// <returns>Task</returns>
    public async Task AuthenticateAsync(string tokenEndpoint, TokenRequest tokenRequest)
    {
      await AuthenticateAsync(new Uri(tokenEndpoint), tokenRequest).ConfigureAwait(false);
    }

    /// <summary>
    /// The authenticate asynchronous method used to call the specified token authentication endpoint
    /// </summary>
    /// <param name="tokenEndpoint">Uri</param>
    /// <param name="tokenRequest">TokenRequest</param>
    /// <returns>Task</returns>
    public async Task AuthenticateAsync(Uri tokenEndpoint, TokenRequest tokenRequest)
    {
      try
      {
        if (tokenEndpoint == null) throw new ArgumentNullException(nameof(tokenEndpoint));
        if (tokenRequest == null) throw new ArgumentNullException(nameof(tokenRequest));
        using var httpClient = new HttpClient();
        using var request = new HttpRequestMessage
        {
          RequestUri = tokenEndpoint,
          Method = HttpMethod.Post,
          Content = new ByteArrayContent(JsonSerializer.SerializeToUtf8Bytes(tokenRequest)),
          Version = new Version(2, 0)
        };
        var tokenResponse = await httpClient.SendAsync(request).ConfigureAwait(false);
        tokenResponse.EnsureSuccessStatusCode(); // TODO: Consider if we should be checking status code before doing this? Do we have failover logic for 404, 500 or others?

        var token = await tokenResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
        Token = token;
        request.Dispose();
        httpClient.Dispose();
      }
      catch (ArgumentNullException ex)
      {
        _logger.LogError(ex, ex.Message);
        throw; // We likely want report fault back to client here.
      }
      catch (HttpRequestException ex)
      {
        _logger.LogError(ex, ex.Message);
        throw;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, ex.Message);
        throw; // We likely want report fault back to client here.
      }
    }

    /// <summary>
    /// The try authenticate asynchronous method used to call the specified token authentication endpoint
    /// </summary>
    /// <param name="tokenEndpoint">Uri</param>
    /// <param name="tokenRequest">TokenRequest</param>
    /// <returns><![CDATA[Task<bool>]]></returns>
    public async Task<bool> TryAuthenticateAsync(string tokenEndpoint, TokenRequest tokenRequest)
    {
      try
      {
        await AuthenticateAsync(tokenEndpoint, tokenRequest).ConfigureAwait(false);
        return true;
      }
      catch (ArgumentNullException)
      {
        throw; // We likely want report fault back to client here.
      }
      catch (HttpRequestException)
      {
        return false; // TODO: Consider if we should be checking status code before doing this? Do we have failover logic for 404, 500 or others?
      }
      catch (Exception)
      {
        throw; // We likely want report fault back to client here.
      }
    }

    /// <summary>
    /// The try authenticate asynchronous method used to call the specified token authentication endpoint
    /// </summary>
    /// <param name="tokenEndpoint">Uri</param>
    /// <param name="tokenRequest">TokenRequest</param>
    /// <returns><![CDATA[Task<bool>]]></returns>
    public async Task<bool> TryAuthenticateAsync(Uri tokenEndpoint, TokenRequest tokenRequest)
    {
      try
      {
        await AuthenticateAsync(tokenEndpoint, tokenRequest).ConfigureAwait(false);
        return true;
      }
      catch (ArgumentNullException)
      {
        throw; // We likely want report fault back to client here.
      }
      catch (HttpRequestException)
      {
        return false; // TODO: Consider if we should be checking status code before doing this? Do we have failover logic for 404, 500 or others?
      }
      catch (Exception)
      {
        throw; // We likely want report fault back to client here.
      }
    }
  }
}
