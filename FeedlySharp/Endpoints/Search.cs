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
    /// Find feeds based on title, url or #topic.
    /// </summary>
    /// <remarks>search-endpoint (https://developer.feedly.com/v3/search/#find-feeds-based-on-title-url-or-topic)</remarks>
    /// <param name="searchQuery">Query can be a feed url, a site title, a site url or a #topic.</param>
    /// <param name="count">Number of results. Default value is 20.</param>
    /// <param name="locale">Hint the search engine to return feeds in that locale (e.g. “pt”, “fr_FR”).</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
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


    /// <summary>
    /// Search the content of a stream.
    /// </summary>
    /// <remarks>search-endpoint (https://developer.feedly.com/v3/search/#search-the-content-of-a-stream)</remarks>
    /// <param name="contentId">
    /// A feedId, a categoryId, a tagId or a system category ids. 
    /// If none is provided, the server will use the “global.all” category. 
    /// The special topic “topic/global.popular” can also be used to search the most popular 50,000 sources in feedly.
    /// </param>
    /// <param name="searchQuery">See the http://feedly.uservoice.com/knowledgebase/articles/441699-power-search-tutorial for syntax.</param>
    /// <param name="newerThan">Date from where to fetch.</param>
    /// <param name="continuation">A continuation id is used to page through the content. Pass the continuation from a search result to get the next set of results for this search. Note: this value is ignored for non-Pro users.</param>
    /// <param name="fields">A comma-separated list of fields. By default, all fields are used for matching. Valid field names: all, title, author, keywords. Default is all.</param>
    /// <param name="embedded">Audio, video, doc or any limit results to also include this media type. “any” means the article must contain at least one embed. Default behavior is to not filter by embedded.</param>
    /// <param name="engagement">Medium or high limit results to articles that have the specified engagement. Default behavior is to not filter by engagement.</param>
    /// <param name="count">Number of entries to return. Default is 10. max is 20. Note: if the user isn’t pro, only 2 articles will be returned.</param>
    /// <param name="locale">Used to filter by locale. Only used in topic/global.popular searches.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
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
      if (!String.IsNullOrWhiteSpace(contentId))
      {
        parameters["streamId"] = contentId;
      }
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
