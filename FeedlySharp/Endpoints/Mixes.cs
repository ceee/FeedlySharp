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
    /// Get a mix of the most engaging content available in a stream.
    /// </summary>
    /// <remarks>mixes-endpoint (https://developer.feedly.com/v3/mixes/#get-a-mix-of-the-most-engaging-content-available-in-a-stream)</remarks>
    /// <param name="contentId">A feed id, a category id, the system category ids or a topic id.</param>
    /// <param name="count">Number of entry ids to return. default is 3. max is 20.</param>
    /// <param name="unreadOnly">if set to <c>true</c> [return unread content only].</param>
    /// <param name="limitHours">Hour of the day. Used for same day optimization: only articles published in the past n hours will be used. This is a more convenient parameter than “newerThan”; both parameters should not be passed in the same request.</param>
    /// <param name="newerThan">Date from where to fetch the data.</param>
    /// <param name="backfill">If “hours” is provided, and there aren’t enough articles to match the entry count requested, the server will look back in time to find more articles. Articles from the first n hours will be returned first.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentException">It is not possible to use both newerThan and limitHours</exception>
    public async Task<List<FeedlyEntry>> GetMixes(
      string contentId,
      int? count = null, 
      bool unreadOnly = false, 
      int? limitHours = null, 
      DateTime? newerThan = null, 
      bool backfill = true, 
      CancellationToken cancellationToken = default(CancellationToken))
    {
      if (newerThan.HasValue && limitHours.HasValue)
      {
        throw new ArgumentException("It is not possible to use both newerThan and limitHours");
      }

      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["streamId"] = contentId;
      parameters["unreadOnly"] = unreadOnly.ToString();
      parameters["backfill"] = backfill.ToString();
      
      if (count.HasValue)
      {
        parameters["count"] = count.Value.ToString();
      }
      if (limitHours.HasValue)
      {
        parameters["hours"] = limitHours.Value.ToString();
      }
      if (newerThan.HasValue)
      {
        DateTime date = ((DateTime)newerThan.Value).ToUniversalTime();
        DateTime epoc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        parameters["newerThan"] = Math.Truncate(date.Subtract(epoc).TotalMilliseconds).ToString();
      }

      return (await Client.Request<MixesResponse>(HttpMethod.Get, "v3/mixes/contents", parameters, false, true, cancellationToken)).List;
    }
  }
}
