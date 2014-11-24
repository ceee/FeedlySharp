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
    public async Task<List<FeedlySearchFeed>> FindFeeds(string searchQuery, int? count = null, string locale = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["query"] = searchQuery;
      if (count.HasValue)
      {
        parameters["count"] = count.Value.ToString();
      }
      if (!String.IsNullOrEmpty(locale))
      {
        parameters["locale"] = locale;
      }

      return (await Client.Request<FindFeedsResponse>(HttpMethod.Get, "v3/search/feeds", parameters, false, true, cancellationToken)).List;
    }


    public async Task<FeedlySearchResponse> FindEntries(
      string contentId, 
      string searchQuery,
      DateTime? newerThan = null,
      string continuation = null,
      string fields = null,
      string embedded = null,
      string engagement = null,  
      int? count = null, 
      string locale = null, 
      CancellationToken cancellationToken = default(CancellationToken))
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["streamId"] = contentId;
      parameters["query"] = searchQuery;
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
      if (!String.IsNullOrEmpty(fields))
      {
        parameters["fields"] = fields;
      }
      if (!String.IsNullOrEmpty(embedded))
      {
        parameters["embedded"] = embedded;
      }
      if (!String.IsNullOrEmpty(engagement))
      {
        parameters["engagement"] = engagement;
      }
      if (count.HasValue)
      {
        parameters["count"] = count.Value.ToString();
      }
      if (!String.IsNullOrEmpty(locale))
      {
        parameters["locale"] = locale;
      }

      return await Client.Request<FeedlySearchResponse>(HttpMethod.Get, "v3/search/contents", parameters, false, true, cancellationToken);
    }
  }
}
