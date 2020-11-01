using LiteDB;

namespace TodoAgility.Agile.Persistence.Framework.Projections
{
    public abstract class ProjectionDbContext
    {
        protected ProjectionDbContext(string connectionString, BsonMapper modelBuilder)
        {
            Database = new LiteDatabase(connectionString);
            OnModelCreating(modelBuilder);
        }

        public ILiteDatabase Database { get; }
       
        protected abstract void OnModelCreating(BsonMapper modelBuilder);
    }
}