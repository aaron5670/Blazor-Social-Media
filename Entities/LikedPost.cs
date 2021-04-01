using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
  public class LikedPost
  {
    // [Key]
    public string PostId { get; set; }

    public DateTime Timestamp { get; set; }
  }
}