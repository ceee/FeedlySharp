using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedlySharp.Models
{
  public class FeedlyFeed
  {
    public virtual string Id { get; set; }

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

    public string[] Topics { get; set; }

    public string Language { get; set; }

    public string Description { get; set; }

    public int? Subscribers { get; set; }

    public double? Velocity { get; set; }

    [JsonProperty("facebookUsername")]
    public string FacebookName { get; set; }

    public int? FacebookLikes { get; set; }

    [JsonProperty("twitterScreenName")]
    public string TwitterName { get; set; }

    public int? TwitterFollowers { get; set; }
  }
}
