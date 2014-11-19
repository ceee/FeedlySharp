using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedlySharp.Models
{
  public class FeedlySubscription
  {
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Name { get; set; }

    [JsonProperty("visualUrl")]
    public Uri Image { get; set; }

    public string CoverColor { get; set; }

    [JsonProperty("coverUrl")]
    public Uri CoverImage { get; set; }

    [JsonProperty("iconUrl")]
    public Uri IconImage { get; set; }

    [JsonProperty("website")]
    public Uri Uri { get; set; }

    public List<FeedlyCategory> Categories { get; set; }

    public string[] Topics { get; set; }

    public int? Subscribers { get; set; }

    public double? Velocity { get; set; }

    [JsonProperty("updated")]
    public DateTime? UpdateDate { get; set; }
  }
}
