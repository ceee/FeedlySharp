using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FeedlySharp.Extensions;
using FeedlySharp.Models;
using System.Net.Http;
using System.Threading;

namespace FeedlySharp
{
  public partial class FeedlyClient
  {
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


    public AuthenticationResponse ParseAuthenticationResponseUri(string uri)
    {
      return ParseAuthenticationResponseUri(new Uri(uri, UriKind.Absolute));
    }


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
