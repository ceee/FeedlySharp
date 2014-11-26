using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FeedlySharp.Extensions;
using FeedlySharp.Models;
using System.Net.Http;
using System.Threading;

namespace FeedlySharp
{
  public partial class FeedlyClient
  {
    /// <summary>
    /// Creates the authentication URI to redirect the user to.
    /// </summary>
    /// <remarks>auth-endpoint (https://developer.feedly.com/v3/auth/#authenticating-a-user-and-obtaining-a-code)</remarks>
    /// <param name="scope">The scope.</param>
    /// <param name="state">The state which is passed and returned when finished.</param>
    /// <returns></returns>
    public string GetAuthenticationUri(string scope = "subscriptions", string state = default(String))
    {
      return String.Format("{0}/v3/auth/auth?redirect_uri={1}&client_id={2}&scope={3}&state={4}&response_type=code",
        CloudUri,
        WebUtility.UrlEncode(RedirectUri),
        WebUtility.UrlEncode(ClientId),
        WebUtility.UrlEncode(GetCloudUri(CloudEnvironment.Production) + "/" + scope.Trim('/')),
        WebUtility.UrlEncode(state ?? String.Empty)
      );
    }

    /// <summary>
    /// Parses the authentication response URI and creates the response object.
    /// </summary>
    /// <param name="uri">The URI where the user has been redirected to after authentication.</param>
    /// <returns></returns>
    public AuthenticationResponse ParseAuthenticationResponseUri(string uri)
    {
      return ParseAuthenticationResponseUri(new Uri(uri, UriKind.Absolute));
    }

    /// <summary>
    /// Parses the authentication response URI and creates the response object.
    /// </summary>
    /// <param name="uri">The URI where the user has been redirected to after authentication.</param>
    /// <returns></returns>
    public AuthenticationResponse ParseAuthenticationResponseUri(Uri uri)
    {
      Dictionary<string, string> queryParams = uri.ParseQueryString();
      string code = null;
      string error = null;
      string state = null;

      queryParams.TryGetValue("code", out code);
      queryParams.TryGetValue("error", out error);
      queryParams.TryGetValue("state", out state);

      return new AuthenticationResponse() { Code = code, Error = error, State = state };
    }


    /// <summary>
    /// Request to get the access token.
    /// </summary>
    /// <remarks>auth-endpoint (https://developer.feedly.com/v3/auth/#exchanging-a-code-for-a-refresh-token-and-an-access-token)</remarks>
    /// <param name="authenticationCode">The authentication code.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<AccessTokenResponse> RequestAccessToken(string authenticationCode, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Client.Request<AccessTokenResponse>(HttpMethod.Post, "v3/auth/token", new Dictionary<string, string>()
      {
        { "code", WebUtility.UrlEncode(authenticationCode) },
        { "client_id", ClientId },
        { "client_secret", ClientSecret },
        { "redirect_uri", RedirectUri },
        { "grant_type", "authorization_code" }
      }, false, false, cancellationToken);
    }


    /// <summary>
    /// Request to get a new refresh token and update authentication.
    /// </summary>
    /// <remarks>auth-endpoint (https://developer.feedly.com/v3/auth/#using-a-refresh-token)</remarks>
    /// <param name="refreshToken">The current refresh token.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<AccessTokenResponse> RequestRefreshToken(string refreshToken, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Client.Request<AccessTokenResponse>(HttpMethod.Post, "v3/auth/token", new Dictionary<string, string>()
      {
        { "refresh_token", WebUtility.UrlEncode(refreshToken) },
        { "client_id", ClientId },
        { "client_secret", ClientSecret },
        { "grant_type", "refresh_token" }
      }, false, false, cancellationToken);
    }


    /// <summary>
    /// Revokes the current refresh token and logs out.
    /// </summary>
    /// <remarks>auth-endpoint (https://developer.feedly.com/v3/auth/#revoking-a-refresh-token)</remarks>
    /// <param name="refreshToken">The refresh token.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task RevokeRefreshToken(string refreshToken, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.Request<object>(HttpMethod.Post, "v3/auth/token", new Dictionary<string, string>()
      {
        { "refresh_token", WebUtility.UrlEncode(refreshToken) },
        { "client_id", ClientId },
        { "client_secret", ClientSecret },
        { "grant_type", "revoke_token" }
      }, false, false, cancellationToken);
    }
  }
}
