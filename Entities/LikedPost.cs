using System;

namespace Entities
{
  public class LikedPost
  {
    // [Key]
    public Guid PostId { get; set; }

    public DateTime Timestamp { get; set; }
  }
}