using LiteDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TodoAgility.Agile.Persistence.Framework.Projections;
using TodoAgility.Agile.Persistence.Projections;

namespace TodoAgility.Agile.Persistence.Model.Projections
{
    public class ActivityProjectionDbContext:ProjectionDbContext
    {
        public ActivityProjectionDbContext(string connectionString, BsonMapper modelBuilder)
        :base(connectionString, modelBuilder)
        {
            Activities = Database.GetCollection<ActivityProjection>("activity");
        }
        
        public ILiteCollection<ActivityProjection> Activities { get; }
        
        protected override void OnModelCreating(BsonMapper modelBuilder)
        {
            #region ConfigureActivityView

            modelBuilder.Entity<ActivityProjection>()
                .Field(k => k.ActivityId,"activityId")
                .Field(p=> p.ProjectId, "projectId")
                .Field(p=> p.Description,"description")
                .Field(p=> p.Status, "status")
                ;

            #endregion
        }
    }
}