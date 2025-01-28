using Microsoft.EntityFrameworkCore;
using TASK_session5.Data.models;

namespace TASK_session5.Data
{
    public class ApplicationDbContext: DbContext
    {
        
    
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Product> products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasIndex(x => x.Name).IsUnique();
        }


    }
}
