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
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Domain.Aggregations
{
    public class TaskAggregationRoot 
    {
        private Task _task;
        
        public void AddTask(string description)
        {
            _task = Task.FromDescription(Description.From(description));
        }
        
        public void UpdateTask(TaskState task, string description)
        {
            if(Description.From(task.Description) == Description.From(description))
                throw new ApplicationException("A nova descrição não pode ser igual a anterior.");
                
            _task = Task.FromStateAndPatch(task,new Task.Patch(description));
        }
        
        public void CompleteTask(TaskId id)
        {
            
        }
        
        public Task Changes()
        {
            return _task;
        }
    }
}