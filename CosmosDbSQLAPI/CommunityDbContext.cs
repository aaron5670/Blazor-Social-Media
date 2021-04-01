using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CosmosDbSQLAPI
{
    public class CommunityDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<LikedPost> LikedPosts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var Endpoint = "https://localhost:8081";
            var Key = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

            optionsBuilder.UseCosmos(Endpoint, Key, "CommunityDatabase")
                .UseLoggerFactory(GenerateLoggerFactory())
                .EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToContainer("Users");
            modelBuilder.Entity<User>().OwnsMany(user => user.Posts);
            modelBuilder.Entity<User>().OwnsMany(user => user.LikedPosts);
            base.OnModelCreating(modelBuilder);
        }

        private ILoggerFactory GenerateLoggerFactory()
        {
            var serviceCollection = new ServiceCollection();

            //Query debugger / logger
            serviceCollection.AddLogging(builder =>
                builder.AddConsole().AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Trace));

            return serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
        }
    }
}