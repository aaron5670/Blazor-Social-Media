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
                            Name = username
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

            if (user.Posts == null || user.Posts.Count == 0)
                user.Posts = new List<Post>();


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

        public User GetUserByUsername(string username)
        {
            // Todo async and  Error code!  
            using var userRepository = new UserRepository(new CommunityDbContext());
            var user = userRepository.Find(u => u.Name == username).Single();
            return user;
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

        public async Task<bool> HandlePostLike(Guid postId, string postUsername, string username)
        {
            using var userRepository = new UserRepository(new CommunityDbContext());
            var user = userRepository.Find(u => u.Name == username).Single();
            var author = userRepository.Find(u => u.Name == postUsername).Single();
            var post = author?.Posts.Single(p => p.PostId == postId);

            if (post != null) post.Likes += 1;

            if (user.LikedPosts == null || user.LikedPosts.Count == 0)
                user.LikedPosts = new List<LikedPost>();

            if (user.LikedPosts != null && user.LikedPosts.Any(p => p.PostId == postId))
            {
                foreach (var userLikedPost in user.LikedPosts)
                {
                    if (userLikedPost.PostId == postId)
                    {
                        user.LikedPosts.Remove(userLikedPost);
                    }
                }

                if (post != null) post.Likes -= 1;
                userRepository.Commit();
                return false;
            }

            user.LikedPosts.Add(new LikedPost {PostId = postId, Timestamp = DateTime.Now});
            userRepository.Commit();

            return true;
        }
    }
}