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
    public async Task<List<FeedlyTopic>> GetTopics(CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Client.Request<List<FeedlyTopic>>(HttpMethod.Get, "v3/topics", null, false, true, cancellationToken);
    }


    public async Task<bool> AddOrUpdateTopic(string topic, Interest interest = Interest.Low, CancellationToken cancellationToken = default(CancellationToken))
    {
      if (interest == Interest.Unknown)
      {
        throw new ArgumentOutOfRangeException("Select low, medium or high for the interest.");
      }

      await Client.Request(HttpMethod.Post, "v3/topics", new { id = ValueToResource("topic", topic, false), interest = interest.ToString().ToLower() }, true, true, cancellationToken);
      return true;
    }


    public async Task<bool> RemoveTopic(string topic, Interest interest = Interest.Low, CancellationToken cancellationToken = default(CancellationToken))
    {
      if (interest == Interest.Unknown)
      {
        throw new ArgumentOutOfRangeException("Select low, medium or high for the interest.");
      }

      await Client.Request(HttpMethod.Delete, String.Format("v3/topics/{0}", ValueToResource("topic", topic)), null, false, true, cancellationToken);
      return true;
    }
  }
}
