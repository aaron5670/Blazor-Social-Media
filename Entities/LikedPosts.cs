using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
  public class LikedPosts
  {
    [Key]
    public Guid PostId { get; set; }

    public DateTime Timestamp { get; set; }
  }
}