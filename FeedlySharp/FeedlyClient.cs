using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FeedlySharp
{
  public partial class FeedlyClient : IDisposable, FeedlySharp.IFeedlyClient
  {
    /// <summary>
    /// The environment of feedly.
    /// </summary>
    public readonly CloudEnvironment Environment;

    private readonly string ClientId;

    private readonly string ClientSecret;

    private readonly string RedirectUri;

    private string CloudUri { get { return GetCloudUri(Environment); } }

    private string AccessToken { get; set; }

    private string UserId { get; set; }

    private FeedlyHttpClient Client { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FeedlyClient"/> class.
    /// </summary>
    /// <param name="environment">The environment of feedly.</param>
    /// <param name="clientId">The client id.</param>
    /// <param name="clientSecret">The client secret.</param>
    /// <param name="redirectUri">The redirect URI.</param>
    /// <exception cref="System.ArgumentNullException">redirectUri;Authentication and token generation requires an URI to redirect to afterwards</exception>
    public FeedlyClient(CloudEnvironment environment, string clientId, string clientSecret, string redirectUri)
    {
      if (String.IsNullOrEmpty(redirectUri))
      {
        throw new ArgumentNullException("redirectUri", "Authentication and token generation requires an URI to redirect to afterwards");
      }

      Environment = environment;
      ClientId = clientId;
      ClientSecret = clientSecret;
      RedirectUri = redirectUri;
      Client = new FeedlyHttpClient(new Uri(CloudUri, UriKind.Absolute));
    }

    /// <summary>
    /// Activates the client to call methods which need authentication.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="userId">The user id.</param>
    public void Activate(string accessToken, string userId)
    {
      AccessToken = accessToken;
      UserId = userId;
      Client.AccessToken = accessToken;
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      Client.Dispose();
    }


    private string GetCloudUri(CloudEnvironment environment)
    {
      return String.Format("https://{0}.feedly.com", environment == CloudEnvironment.Production ? "cloud" : "sandbox");
    }

    private string ValueToResource(string key, string value, bool encode = true)
    {
      string text;
      if (key == "feed") text = value.StartsWith("feed/") ? value : "feed/" + value;
      else text = value.StartsWith("user/") ? value : String.Format("user/{0}/{1}/{2}", UserId, key, value);

      return encode ? WebUtility.UrlEncode(text) : text;
    }

    private string ValueToResource(ContentType type, string value, bool encode = true)
    {
      string key = type == ContentType.Feed ? "feed" : (type == ContentType.Tag ? "tag" : (type == ContentType.SystemCategory ? "systemcategory" : "category"));
      if (key == "systemcategory")
      {
        return encode ? WebUtility.UrlEncode(value) : value;
      }

      return ValueToResource(key, value, encode);
    }
  }

  public enum CloudEnvironment
  {
    Production,
    Sandbox
  }

  public enum ContentType
  {
    Feed,
    Category,
    SystemCategory,
    Tag
  }
}
