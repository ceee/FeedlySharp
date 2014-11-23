using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedlySharp.Models
{
  public class FeedlyReadOperations
  {
    [JsonProperty("feeds")]
    public List<FeedlyReadOperationsFeed> ReadFeeds { get; set; }

    [JsonProperty("entries")]
    public List<string> ReadEntries { get; set; }

    [JsonProperty("unread")]
    public List<string> UnreadEntries { get; set; }
  }

  public class FeedlyReadOperationsFeed
  {
    [JsonProperty("asOf")]
    public DateTime? ReadAt { get; set; }

    public string Id { get; set; }
  }
}
