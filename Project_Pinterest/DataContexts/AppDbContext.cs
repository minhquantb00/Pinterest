using Microsoft.EntityFrameworkCore;
using Project_Pinterest.Entities;

namespace Project_Pinterest.DataContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext() { }
        public DbSet<Collection> collections { get; set; }
        public DbSet<ConfirmEmail> confirmEmails { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<PostCollection> postCollections { get; set; }
        public DbSet<PostStatus> postsStatus { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }
        public DbSet<Relationship> relationships { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<UserCommentPost> userCommentPosts { get; set; }
        public DbSet<UserLikeCommentOfPost> userLikeCommentOfPosts { get; set; }
        public DbSet<UserLikePost> userLikePosts { get; set; }
        public DbSet<UserStatus> userStatuses { get; set; }
        public DbSet<Report> reports { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
