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


    public async Task ImportOPML(string opml, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new NotImplementedException();
    }
  }
}
