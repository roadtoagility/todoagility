using LiteDB;
using TodoAgility.Agile.Persistence.Framework.Contexts;

namespace TodoAgility.Agile.Persistence.Framework.EventStore
{
    public class EventStoreDbContext:LiteDbContext
    {
        public EventStoreDbContext(string connectionString, string aggregationTypeName, BsonMapper modelBuilder)
        :base(connectionString)
        {
            Aggregate = Database.GetCollection<AggregateState>(aggregationTypeName);
            OnModelCreating(modelBuilder);
        }
        
        public ILiteCollection<AggregateState> Aggregate { get; }
        
        private void OnModelCreating(BsonMapper modelBuilder)
        {
            #region ConfigureActivityView

            modelBuilder.Entity<AggregateState>()
                .Field(k => k.Id,"activityId")
                .Field(p=> p.AggregateType, "projectId")
                .Field(p=> p.Events,"events")
                .Field(p=> p.Version, "version")
                .Field(p=> p.CreateAt, "createdAt")
                ;

            #endregion
        }
    }
}