using FeedlySharp.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FeedlySharp
{
  public partial class FeedlyClient
  {
    public async Task<string> GetOPML(CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Client.Request(HttpMethod.Get, "v3/opml", null, false, true, cancellationToken);
    }


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
