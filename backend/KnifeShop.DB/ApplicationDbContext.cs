using KnifeShop.DB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KnifeShop.DB
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Knife> Knifes { get; set; }
        public DbSet<KnifeInfo> KnifesInfo { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<FavoriteKnife> FavoriteKnifes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Knife>().HasKey(k => k.Id);

            modelBuilder.Entity<Knife>().
                HasOne(k => k.KnifeInfo)
                .WithOne()
                .HasForeignKey<KnifeInfo>(ki => ki.Id);

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
        }
    }
}
