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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var Endpoint = "https://social-media-account.documents.azure.com:443/";
            var Key = "S33BzGJ9MmPO8gdrnDuBQfDJwovc8ud5EwEIX2UfI3DhJDOlcEHo2BqatxWnjcjpGbUYpHr6lD2SKBzAdyVcyw==";

            optionsBuilder.UseCosmos(Endpoint, Key, "CommunityDatabase")
                .UseLoggerFactory(GenerateLoggerFactory())
                .EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToContainer("Users");
            modelBuilder.Entity<User>().OwnsMany(s => s.Posts);
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