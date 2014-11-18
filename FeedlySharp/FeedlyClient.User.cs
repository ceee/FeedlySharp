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
    public async Task<FeedlyUser> GetUser(CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Client.AuthRequest<FeedlyUser>(HttpMethod.Get, "v3/profile", null, cancellationToken);
    }


    public async Task<FeedlyUser> UpdateUser(Dictionary<string, string> parameters, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Client.AuthRequest<FeedlyUser>(HttpMethod.Post, "v3/profile", parameters, cancellationToken);
    }
  }
}
