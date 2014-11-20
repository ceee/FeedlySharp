using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedlySharp.Models
{
  public class FeedlyEntry
  {
    public string Id { get; set; }

    public string Fingerprint { get; set; }

    public string OriginId { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    [JsonProperty("published")]
    public DateTime? PublishedDate { get; set; }

    [JsonProperty("crawled")]
    public DateTime? CrawledDate { get; set; }

    [JsonProperty("updated")]
    public DateTime? UpdateDate { get; set; }

    [JsonProperty("unread")]
    public bool IsUnread { get; set; }

    public string[] Keywords { get; set; }

    public List<FeedlyTag> Tags { get; set; }

    public int Engagement { get; set; }

    [JsonProperty("alternate")]
    public List<FeedlyLink> AlternateLinks { get; set; }

    [JsonProperty("canonical")]
    public List<FeedlyLink> CanonicalLinks { get; set; }

    public FeedlyText Summary { get; set; }

    public FeedlyText Content { get; set; }

    public FeedlyOrigin Origin { get; set; }

    public List<FeedlyCategory> Categories { get; set; }
  }
}
