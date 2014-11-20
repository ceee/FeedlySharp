using Newtonsoft.Json;
using System;

namespace FeedlySharp.Models
{
  public class FeedlyImage
  {
    [JsonProperty("url")]
    public Uri Uri { get; set; }

    public int? Width { get; set; }

    public int? Height { get; set; }

    public string ContentType { get; set; }
  }
}
