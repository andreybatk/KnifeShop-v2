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
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Knife> Knifes { get; set; }
        public DbSet<KnifeInfo> KnifesInfo { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Knife>(entity =>
            {
                entity.HasOne(k => k.KnifesInfo)
                    .WithOne()
                    .HasForeignKey<KnifeInfo>(ki => ki.Id);

                entity.HasKey(k => k.Id);
            });

            modelBuilder.Entity<KnifeInfo>().HasKey(k => k.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
