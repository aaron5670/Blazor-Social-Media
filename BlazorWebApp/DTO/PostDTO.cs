using System;
using Entities;

namespace SocialMediaApplication.DTO
{
    public class PostDTO
    {
        public Guid PostId { get; set; }
        public string Username { get; set; }
    
        public string Content { get; set; }
        
        public int Likes { get; set; }
        
        public DateTime Timestamp { get; set; }
    }
}