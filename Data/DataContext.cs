using EC_API.Models;
using Microsoft.EntityFrameworkCore;

namespace EC_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<GlueIngredient> GlueIngredient { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Glue> Glues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}