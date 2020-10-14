using Microsoft.EntityFrameworkCore;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Persistence.Repositories.Domain
{
    public class AggregateDbContext : DbContext
    {
        public AggregateDbContext(DbContextOptions options) 
            : base(options)
        {

        }
    }
}