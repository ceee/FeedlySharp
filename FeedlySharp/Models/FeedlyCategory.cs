using Newtonsoft.Json;
using System;
using System.Linq;

namespace FeedlySharp.Models
{
  public class FeedlyCategory
  {
    [JsonProperty("id")]
    public string Id { get; set; }

    public string Name { get { return Id == null ? String.Empty : Id.Split('/').Last(); } }

    [JsonProperty("label")]
    public string Label { get; set; }

    public bool IsGlobal { get { return Name.StartsWith("global."); } }
  }
}
