using Microsoft.EntityFrameworkCore;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Persistence.Repositories.Domain
{
    public class ProjectDbContext: AggregateDbContext
    {
        public ProjectDbContext(DbContextOptions options) 
            : base(options)
        {
        }
        
        public DbSet<ProjectState> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            #region ConfigureTask
            
            modelBuilder.Entity<ProjectState>(
                b =>
                {
                    b.Property(e => e.Id);
                    b.HasKey(k => new { k.Id });
                    b.Property(e => e.Description).IsRequired();
                    b.Property(e => e.CreateAt);
                });
            #endregion
        }
    }
}