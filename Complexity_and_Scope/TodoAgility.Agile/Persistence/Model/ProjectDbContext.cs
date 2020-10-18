using Microsoft.EntityFrameworkCore;

namespace TodoAgility.Agile.Persistence.Model
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
            
            #region ConfigureProject
            
            modelBuilder.Entity<ProjectState>(
                b =>
                {
                    b.Property(e => e.TransactionId).ValueGeneratedNever().IsRequired();
                    b.HasKey(k => new { k.TransactionId });
                    b.Property(e => e.Id).ValueGeneratedNever().IsRequired();
                    b.Property(e => e.Description).IsRequired();
                    b.Property(e => e.RowVersion).IsRequired();
                    b.Property(e => e.CreateAt);
                    b.HasIndex(idx => new {idx.Id, idx.RowVersion}).IsUnique();
                });
            #endregion
        }
    }
}
