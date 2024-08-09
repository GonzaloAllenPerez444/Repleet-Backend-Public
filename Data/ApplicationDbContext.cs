using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repleet.Models.Entities;

namespace Repleet.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> //used to be just plain DbContext

    {
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProblemSet> ProblemSets { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optionsBuilder)
            : base(optionsBuilder)
        {
            
        }
    
        



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.ProblemSet)
            .WithOne()
            .HasForeignKey<ApplicationUser>(u => u.ProblemSetId);

            modelBuilder.Entity<Problem>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Problems)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Problems)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId); 
                

            modelBuilder.Entity<ProblemSet>()
                .HasMany(ps => ps.Categories)
                .WithOne()
                .HasForeignKey("ProblemSetId"); //this part is just for clarity
                

           
        }
    }
}
