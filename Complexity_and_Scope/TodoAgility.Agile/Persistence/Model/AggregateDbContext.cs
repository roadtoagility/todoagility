using Microsoft.EntityFrameworkCore;

namespace TodoAgility.Agile.Persistence.Model
{
    public class AggregateDbContext : DbContext
    {
        public AggregateDbContext(DbContextOptions options) 
            : base(options)
        {

        }
    }
}