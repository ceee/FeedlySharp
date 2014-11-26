using FeedlySharp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace FeedlySharp
{
  public partial class FeedlyClient
  {
    /// <summary>
    /// Get the list of unread counts.
    /// </summary>
    /// <remarks>markers-endpoint (https://developer.feedly.com/v3/markers/#get-the-list-of-unread-counts)</remarks>
    /// <param name="isAutoRefresh">Let’s the server know if this is a background auto-refresh or not. In case of very high load on the service, the server can deny access to background requests and give priority to user facing operations.</param>
    /// <param name="newerThan">Date used as a lower time limit, instead of the default 30 days</param>
    /// <param name="categoryId">A user or system category can be passed to restrict the unread count response to feeds in this category.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<List<FeedlyCategoryUnreadCount>> GetMarkersUnreadCount(bool isAutoRefresh = false, DateTime? newerThan = null, string categoryId = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      if (isAutoRefresh)
      {
        parameters["autorefresh"] = "true";
      }
      if (newerThan.HasValue)
      {
        DateTime date = ((DateTime)newerThan.Value).ToUniversalTime();
        DateTime epoc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        parameters["newerThan"] = Math.Truncate(date.Subtract(epoc).TotalMilliseconds).ToString();
      }
      if (!String.IsNullOrEmpty(categoryId))
      {
        parameters["streamId"] = ValueToResource("category", categoryId, false);
      }
      return (await Client.Request<CategoryUnreadCountResponse>(HttpMethod.Get, "v3/markers/counts", parameters, false, true, cancellationToken)).List;
    }

    /// <summary>
    /// Marks multiple entries as read.
    /// </summary>
    /// <remarks>markers-endpoint (https://developer.feedly.com/v3/markers/#mark-one-or-multiple-articles-as-read)</remarks>
    /// <param name="entryIds">The entry ids.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> MarkEntriesAsRead(string[] entryIds, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.Request(HttpMethod.Post, "v3/markers", new { action = "markAsRead", entryIds = entryIds, type = "entries" }, true, true, cancellationToken);
      return true;
    }

    /// <summary>
    /// Marks an entry as read.
    /// </summary>
    /// <remarks>markers-endpoint (https://developer.feedly.com/v3/markers/#mark-one-or-multiple-articles-as-read)</remarks>
    /// <param name="entryId">The entry id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> MarkEntryAsRead(string entryId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await MarkEntriesAsRead(new string[] { entryId }, cancellationToken);
    }

    /// <summary>
    /// Keep multiple entries as unread.
    /// </summary>
    /// <remarks>markers-endpoint (https://developer.feedly.com/v3/markers/#keep-one-or-multiple-articles-as-unread)</remarks>
    /// <param name="entryIds">The entry ids.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> KeepEntriesAsUnread(string[] entryIds, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.Request(HttpMethod.Post, "v3/markers", new { action = "keepUnread", entryIds = entryIds, type = "entries" }, true, true, cancellationToken);
      return true;
    }

    /// <summary>
    /// Keep an entry as unread.
    /// </summary>
    /// <remarks>markers-endpoint (https://developer.feedly.com/v3/markers/#keep-one-or-multiple-articles-as-unread)</remarks>
    /// <param name="entryId">The entry id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> KeepEntryAsUnread(string entryId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await KeepEntriesAsUnread(new string[] { entryId }, cancellationToken);
    }

    /// <summary>
    /// Mark multiple feeds as read.
    /// </summary>
    /// <remarks>markers-endpoint (https://developer.feedly.com/v3/markers/#mark-a-feed-as-read)</remarks>
    /// <param name="feedIds">The feed ids.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> MarkFeedsAsRead(string[] feedIds, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.Request(HttpMethod.Post, "v3/markers", new { action = "markAsRead", feedIds = feedIds, type = "feeds" }, true, true, cancellationToken);
      return true;
    }

    /// <summary>
    /// Mark a feed as read.
    /// </summary>
    /// <remarks>markers-endpoint (https://developer.feedly.com/v3/markers/#mark-a-feed-as-read)</remarks>
    /// <param name="feedId">The feed id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> MarkFeedAsRead(string feedId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await MarkFeedsAsRead(new string[] { feedId }, cancellationToken);
    }

    /// <summary>
    /// Mark multiple categories as read.
    /// </summary>
    /// <remarks>markers-endpoint (https://developer.feedly.com/v3/markers/#mark-a-category-as-read)</remarks>
    /// <param name="categoryIds">The category ids.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> MarkCategoriesAsRead(string[] categoryIds, CancellationToken cancellationToken = default(CancellationToken))
    {
      categoryIds = categoryIds.Select(x => ValueToResource("category", x, false)).ToArray();
      await Client.Request(HttpMethod.Post, "v3/markers", new { action = "markAsRead", categoryIds = categoryIds, type = "categories" }, true, true, cancellationToken);
      return true;
    }

    /// <summary>
    /// Mark a category as read.
    /// </summary>
    /// <remarks>markers-endpoint (https://developer.feedly.com/v3/markers/#mark-a-category-as-read)</remarks>
    /// <param name="categoryId">The category id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> MarkCategoryAsRead(string categoryId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await MarkCategoriesAsRead(new string[] { categoryId }, cancellationToken);
    }

    /// <summary>
    /// Marks multiple entries as saved.
    /// </summary>
    /// <remarks>markers-endpoint (https://developer.feedly.com/v3/markers/#mark-one-or-multiple-articles-as-saved)</remarks>
    /// <param name="entryIds">The entry ids.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> MarkEntriesAsSaved(string[] entryIds, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.Request(HttpMethod.Post, "v3/markers", new { action = "markAsSaved", categoryIds = entryIds, type = "entries" }, true, true, cancellationToken);
      return true;
    }

    /// <summary>
    /// Marks an entriy as saved.
    /// </summary>
    /// <remarks>markers-endpoint (https://developer.feedly.com/v3/markers/#mark-one-or-multiple-articles-as-saved)</remarks>
    /// <param name="entryId">The entry id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> MarkEntryAsSaved(string entryId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await MarkEntriesAsSaved(new string[] { entryId }, cancellationToken);
    }

    /// <summary>
    /// Marks multiple entries as unsaved.
    /// </summary>
    /// <remarks>markers-endpoint (https://developer.feedly.com/v3/markers/#mark-one-or-multiple-articles-as-unsaved)</remarks>
    /// <param name="entryIds">The entry ids.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> MarkEntriesAsUnsaved(string[] entryIds, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.Request(HttpMethod.Post, "v3/markers", new { action = "markAsUnsaved", categoryIds = entryIds, type = "entries" }, true, true, cancellationToken);
      return true;
    }

    /// <summary>
    /// Marks an entry as unsaved.
    /// </summary>
    /// <remarks>markers-endpoint (https://developer.feedly.com/v3/markers/#mark-one-or-multiple-articles-as-unsaved)</remarks>
    /// <param name="entryId">The entry id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> MarkEntryAsUnsaved(string entryId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await MarkEntriesAsUnsaved(new string[] { entryId }, cancellationToken);
    }

    /// <summary>
    /// Get the latest read operations.
    /// </summary>
    /// <remarks>markers-endpoint (https://developer.feedly.com/v3/markers/#get-the-latest-read-operations-to-sync-local-cache)</remarks>
    /// <param name="newerThan">Date from where to fetch the data. Default is 30 days.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<FeedlyReadOperations> GetMarkersReadOperations(DateTime newerThan, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Client.Request<FeedlyReadOperations>(HttpMethod.Get, "v3/markers/reads", new Dictionary<string, string>()
      {
        { "newerThan",  Math.Truncate(newerThan.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds).ToString() }
      }, false, true, cancellationToken);
    }

  }
}
