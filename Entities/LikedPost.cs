using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
  public class LikedPost
  {
    // [Key]
    public Guid PostId { get; set; }

    public DateTime Timestamp { get; set; }
  }
}