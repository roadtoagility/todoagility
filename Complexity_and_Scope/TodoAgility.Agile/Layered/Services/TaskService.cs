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
using TodoAgility.Agile.Layered.BusinessObjects;
using TodoAgility.Agile.Persistence.Model;
using TodoAgility.Agile.Persistence.Repositories.Layered;

namespace TodoAgility.Agile.Layered.Services
{
    public class TaskService:ITaskService
    {
        private readonly IRepository<TaskState> _taskRepository;
        
        public TaskService(IRepository<TaskState> taskRepository)
        {
            _taskRepository = taskRepository;
        }
        
        public void AddTask(Task task)
        {
            if (string.IsNullOrEmpty(task.Description) || string.IsNullOrWhiteSpace(task.Description))
                throw new ArgumentNullException(nameof(task.Description));
            
            _taskRepository.Save(new TaskState(1,task.Description,task.Id, task.ProjectId));
        }

        public void UpdateTask(uint id, Task task)
        {
            if (id == 0)
            {
                throw new ArgumentException(nameof(id));
            }

            if (string.IsNullOrEmpty(task.Description) || string.IsNullOrWhiteSpace(task.Description))
            {
                throw new ArgumentNullException(nameof(task.Description));
            }

            var found = _taskRepository.FindBy(id);

            if (found != null)
            {
                var taskUpdate = new TaskState(found.Status,task.Description,found.Id,found.ProjectId);
                _taskRepository.Save(taskUpdate);    
            }
            else
            {
                throw new ArgumentNullException(nameof(id));
            }
        }
    }
}