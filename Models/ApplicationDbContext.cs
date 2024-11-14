using Microsoft.EntityFrameworkCore;

namespace GameGallery.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<BlogPosts> BlogPosts { get; set; }
        public DbSet<Games> Games { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<GameReviews> GameReviews { get; set; }
        public DbSet<UserBlogComments> UserBlogComments { get; set; }
        public DbSet<GameGenres> GameGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Users>()
                .HasKey(u => new { u.UserId });
            modelBuilder.Entity<BlogPosts>()
                .HasKey(b => new { b.BlogPostId });
            modelBuilder.Entity<Games>()
                .HasKey(g => new { g.GameId });
            modelBuilder.Entity<Reviews>()
                .HasKey(r => new { r.ReviewId });
            modelBuilder.Entity<Comments>()
                .HasKey(c => new { c.CommentId });
            modelBuilder.Entity<Genres>()
                .HasKey(gg => new { gg.GenreId });
            modelBuilder.Entity<CartItem>()
                .HasKey(c => new { c.CartItemId });
            modelBuilder.Entity<UserBlogComments>()
                .HasKey(bi => new { bi.UserId, bi.CommentId, bi.BlogPostId });
            modelBuilder.Entity<GameReviews>()
                .HasKey(ggg => new { ggg.GameId, ggg.ReviewId, ggg.UserId });
            modelBuilder.Entity<GameGenres>()
                .HasKey(gggg => new { gggg.GameId, gggg.GenreId });
            modelBuilder.Entity<GameReviews>()
                .HasOne(gr => gr.Games)
                .WithMany(gr => gr.GameReviews)
                .HasForeignKey(gr => gr.GameId);
            modelBuilder.Entity<GameReviews>()
                .HasOne(grr => grr.Reviews)
                .WithMany(grr => grr.GameReviews)
                .HasForeignKey(grr => grr.ReviewId);
            modelBuilder.Entity<GameReviews>()
                .HasOne(grrr => grrr.Users)
                .WithMany(grrr => grrr.GameReviews)
                .HasForeignKey(grrr => grrr.UserId);
            modelBuilder.Entity<UserBlogComments>()
                .HasOne(u => u.Comments)
                .WithMany(u => u.UserBlogComments)
                .HasForeignKey(u => u.CommentId);
            modelBuilder.Entity<UserBlogComments>()
                .HasOne(u => u.Users)
                .WithMany(u => u.UserBlogComments)
                .HasForeignKey(u => u.UserId);
            modelBuilder.Entity<GameGenres>()
                .HasOne(g => g.Games)
                .WithMany(gg => gg.GameGenres)
                .HasForeignKey(gi => gi.GameId);
            modelBuilder.Entity<GameGenres>()
                .HasOne(g => g.Genres)
                .WithMany(gg => gg.GameGenres)
                .HasForeignKey(fk => fk.GenreId);
            modelBuilder.Entity<BlogPosts>()
                .HasOne(u => u.User)
                .WithMany(u => u.BlogPosts)
                .HasForeignKey(u => u.UserId);
        }
    }
}
