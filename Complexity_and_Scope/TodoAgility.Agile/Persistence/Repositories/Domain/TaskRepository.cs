// Copyright (C) 2020  Road to Agility
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Library General Public
// License as published by the Free Software Foundation; either
// version 2 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Library General Public License for more details.
//
// You should have received a copy of the GNU Library General Public
// License along with this library; if not, write to the
// Free Software Foundation, Inc., 51 Franklin St, Fifth Floor,
// Boston, MA  02110-1301, USA.
//


using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Persistence.Repositories.Domain
{
    public sealed class TaskRepository: IRepository<TaskState, Task>, IDisposable
    {
        private readonly TaskDbContext _contextDb;

        public TaskRepository(DbContextOptions<TaskDbContext> contextOptions)
        {
            _contextDb = new TaskDbContext(contextOptions);
            _contextDb.Database.EnsureDeleted();
            _contextDb.Database.EnsureCreated();
        }
        
        public void Save(IExposeValue<TaskState> state)
        {
            TaskState task = state.GetValue();
            task.RowVersion = task.RowVersion + 1;
            _contextDb.Tasks.Add(task);
        }

        public Task FindBy(EntityId id)
        {
            IExposeValue<uint> entityId = id;
            
            var task = _contextDb.Tasks.AsQueryable()
                .OrderByDescending( ob => ob.RowVersion )
                .ThenBy( tb => tb.Id)
                .First(t => t.Id == entityId.GetValue());
            
            return Task.FromState(task);
        }

        public void Commit()
        {
            var count = _contextDb.SaveChanges();
        }

        public void Dispose()
        {
            _contextDb?.Dispose();
        }
    }
}