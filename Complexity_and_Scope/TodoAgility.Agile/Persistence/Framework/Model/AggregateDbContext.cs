using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TodoAgility.Agile.Persistence.Framework.Model
{
    public class AggregateDbContext : DbContext
    {
        public AggregateDbContext(DbContextOptions options) 
            : base(options)
        {
            
        }

        public override int SaveChanges()
        {
            UpdateSoftDeleteLogic();
            return base.SaveChanges();
        }
       
        private void UpdateSoftDeleteLogic()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                    default:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;
                }
            }
        }
    }
}