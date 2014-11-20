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
    public async Task<FeedlyEntry> GetEntry(string id, CancellationToken cancellationToken = default(CancellationToken))
    {
      List<FeedlyEntry> entries = await Client.Request<List<FeedlyEntry>>(HttpMethod.Get, String.Format("v3/entries/{0}", WebUtility.UrlEncode(id)), null, false, true, cancellationToken);
      return entries != null && entries.Any() ? entries[0] : null;
    }


    public async Task<List<FeedlyEntry>> GetEntries(string[] ids, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Client.Request<List<FeedlyEntry>>(HttpMethod.Post, "v3/entries/.mget", ids, true, true, cancellationToken);
    }
  }
}
