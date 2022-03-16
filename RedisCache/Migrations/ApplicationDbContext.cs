using RedisCache.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RedisCache.Migrations
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Pessoa> Pessoa { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            BrandModelBuilder(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void BrandModelBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>()
                           .HasKey(d => d.Id);
            modelBuilder.Entity<Pessoa>()
              .HasIndex(d => d.Id)
               .IsUnique();

            modelBuilder.Entity<Pessoa>()
                .Property(p => p.Name).HasMaxLength(100).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
        public async Task SaveChanges(CancellationToken cancellationToken = default(CancellationToken))
        {
            await SaveChangesAsync();
        }
    }
}
