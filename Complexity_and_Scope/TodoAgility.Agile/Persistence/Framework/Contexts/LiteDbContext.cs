using LiteDB;

namespace TodoAgility.Agile.Persistence.Framework.Contexts
{
    public class LiteDbContext: IDataContext<ILiteDatabase>
    {
        protected LiteDbContext(string connectionString)
        {
            Database = new LiteDatabase(connectionString);
        }

        public ILiteDatabase Database { get; }

    }
}