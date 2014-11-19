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
    public async Task<Dictionary<string, string>> GetPreferences(CancellationToken cancellationToken = default(CancellationToken))
    {
      return (await Client.Request<Dictionary<string, string>>(HttpMethod.Get, "v3/preferences", null, false, true, cancellationToken)) ?? new Dictionary<string, string>();
    }


    public async Task<Dictionary<string, string>> UpdatePreferences(Dictionary<string, string> preferences, CancellationToken cancellationToken = default(CancellationToken))
    {
      return (await Client.Request<Dictionary<string, string>>(HttpMethod.Post, "v3/preferences", preferences, true, true, cancellationToken)) ?? new Dictionary<string, string>();
    }
  }
}
