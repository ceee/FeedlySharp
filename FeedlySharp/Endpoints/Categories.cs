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
    public async Task<List<FeedlyCategory>> GetCategories(CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Client.Request<List<FeedlyCategory>>(HttpMethod.Get, "v3/categories", null, false, true, cancellationToken);
    }


    public async Task RenameCategory(string id, string label, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.Request<FeedlyUser>(HttpMethod.Post, String.Format("v3/categories/{0}", ValueToResource("category", id)), new { label = label }, true, true, cancellationToken);
    }


    public async Task DeleteCategory(string id, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.Request<FeedlyUser>(HttpMethod.Delete, String.Format("v3/categories/{0}", ValueToResource("category", id)), null, false, true, cancellationToken);
    }
  }
}
