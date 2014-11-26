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
    /// <summary>
    /// Gets the content of an entry.
    /// </summary>
    /// <remarks>entries-endpoint (https://developer.feedly.com/v3/entries/#get-the-content-of-an-entry)</remarks>
    /// <param name="id">The entry id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<FeedlyEntry> GetEntry(string id, CancellationToken cancellationToken = default(CancellationToken))
    {
      List<FeedlyEntry> entries = await Client.Request<List<FeedlyEntry>>(HttpMethod.Get, String.Format("v3/entries/{0}", WebUtility.UrlEncode(id)), null, false, false, cancellationToken);
      return entries != null && entries.Any() ? entries[0] : null;
    }


    /// <summary>
    /// Gets the content of multiple entries.
    /// </summary>
    /// <remarks>entries-endpoint (https://developer.feedly.com/v3/entries/#get-the-content-for-a-dynamic-list-of-entries)</remarks>
    /// <param name="ids">The ids of the entries.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<List<FeedlyEntry>> GetEntries(string[] ids, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Client.Request<List<FeedlyEntry>>(HttpMethod.Post, "v3/entries/.mget", ids, true, false, cancellationToken);
    }
  }
}
