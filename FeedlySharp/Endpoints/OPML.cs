using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FeedlySharp
{
  public partial class FeedlyClient
  {
    /// <summary>
    /// Export the user's subscriptions as an OPML file.
    /// </summary>
    /// <remarks>OPML-endpoint (https://developer.feedly.com/v3/opml/#export-the-users-subscriptions-as-an-opml-file)</remarks>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<string> GetOPML(CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Client.Request(HttpMethod.Get, "v3/opml", null, false, true, cancellationToken);
    }


    /// <summary>
    /// Import an OPML.
    /// </summary>
    /// <remarks>OPML-endpoint (https://developer.feedly.com/v3/opml/#import-an-opml)</remarks>
    /// <param name="opml">The OPML input.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> ImportOPML(string opml, CancellationToken cancellationToken = default(CancellationToken))
    {
      HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "v3/opml");
      request.Content = new StringContent(opml);
      request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
      request.Headers.Add("Authorization", String.Format("OAuth {0}", AccessToken));
      request.Headers.TryAddWithoutValidation("Accept", "text/xml");

      await Client.Request(request, cancellationToken);
      return true;
    }
  }
}
