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
      return await Client.AuthRequest<List<FeedlyCategory>>(HttpMethod.Get, "v3/categories", null, cancellationToken);
    }


    public async Task UpdateCategory(string id, string label, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.AuthRequest<FeedlyUser>(HttpMethod.Post, String.Format("v3/categories/{0}", id), new Dictionary<string, string>()
      {
        { "label", label }
      }, cancellationToken);
    }


    public async Task DeleteCategory(string id, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.AuthRequest<FeedlyUser>(HttpMethod.Delete, String.Format("v3/categories/{0}", id), null, cancellationToken);
    }
  }
}
