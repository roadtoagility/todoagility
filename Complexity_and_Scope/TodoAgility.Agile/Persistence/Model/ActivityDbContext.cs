using Microsoft.EntityFrameworkCore;
using TodoAgility.Agile.Persistence.Framework.Model;

namespace TodoAgility.Agile.Persistence.Model
{
    public class ActivityDbContext : AggregateDbContext
    {
        public ActivityDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ActivityState> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region ConfigureTask

            modelBuilder.Entity<ActivityState>(
                b =>
                {
                    b.Property(e => e.ActivityId).ValueGeneratedNever();
                    b.HasKey(e => e.ActivityId);
                    b.Property(e => e.Description).IsRequired();
                    b.Property(e => e.Status).IsRequired();
                    b.Property(e => e.ProjectId).IsRequired();

                    b.Property(p => p.PersistenceId);
                    b.Property(e => e.CreateAt).IsRequired();
                    b.Property(e => e.IsDeleted).IsRequired();
                    b.Property(e => e.RowVersion).ValueGeneratedOnAddOrUpdate().IsRowVersion();

                    b.HasQueryFilter(qf => !qf.IsDeleted);
                    b.HasIndex(idx => new {idx.ProjectId, idx.ActivityId});
                });

            #endregion
        }
    }
}