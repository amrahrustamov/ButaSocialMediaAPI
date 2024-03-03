using ButaAPI.Database.Base;
using ButaAPI.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace ButaAPI.Database
{
    public class ButaDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ButaDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is not IAuditable)
                    continue;

                IAuditable auditable = (IAuditable)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    auditable.CreatedAt = DateTime.UtcNow;
                    auditable.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    auditable.UpdatedAt = DateTime.UtcNow;
                }
            }


            return base.SaveChanges();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sqlDataSource = "Server=localhost;Port=5432;Database=ButaAPI;User Id=postgres;Password=postgres;";

            optionsBuilder.UseNpgsql(sqlDataSource);

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<FriendshipRequest> FriendshipsRequests { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Friendships> Friendships { get; set; }
    }
}