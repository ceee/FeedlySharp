using System;
using System.Linq;

namespace FeedlySharp.Models
{
  public class FeedlyTag
  {
    public string Id { get; set; }

    public string Name { get { return Id == null ? String.Empty : Id.Split('/').Last(); } }

    public string Label { get; set; }

    public bool IsGlobal { get { return Name.StartsWith("global."); } }
  }
}
