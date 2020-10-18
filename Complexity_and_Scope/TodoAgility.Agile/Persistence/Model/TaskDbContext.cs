using Microsoft.EntityFrameworkCore;

namespace TodoAgility.Agile.Persistence.Model
{
    public class TaskDbContext: AggregateDbContext
    {
        public TaskDbContext(DbContextOptions options) 
            : base(options)
        {
        }
        
        public DbSet<TaskState> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            #region ConfigureTask
            
            modelBuilder.Entity<TaskState>(
                b =>
                {
                    b.Property(e => e.TransactionId).ValueGeneratedNever();
                    b.HasKey(k => new { k.TransactionId });
                    b.Property(e => e.Id).ValueGeneratedNever();
                    b.HasIndex(idx => new {idx.Id});
                    b.Property(e => e.Description).IsRequired();
                    b.Property(e => e.Status).IsRequired();
                    b.Property(e => e.ProjectId).IsRequired();
                    b.HasIndex(idx => new {idx.ProjectId});
                    b.Property(e => e.CreateAt);
                    b.Property(e => e.RowVersion);
                });
            #endregion
        }
    }
}