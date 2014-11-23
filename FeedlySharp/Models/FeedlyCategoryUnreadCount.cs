using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedlySharp.Models
{
  public class FeedlyCategoryUnreadCount
  {
    [JsonProperty("updated")]
    public DateTime? UpdatedAt { get; set; }

    public int Count { get; set; }

    public string Id { get; set; }

    public string Name { get { return Id == null ? String.Empty : Id.Split('/').Last(); } }
  }
}
