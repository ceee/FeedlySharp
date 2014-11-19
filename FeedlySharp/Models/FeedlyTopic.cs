using Newtonsoft.Json;
using System;
using System.Linq;

namespace FeedlySharp.Models
{
  public class FeedlyTopic
  {
    public string Id { get; set; }

    public string Name { get { return Id == null ? String.Empty : Id.Split('/').Last(); } }

    [JsonProperty("updated")]
    public DateTime? UpdateDate { get; set; }

    [JsonProperty("created")]
    public DateTime? CreateDate { get; set; }

    public Interest Interest { get; set; }
  }

  public enum Interest
  {
    Unknown,
    Low,
    Medium,
    High
  }
}
