using System;
using System.Collections.Generic;

namespace Entities
{
  public class User
  {
    public Guid UserId { get; set; }

    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }
    
    public ICollection<LikedPosts> LikedPosts { get; set; }
  }
}