using Entities;
using Microsoft.EntityFrameworkCore;

namespace CosmosDbSQLAPI
{
    public class CommunityDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string endpoint = "https://social-media-account.documents.azure.com:443/";
            const string key = "ItGlzIfhyXuzAgkkartYlEUAxdaXQymJykr6sqkGmAwlZwXym1LIaNME9rn6YooLyDzKsBEnjxXgX1XUHfbfXw==";

            optionsBuilder.UseCosmos(endpoint, key, "CommunityDatabase");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToContainer("Users");
            modelBuilder.Entity<User>().OwnsMany(user => user.Posts);
            modelBuilder.Entity<User>().OwnsMany(user => user.LikedPosts);
            base.OnModelCreating(modelBuilder);
        }
    }
}