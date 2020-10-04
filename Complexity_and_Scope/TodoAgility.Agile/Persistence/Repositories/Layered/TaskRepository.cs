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
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Persistence.Repositories.Layered
{
    public class  TaskRepository: IRepository<TaskState>
    {
        private readonly IDictionary<uint, TaskState> _tasks = new Dictionary<uint, TaskState>();
        
        public void Save(TaskState task)
        {
            if (_tasks.ContainsKey(task.Id))
            {
                _tasks[task.Id] = task;
            }
            else
            {
                _tasks.Add(task.Id,task);
            }
        }

        public TaskState FindBy(uint id)
        {
            return _tasks[id];
        }

        public void Commit()
        {
            
        }
    }
}