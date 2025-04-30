using KnifeShop.DB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KnifeShop.DB
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Knife> Knifes { get; set; }
        public DbSet<KnifeInfo> KnifesInfo { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<FavoriteKnife> FavoriteKnifes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<KnifeCategory> KnifeCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Knife>().HasKey(k => k.Id);

            modelBuilder.Entity<Knife>().
                HasOne(k => k.KnifeInfo)
                .WithOne()
                .HasForeignKey<KnifeInfo>(ki => ki.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<KnifeInfo>().HasKey(k => k.Id);

            modelBuilder.Entity<FavoriteKnife>()
                .HasKey(fk => new { fk.UserId, fk.KnifeId });

            modelBuilder.Entity<FavoriteKnife>()
                .HasOne(fk => fk.User)
                .WithMany(u => u.FavoriteKnifes)
                .HasForeignKey(fk => fk.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavoriteKnife>()
                .HasOne(fk => fk.Knife)
                .WithMany(k => k.FavoriteKnifes)
                .HasForeignKey(fk => fk.KnifeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
               .HasOne(o => o.User) 
               .WithMany(u => u.Orders)
               .HasForeignKey(o => o.UserId) 
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Knife)
                .WithMany()
                .HasForeignKey(oi => oi.KnifeId) 
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => oi.Id);

            modelBuilder.Entity<KnifeCategory>()
                .HasKey(kc => new { kc.KnifeId, kc.CategoryId });

            modelBuilder.Entity<KnifeCategory>()
                .HasOne(kc => kc.Knife)
                .WithMany(k => k.KnifeCategories)
                .HasForeignKey(kc => kc.KnifeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<KnifeCategory>()
                .HasOne(kc => kc.Category)
                .WithMany(c => c.KnifeCategories)
                .HasForeignKey(kc => kc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
