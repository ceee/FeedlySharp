using FeedlySharp.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace FeedlySharp
{
  public partial class FeedlyClient
  {
    public async Task<FeedlyFeed> GetFeed(string id, CancellationToken cancellationToken = default(CancellationToken))
    {
      id = id.StartsWith("feed/") ? id : "feed/" + id;
      return await Client.Request<FeedlyFeed>(HttpMethod.Get, String.Format("v3/feeds/{0}", WebUtility.UrlEncode(id)), null, false, true, cancellationToken);
    }


    public async Task<List<FeedlyFeed>> GetFeeds(string[] ids, CancellationToken cancellationToken = default(CancellationToken))
    {
      ids = ids.Select(id => id.StartsWith("feed/") ? id : "feed/" + id).ToArray();
      return await Client.Request<List<FeedlyFeed>>(HttpMethod.Post, "v3/feeds/.mget", ids, true, true, cancellationToken);
    }
  }
}
