using Microsoft.EntityFrameworkCore;
using TodoAgility.Agile.Persistence.Framework.Model;
using TodoAgility.Agile.Persistence.Projections.Activity;
using TodoAgility.Agile.Persistence.Projections.Project;

namespace TodoAgility.Agile.Persistence.Model
{
    public class ManagementDbContext : AggregateDbContext
    {
        public ManagementDbContext(DbContextOptions options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            Database.Migrate();
        }

        public DbSet<ProjectState> Projects { get; set; }

        public DbSet<ActivityStateReference> ActivitiesRef { get; set; }

        public DbSet<ActivityState> Activities { get; set; }

        public DbSet<ProjectStateReference> ProjectsRef { get; set; }

        public DbSet<LastProjectsProjection> LastProjects { get; set; }

        public DbSet<ActivityByProjectProjection> ActivitiesByProject { get; set; }

        public DbSet<FeaturedProjectsProjection> FeaturedProjects { get; set; }

        public DbSet<CounterRegistry> Counters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region ConfigureProject

            modelBuilder.Entity<CounterRegistry>(
                b =>
                {
                    b.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
                    b.HasKey(e => e.Id);
                    b.Property(e => e.Label).IsRequired();
                    b.Property(e => e.Type).IsRequired();
                    b.Property(e => e.Order).IsRequired();
                    b.Property(p => p.PersistenceId);
                    b.HasQueryFilter(q => !q.IsDeleted);
                    b.Property(e => e.CreateAt);
                    b.Property(e => e.RowVersion).ValueGeneratedOnAddOrUpdate().IsRowVersion();
                });

            modelBuilder.Entity<FeaturedProjectsProjection>(
               b =>
               {
                   b.Property(e => e.Id).ValueGeneratedNever().IsRequired();
                   b.HasKey(e => e.Id);
                   b.Property(e => e.Name).IsRequired();
                   b.Property(p => p.Icon);
               });

            modelBuilder.Entity<ActivityByProjectProjection>(
                b =>
                {
                    b.Property(e => e.Id).ValueGeneratedNever().IsRequired();
                    b.HasKey(e => e.Id);
                    b.Property(e => e.Title).IsRequired();
                    b.Property(p => p.ProjectId).IsRequired();
                });

            modelBuilder.Entity<LastProjectsProjection>(
                b =>
                {
                    b.Property(e => e.Id).ValueGeneratedNever().IsRequired();
                    b.HasKey(e => e.Id);
                    b.Property(e => e.Budget).IsRequired();
                    b.Property(p => p.Client);
                });

            modelBuilder.Entity<ProjectState>(
                b =>
                {
                    b.Property(e => e.ProjectId).ValueGeneratedNever().IsRequired();
                    b.HasKey(e => e.ProjectId);
                    b.Property(e => e.Description).IsRequired();
                    b.Property(p => p.PersistenceId);
                    b.HasQueryFilter(q => !q.IsDeleted);
                    b.Property(e => e.CreateAt);
                    b.Property(e => e.RowVersion).ValueGeneratedOnAddOrUpdate().IsRowVersion();
                });

            modelBuilder.Entity<ActivityStateReference>(
                b =>
                {
                    b.Property(k => k.ActivityReferenceId)
                        .ValueGeneratedNever().IsRequired();
                    b.HasKey(k => k.ActivityReferenceId);
                    b.HasOne<ProjectState>()
                        .WithMany(m => m.Activities)
                        .HasForeignKey(f => f.ProjectId);

                    b.Property(p => p.PersistenceId);
                    b.HasQueryFilter(q => !q.IsDeleted);
                    b.Property(e => e.CreateAt);
                    b.Property(e => e.RowVersion).ValueGeneratedOnAddOrUpdate().IsRowVersion();
                    b.HasIndex(idx => new {idx.ActivityReferenceId, idx.ProjectId}).IsUnique();
                });

            modelBuilder.Entity<ActivityState>(
                b =>
                {
                    b.Property(e => e.ActivityId).ValueGeneratedNever();
                    b.HasKey(e => e.ActivityId);
                    b.Property(e => e.Description).IsRequired();
                    b.Property(e => e.Status).IsRequired();
                    b.Property(p => p.ProjectId).IsRequired();
                    b.Property(p => p.PersistenceId);
                    b.Property(e => e.CreateAt).IsRequired();
                    b.Property(e => e.IsDeleted).IsRequired();
                    b.Property(e => e.RowVersion).ValueGeneratedOnAddOrUpdate().IsRowVersion();

                    b.HasQueryFilter(qf => !qf.IsDeleted);
                });

            modelBuilder.Entity<ProjectStateReference>(
                b =>
                {
                    b.Property(e => e.ProjectId).ValueGeneratedNever();
                    b.HasKey(e => e.ProjectId);
                    b.Property(p => p.PersistenceId);
                    b.Property(p => p.Description);
                    b.Property(e => e.CreateAt).IsRequired();
                    b.Property(e => e.IsDeleted).IsRequired();
                    b.Property(e => e.RowVersion).ValueGeneratedOnAddOrUpdate().IsRowVersion();
                    b.HasQueryFilter(qf => !qf.IsDeleted);
                });

            #endregion
        }
    }
}