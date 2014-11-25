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
