using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
  public class Post
  {
    [Key]
    public Guid PostId { get; set; }
    
    public string Content { get; set; }
    
    public int Likes { get; set; }
    
    public DateTime Timestamp { get; set; }
  }
}