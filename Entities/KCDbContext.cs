using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeControl.Entities
{
    public class KCDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public KCDbContext(DbContextOptions<KCDbContext> options)
            : base(options)
        {
        }

        public DbSet<Test> Tests { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
    
            builder.Entity<Solution>()
                .HasOne(_ => _.User)
                .WithMany(_ => _.Solutions)
                .HasForeignKey(_ => _.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}