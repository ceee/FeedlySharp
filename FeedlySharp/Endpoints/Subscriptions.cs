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
    /// Get the users subscriptions.
    /// </summary>
    /// <remarks>subscriptions-endpoint (https://developer.feedly.com/v3/subscriptions/#get-the-users-subscriptions)</remarks>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<List<FeedlySubscription>> GetSubscriptions(CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Client.Request<List<FeedlySubscription>>(HttpMethod.Get, "v3/subscriptions", null, false, true, cancellationToken);
    }

    /// <summary>
    /// Adds or updates a subscription.
    /// </summary>
    /// <remarks>subscriptions-endpoint (https://developer.feedly.com/v3/subscriptions/#subscribe-to-a-feed)</remarks>
    /// <param name="id">The id of the subscription/feed.</param>
    /// <param name="categories">The categories where the feed to assign to.</param>
    /// <param name="optionalTitle">The optional title for the feed.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> AddOrUpdateSubscription(string id, List<FeedlyCategory> categories = null, string optionalTitle = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      dynamic parameters = new { id = (id.StartsWith("feed/") ? id : "feed/" + id) };

      if (categories != null && categories.Any())
      {
        parameters.categories = categories;
      }
      if (!String.IsNullOrEmpty(optionalTitle))
      {
        parameters.title = optionalTitle;
      }

      await Client.Request(HttpMethod.Post, "v3/subscriptions", parameters, true, true, cancellationToken);
      return true;
    }

    /// <summary>
    /// Unsubscribe from a feed.
    /// </summary>
    /// <param name="id">The id of the subscription/feed.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> RemoveSubscription(string id, CancellationToken cancellationToken = default(CancellationToken))
    {
      id = id.StartsWith("feed/") ? id : "feed/" + id;
      await Client.Request(HttpMethod.Delete, String.Format("v3/subscriptions/{0}", WebUtility.UrlEncode(id)), null, false, true, cancellationToken);
      return true;
    }
  }
}
