using System;
using CosmosDbSQLAPI;
using Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaApplication.Data
{
    public class UserService
    {
        public UserService()
        {
            // Generate some dummy data.  
            using (var userRepository = new UserRepository(new CommunityDbContext()))
            {
                userRepository.GenerateDatabase();

                for (var i = 0; i < 5; i++)
                {
                    userRepository
                        .Add(
                            new User
                            {
                                Name = "Bassam_" + i,
                                Posts = new Collection<Post>
                                {
                                    new Post
                                    {
                                        Title = "www.bassam.ml",
                                        Content = "My page"
                                    }
                                }
                            }
                        );
                }

                userRepository.Commit();
            }
        }

        public async Task  LoginUser(string username)
        {
            using (var userRepository = new UserRepository(new CommunityDbContext()))
            {
                userRepository
                    .Add(
                        new User
                        {
                            Name = username,
                            Posts = new Collection<Post>
                            {
                                new Post
                                {
                                    Title = "www.bassam.ml",
                                    Content = "My page"
                                }
                            }
                        }
                    );
                userRepository.Commit();
            }
        }

        public async Task AddPost(string username, string message)
        {
            using (var userRepository = new UserRepository(new CommunityDbContext()))
            {
                var user = userRepository.Find(u => u.Name == username).Single();
                user.Posts.Add(new Post() {Content = message, Title = "I love you CosmosDb!"});
                userRepository.Commit();
            }
        }

        public void DeleteUser(string username)
        {
            // Todo async and  Error code!  
            using (var userRepository = new UserRepository(new CommunityDbContext()))
            {
                var user = userRepository.Find(u => u.Name == username).Single();
                userRepository.Delete(user);
                userRepository.Commit();
            }
        }

        public ICollection<User> GetUsers()
        {
            // Todo async and Error code!  
            using (var userRepository = new UserRepository(new CommunityDbContext()))
            {
                userRepository.Include(nameof(User.Posts));
                Debug.WriteLine(userRepository.GetAll());
                return userRepository.GetAll();
            }
        }
    }
}