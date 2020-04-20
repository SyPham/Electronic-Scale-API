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
        public DbSet<Line> Line { get; set; }
        public DbSet<ModelName> ModelNames { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<ModelNo> ModelNos { get; set; }
        public DbSet<MapModel> MapModel { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}