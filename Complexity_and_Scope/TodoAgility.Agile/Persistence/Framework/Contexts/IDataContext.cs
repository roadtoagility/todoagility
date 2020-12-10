using LiteDB;

namespace TodoAgility.Agile.Persistence.Framework.Contexts
{
    public interface IDataContext<T>
    {
        T Database { get; }
    }
}