using Newtonsoft.Json;
using System;

namespace FeedlySharp.Models
{
  public class FeedlyCategory
  {
    public string Id { get; set; }

    public string Label { get; set; }

    public bool IsGlobal { get; set; }
  }
}
