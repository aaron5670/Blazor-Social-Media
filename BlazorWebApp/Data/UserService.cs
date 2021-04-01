using CosmosDbSQLAPI;
using Entities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task AddPost(string username, string message)
        {
            using var userRepository = new UserRepository(new CommunityDbContext());
            var user = userRepository.Find(u => u.Name == username).Single();
            user.Posts.Add(new Post()
            {
                Content = message,
                Likes = 0
            });
            userRepository.Commit();
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
                        Username = user.Name,
                        Content = post.Content,
                        Likes = post.Likes
                    }));
            });
            return posts;
        }
    }
}