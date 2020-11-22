using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Persistence.Repositories.CounterRepos
{
    public class CounterRepository : ICounterRepository
    {
        private readonly ManagementDbContext _context;
        public CounterRepository(ManagementDbContext context)
        {
            _context = context;
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}
