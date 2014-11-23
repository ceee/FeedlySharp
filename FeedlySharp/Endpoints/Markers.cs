using FeedlySharp.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Dynamic;

namespace FeedlySharp
{
  public partial class FeedlyClient
  {
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


    public async Task<bool> MarkEntriesAsRead(string[] entryIds, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.Request(HttpMethod.Post, "v3/markers", new { action = "markAsRead", entryIds = entryIds, type = "entries" }, true, true, cancellationToken);
      return true;
    }

    public async Task<bool> MarkEntryAsRead(string entryId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await MarkEntriesAsRead(new string[] { entryId }, cancellationToken);
    }


    public async Task<bool> KeepEntriesAsUnread(string[] entryIds, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.Request(HttpMethod.Post, "v3/markers", new { action = "keepUnread", entryIds = entryIds, type = "entries" }, true, true, cancellationToken);
      return true;
    }

    public async Task<bool> KeepEntryAsUnread(string entryId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await KeepEntriesAsUnread(new string[] { entryId }, cancellationToken);
    }


    public async Task<bool> MarkFeedsAsRead(string[] feedIds, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.Request(HttpMethod.Post, "v3/markers", new { action = "markAsRead", feedIds = feedIds, type = "feeds" }, true, true, cancellationToken);
      return true;
    }

    public async Task<bool> MarkFeedAsRead(string feedId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await MarkFeedsAsRead(new string[] { feedId }, cancellationToken);
    }


    public async Task<bool> MarkCategoriesAsRead(string[] categoryIds, CancellationToken cancellationToken = default(CancellationToken))
    {
      categoryIds = categoryIds.Select(x => ValueToResource("category", x, false)).ToArray();
      await Client.Request(HttpMethod.Post, "v3/markers", new { action = "markAsRead", categoryIds = categoryIds, type = "categories" }, true, true, cancellationToken);
      return true;
    }

    public async Task<bool> MarkCategoryAsRead(string categoryId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await MarkCategoriesAsRead(new string[] { categoryId }, cancellationToken);
    }


    public async Task<bool> MarkEntriesAsSaved(string[] entryIds, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.Request(HttpMethod.Post, "v3/markers", new { action = "markAsSaved", categoryIds = entryIds, type = "entries" }, true, true, cancellationToken);
      return true;
    }

    public async Task<bool> MarkEntryAsSaved(string entryId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await MarkEntriesAsSaved(new string[] { entryId }, cancellationToken);
    }


    public async Task<bool> MarkEntriesAsUnsaved(string[] entryIds, CancellationToken cancellationToken = default(CancellationToken))
    {
      await Client.Request(HttpMethod.Post, "v3/markers", new { action = "markAsUnsaved", categoryIds = entryIds, type = "entries" }, true, true, cancellationToken);
      return true;
    }

    public async Task<bool> MarkEntryAsUnsaved(string entryId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await MarkEntriesAsUnsaved(new string[] { entryId }, cancellationToken);
    }


    public async Task<FeedlyReadOperations> GetMarkersReadOperations(DateTime newerThan, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await Client.Request<FeedlyReadOperations>(HttpMethod.Get, "v3/markers/reads", new Dictionary<string, string>()
      {
        { "newerThan",  Math.Truncate(newerThan.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds).ToString() }
      }, false, true, cancellationToken);
    }

  }
}
