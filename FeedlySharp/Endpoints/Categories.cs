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
    /// Returns all categories created by the user.
    /// </summary>
    /// <remarks>categories-endpoint (https://developer.feedly.com/v3/categories/#get-the-list-of-all-categories)</remarks>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<List<FeedlyCategory>> GetCategories(CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Client.Request<List<FeedlyCategory>>(HttpMethod.Get, "v3/categories", null, false, true, cancellationToken);
    }

    /// <summary>
    /// Renames a user category.
    /// </summary>
    /// <remarks>categories-endpoint (https://developer.feedly.com/v3/categories/#change-the-label-of-an-existing-category)</remarks>
    /// <param name="id">The category id.</param>
    /// <param name="label">The new label.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task RenameCategory(string id, string label, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.Request<FeedlyUser>(HttpMethod.Post, String.Format("v3/categories/{0}", ValueToResource("category", id)), new { label = label }, true, true, cancellationToken);
    }

    /// <summary>
    /// Deletes a user category.
    /// </summary>
    /// <remarks>categories-endpoint (https://developer.feedly.com/v3/categories/#delete-a-category)</remarks>
    /// <param name="id">The category id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task DeleteCategory(string id, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.Request<FeedlyUser>(HttpMethod.Delete, String.Format("v3/categories/{0}", ValueToResource("category", id)), null, false, true, cancellationToken);
    }
  }
}
