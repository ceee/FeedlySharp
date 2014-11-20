
namespace FeedlySharp.Models
{
  public class FeedlyText
  {
    public Direction Direction { get; set; }

    public string Content { get; set; }
  }

  public enum Direction
  {
    Unknown,
    Ltr,
    Rtl
  }
}
