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


using System.Collections.Generic;

using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Persistence.Repositories.Domain
{
    public class  TaskRepository: IRepository<TaskState, Task>
    {
        private readonly IDictionary<EntityId, TaskState> _tasks = new Dictionary<EntityId, TaskState>();
        
        public void Save(IExposeValue<TaskState> state)
        {
            TaskState task = state.GetValue();
            var id = EntityId.From(task.Id);
            
            if (_tasks.ContainsKey(id))
            {
                _tasks[id] = task;
            }
            else
            {
                _tasks.Add(id,task);
            }
        }

        public Task FindBy(EntityId id)
        {
            return Task.FromState(_tasks[id]);
        }

        public void Commit()
        {
            
        }
    }
}