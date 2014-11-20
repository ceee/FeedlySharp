using Newtonsoft.Json;
using System;

namespace FeedlySharp.Models
{
  public class FeedlyLink
  {
    [JsonProperty("href")]
    public Uri Uri { get; set; }

    [JsonProperty("type")]
    public string ContentType { get; set; }
  }
}
