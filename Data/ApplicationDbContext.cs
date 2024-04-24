using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RecipePage.Models.Entities;

namespace RecipePage.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Ingredients> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredients>()
                .Property(i => i.Amount)
                .HasColumnType("decimal(18,2)"); // Adjust precision and scale as needed

            base.OnModelCreating(modelBuilder);
        }

    }
}