using FeedlySharp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FeedlySharp
{
  public partial class FeedlyClient
  {
    /// <summary>
    /// Get the list of topics the user has added to their feedly.
    /// </summary>
    /// <remarks>topics-endpoint (https://developer.feedly.com/v3/topics/#get-the-list-of-topics-the-user-has-added-to-their-feedly)</remarks>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<List<FeedlyTopic>> GetTopics(CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Client.Request<List<FeedlyTopic>>(HttpMethod.Get, "v3/topics", null, false, true, cancellationToken);
    }

    /// <summary>
    /// Add or update a topic-
    /// </summary>
    /// <remarks>topics-endpoint (https://developer.feedly.com/v3/topics/#add-a-topic-to-the-user-feedly-account)</remarks>
    /// <param name="topicId">The topic id.</param>
    /// <param name="interest">The interest of the topic.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Select low, medium or high for the interest.</exception>
    public async Task<bool> AddOrUpdateTopic(string topicId, Interest interest = Interest.Low, CancellationToken cancellationToken = default(CancellationToken))
    {
      if (interest == Interest.Unknown)
      {
        throw new ArgumentOutOfRangeException("Select low, medium or high for the interest.");
      }

      await Client.Request(HttpMethod.Post, "v3/topics", new { id = ValueToResource("topic", topicId, false), interest = interest.ToString().ToLower() }, true, true, cancellationToken);
      return true;
    }

    /// <summary>
    /// Remove a topic from a feedly account.
    /// </summary>
    /// <remarks>topics-endpoint (https://developer.feedly.com/v3/topics/#remove-a-topic-from-a-feedly-account)</remarks>
    /// <param name="topic">The topic id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> RemoveTopic(string topicId, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.Request(HttpMethod.Delete, String.Format("v3/topics/{0}", ValueToResource("topic", topicId)), null, false, true, cancellationToken);
      return true;
    }
  }
}
