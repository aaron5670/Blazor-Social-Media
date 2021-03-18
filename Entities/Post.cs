using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
  public class Post
  {
    [Key]
    public Guid PostId { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public User User { get; set; }

  }
}