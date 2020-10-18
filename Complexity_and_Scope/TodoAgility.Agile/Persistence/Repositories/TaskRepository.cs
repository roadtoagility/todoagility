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
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Persistence.Repositories
{
    public sealed class TaskRepository: Repository<TaskState,Task>, ITaskRepository
    {
        private TaskDbContext TaskDbContext => Context as TaskDbContext;

        public TaskRepository(DbContext context)
        :base(context)
        {
            Context.Database.EnsureCreated();
        }

        public override Task Get(EntityId id)
        {
            IExposeValue<uint> entityId = id;
            var task = TaskDbContext.Tasks.AsQueryable()
                .OrderByDescending( ob => ob.RowVersion )
                .ThenBy( tb => tb.Id)
                .First(t => t.Id == entityId.GetValue());
            return Task.FromState(task);
        }

        public override IEnumerable<Task> Find(Expression<Func<TaskState, bool>> predicate)
        {
            return TaskDbContext.Tasks.Where(predicate).Select(t=> Task.FromState(t));
        }
        
        protected override TaskState PrepareToAdd(IExposeValue<TaskState> entity)
        {
            var newState = entity.GetValue();
            newState.RowVersion++;
            try
            {
                var task = Get(EntityId.From(newState.Id));
                IExposeValue<TaskState> oldState = task;

                if (oldState.GetValue().RowVersion >= newState.RowVersion)
                {
                    throw new DbUpdateException("O Registro já foi alterado");
                }
            }
            catch (InvalidOperationException ex)
            {
                
            }
            return newState;
        }
    }
}