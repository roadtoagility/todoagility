using LiteDB;
using TodoAgility.Agile.Persistence.Framework.Contexts;

namespace TodoAgility.Agile.Persistence.Projections
{
    public class ActivityProjectionDbContext:LiteDbContext
    {
        public ActivityProjectionDbContext(string connectionString, BsonMapper modelBuilder)
        :base(connectionString)
        {
            Activities = Database.GetCollection<ActivityProjection>("activity");
            OnModelCreating(modelBuilder);
        }
        
        public ILiteCollection<ActivityProjection> Activities { get; }
        
        private void OnModelCreating(BsonMapper modelBuilder)
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