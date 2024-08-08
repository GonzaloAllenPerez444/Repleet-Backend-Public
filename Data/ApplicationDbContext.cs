using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repleet.Models.Entities;

namespace Repleet.Data
{
    public class ApplicationDbContext : DbContext //was originally IdentityDbContext, maybe we have to re-add it once we introduce users

    {
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProblemSet> ProblemSets { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optionsBuilder)
            : base(optionsBuilder)
        {
            
        }
    
        /* I don't believe i need this since both program.cs and integration tests should have the connection in their
         * optionsbuilder
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //should it be <ApplicationDBContext>?
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RepleetDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
            
            
        } */



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Problem>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Problems)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Problems)
                .WithOne(p => p.Category);

            modelBuilder.Entity<ProblemSet>()
                .HasMany(ps => ps.Categories);
                

           // modelBuilder.Entity<ProblemSet>().HasData(DefaultData.GetDefaultProblemSet()); I considered seeding for default data but ill try CREATE Command First
        }
    }
}
