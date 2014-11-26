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
    /// Get a list of entry ids for a specific stream.
    /// </summary>
    /// <remarks>streams-endpoint (https://developer.feedly.com/v3/streams/#get-a-list-of-entry-ids-for-a-specific-stream)</remarks>
    /// <param name="id">A feedId, a categoryId, a tagId or a system category id.</param>
    /// <param name="type">The type of the <paramref name="id"/>.</param>
    /// <param name="count">Number of entry ids to return. Default is 20, max is 10000.</param>
    /// <param name="sorting">Newest or oldest. Default is newest.</param>
    /// <param name="unreadOnly">if <c>true</c>, return unread entry ids only.</param>
    /// <param name="newerThan">Date from where to search on.</param>
    /// <param name="continuation">A continuation id is used to page through the entry ids.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<FeedlyStreamEntryIdsResponse> GetStreamEntryIds(
      string id, 
      ContentType type, 
      int? count = null, 
      FeedSorting sorting = FeedSorting.Newest,
      bool? unreadOnly = null,
      DateTime? newerThan = null,
      string continuation = null,
      CancellationToken cancellationToken = default(CancellationToken))
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["streamId"] = ValueToResource(type, id);
      parameters["ranked"] = sorting.ToString().ToLower();
      if (count.HasValue)
      {
        parameters["count"] = count.Value.ToString();
      }
      if (unreadOnly.HasValue)
      {
        parameters["unreadOnly"] = unreadOnly.Value.ToString();
      }
      if (newerThan.HasValue)
      {
        DateTime date = ((DateTime)newerThan.Value).ToUniversalTime();
        DateTime epoc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        parameters["newerThan"] = Math.Truncate(date.Subtract(epoc).TotalMilliseconds).ToString();
      }
      if (!String.IsNullOrEmpty(continuation))
      {
        parameters["continuation"] = continuation;
      }

      return await Client.Request<FeedlyStreamEntryIdsResponse>(HttpMethod.Get, "v3/streams/ids", parameters, false, (type == ContentType.Category || type == ContentType.Tag), cancellationToken);
    }


    /// <summary>
    /// Get a list of entries for a specific stream.
    /// </summary>
    /// <remarks>streams-endpoint (https://developer.feedly.com/v3/streams/#get-the-content-of-a-stream)</remarks>
    /// <param name="id">A feedId, a categoryId, a tagId or a system category id.</param>
    /// <param name="type">The type of the <paramref name="id"/>.</param>
    /// <param name="count">Number of entry ids to return. Default is 20, max is 10000.</param>
    /// <param name="sorting">Newest or oldest. Default is newest.</param>
    /// <param name="unreadOnly">if <c>true</c>, return unread entries only.</param>
    /// <param name="newerThan">Date from where to search on.</param>
    /// <param name="continuation">A continuation id is used to page through the entries.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<FeedlyStreamEntriesResponse> GetStreamEntries(
      string id,
      ContentType type,
      int? count = null,
      FeedSorting sorting = FeedSorting.Newest,
      bool? unreadOnly = null,
      DateTime? newerThan = null,
      string continuation = null,
      CancellationToken cancellationToken = default(CancellationToken))
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["streamId"] = ValueToResource(type, id);
      parameters["ranked"] = sorting.ToString().ToLower();
      if (count.HasValue)
      {
        parameters["count"] = count.Value.ToString();
      }
      if (unreadOnly.HasValue)
      {
        parameters["unreadOnly"] = unreadOnly.Value.ToString();
      }
      if (newerThan.HasValue)
      {
        DateTime date = ((DateTime)newerThan.Value).ToUniversalTime();
        DateTime epoc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        parameters["newerThan"] = Math.Truncate(date.Subtract(epoc).TotalMilliseconds).ToString();
      }
      if (!String.IsNullOrEmpty(continuation))
      {
        parameters["continuation"] = continuation;
      }

      return await Client.Request<FeedlyStreamEntriesResponse>(HttpMethod.Get, "v3/streams/contents", parameters, false, (type == ContentType.Category || type == ContentType.Tag), cancellationToken);
    }
  }

  public enum FeedSorting
  {
    Newest,
    Oldest
  }
}
