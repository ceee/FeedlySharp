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
    /// Get the list of tags created by the user.
    /// </summary>
    /// <remarks>tags-endpoint (https://developer.feedly.com/v3/tags/#get-the-list-of-tags-created-by-the-user)</remarks>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<List<FeedlyTag>> GetTags(CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Client.Request<List<FeedlyTag>>(HttpMethod.Get, "v3/tags", null, false, true, cancellationToken);
    }

    /// <summary>
    /// Update tags for an existing entry.
    /// </summary>
    /// <remarks>tags-endpoint (https://developer.feedly.com/v3/tags/#tag-an-existing-entry)</remarks>
    /// <param name="entryId">The entry id.</param>
    /// <param name="tags">The new tags.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> UpdateTags(string entryId, string[] tags, CancellationToken cancellationToken = default(CancellationToken))
    {
      string tagsString = String.Join(",", tags.Select(x => ValueToResource("tag", x)));
      await Client.Request<object>(HttpMethod.Put, String.Format("v3/tags/{0}", tagsString), new { entryId = entryId }, true, true, cancellationToken);
      return true;
    }

    /// <summary>
    /// Update tags for existing entries.
    /// </summary>
    /// <remarks>tags-endpoint (https://developer.feedly.com/v3/tags/#tag-multiple-entries)</remarks>
    /// <param name="entryId">The entry ids.</param>
    /// <param name="tags">The new tags.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> UpdateTags(string[] entryIds, string[] tags, CancellationToken cancellationToken = default(CancellationToken))
    {
      string tagsString = String.Join(",", tags.Select(x => ValueToResource("tag", x)));
      await Client.Request<object>(HttpMethod.Put, String.Format("v3/tags/{0}", tagsString), new { entryIds = entryIds }, true, true, cancellationToken);
      return true;
    }

    /// <summary>
    /// Change a tag label.
    /// </summary>
    /// <remarks>tags-endpoint (https://developer.feedly.com/v3/tags/#change-a-tag-label)</remarks>
    /// <param name="oldTag">The old tag label.</param>
    /// <param name="newTag">The new tag label.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> RenameTag(string oldTag, string newTag, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.Request<object>(HttpMethod.Post, String.Format("v3/tags/{0}", ValueToResource("tag", oldTag)), new { label = newTag }, true, true, cancellationToken);
      return true;
    }

    /// <summary>
    /// Untag multiple entries.
    /// </summary>
    /// <remarks>tags-endpoint (https://developer.feedly.com/v3/tags/#untag-multiple-entries)</remarks>
    /// <param name="entryIds">The entry ids.</param>
    /// <param name="tags">The tags to remove from the entries.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> RemoveTags(string[] entryIds, string[] tags, CancellationToken cancellationToken = default(CancellationToken))
    {
      string tagsString = String.Join(",", tags.Select(x => ValueToResource("tag", x)));
      string entryIdsString = String.Join(",", entryIds.Select(x => WebUtility.UrlEncode(x)));
      await Client.Request<object>(HttpMethod.Delete, String.Format("v3/tags/{0}/{1}", tagsString, entryIdsString), null, false, true, cancellationToken);
      return true;
    }

    /// <summary>
    /// Delete tags.
    /// The tags will be removed from all the entries, and from the list of tags. Note: global tags (global.read, global.saved) cannot be passed to this call.
    /// </summary>
    /// <remarks>tags-endpoint (https://developer.feedly.com/v3/tags/#delete-tags)</remarks>
    /// <param name="tags">The tags to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> RemoveTags(string[] tags, CancellationToken cancellationToken = default(CancellationToken))
    {
      string tagsString = String.Join(",", tags.Select(x => ValueToResource("tag", x)));
      await Client.Request<object>(HttpMethod.Delete, String.Format("v3/tags/{0}", tagsString), null, false, true, cancellationToken);
      return true;
    }
  }
}
