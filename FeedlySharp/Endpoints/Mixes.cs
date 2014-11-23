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
