using System;
using CosmosDbSQLAPI;
using Entities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMediaApplication.DTO;

namespace SocialMediaApplication.Data
{
    public class UserService
    {
        public async Task LoginUser(string username)
        {
            using var userRepository = new UserRepository(new CommunityDbContext());
            var user = userRepository.GetAll().Any(u => u.Name == username);
            
            if (!user)
            {
                userRepository
                    .Add(
                        new User
                        {
                            Name = username,
                            LikedPosts = new List<LikedPost>()
                        }
                    );
                userRepository.Commit();
            }
        }

        public async Task<Post> AddPost(string username, string message)
        {
            using var userRepository = new UserRepository(new CommunityDbContext());
            var user = userRepository.Find(u => u.Name == username).Single();
            Console.WriteLine(user.Posts);

            var newPost = new Post()
            {
                Content = message,
                Likes = 0,
                Timestamp = DateTime.Now
            };
            
            user.Posts.Add(newPost);
            userRepository.Commit();

            return newPost;
        }

        public void DeleteUser(string username)
        {
            // Todo async and  Error code!  
            using var userRepository = new UserRepository(new CommunityDbContext());
            var user = userRepository.Find(u => u.Name == username).Single();
            userRepository.Delete(user);
            userRepository.Commit();
        }

        public ICollection<User> GetUsers()
        {
            // Todo async and Error code!  
            using var userRepository = new UserRepository(new CommunityDbContext());
            userRepository.Include(nameof(User.Posts));
            Debug.WriteLine(userRepository.GetAll());
            return userRepository.GetAll();
        }
        
        public List<PostDTO> GetAllPosts()
        {
            // Todo async and Error code!  
            using var userRepository = new UserRepository(new CommunityDbContext());
            var posts = new List<PostDTO>();
            var users = userRepository.GetAll();
            users.ForEach(user =>
            {
                posts.AddRange(user.Posts.Select(post => 
                    new PostDTO
                    {
                        PostId = post.PostId,
                        Username = user.Name,
                        Content = post.Content,
                        Likes = post.Likes,
                        Timestamp = post.Timestamp
                    }).OrderByDescending(post => post.Timestamp));
            });
            return posts;
        }

        public async Task AddedLike(Guid postId, string postUsername, string username)
        {
            using var userRepository = new UserRepository(new CommunityDbContext());
            var user = userRepository.Find(u => u.Name == username).Single();

            var post = (user?.Posts).Single(post => post.PostId == postId);
            post.Likes += 1;
            
            Console.WriteLine("POSTID: " + postId);
            
            user.LikedPosts.Add(new LikedPost {PostId = postId, Timestamp = DateTime.Now});
            userRepository.Commit();
        }
    }
}