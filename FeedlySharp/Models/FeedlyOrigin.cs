using Newtonsoft.Json;
using System;

namespace FeedlySharp.Models
{
  public class FeedlyOrigin
  {
    [JsonProperty("streamId")]
    public string Id { get; set; }

    [JsonProperty("htmlUrl")]
    public Uri Uri { get; set; }

    [JsonProperty("title")]
    public string Name { get; set; }
  }
}
