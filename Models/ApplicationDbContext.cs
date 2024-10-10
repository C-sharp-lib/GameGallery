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
        public DbSet<GameReviews> GameReviews { get; set; }
        public DbSet<UserReviews> UserReviews { get; set; }
        public DbSet<UserComments> UserComments { get; set; }
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
            modelBuilder.Entity<UserReviews>()
                .HasKey(uu => new { uu.UserId, uu.ReviewId });
            modelBuilder.Entity<UserComments>()
                .HasKey(bi => new { bi.UserId, bi.CommentId });
            modelBuilder.Entity<GameReviews>()
                .HasKey(ggg => new { ggg.GameId, ggg.ReviewId });
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
            modelBuilder.Entity<UserReviews>()
                .HasOne(us => us.Users)
                .WithMany(us => us.UserReviews)
                .HasForeignKey(us => us.UserId);
            modelBuilder.Entity<UserReviews>()
                .HasOne(ur => ur.Reviews)
                .WithMany(ur => ur.UsersReviews)
                .HasForeignKey(ur => ur.ReviewId);
            modelBuilder.Entity<GameGenres>()
                .HasOne(g => g.Games)
                .WithMany(gg => gg.GameGenres)
                .HasForeignKey(gi => gi.GameId);
            modelBuilder.Entity<GameGenres>()
                .HasOne(g => g.Genres)
                .WithMany(gg => gg.GameGenres)
                .HasForeignKey(fk => fk.GenreId);
            modelBuilder.Entity<BlogPosts>()
                .HasOne(bt => bt.User)
                .WithMany(bt => bt.BlogPosts)
                .HasForeignKey(bt => bt.UserBlogId);
        }
    }
}
