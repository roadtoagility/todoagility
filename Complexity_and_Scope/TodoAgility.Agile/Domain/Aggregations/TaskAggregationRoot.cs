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

namespace TodoAgility.Agile.Domain.Aggregations
{
    public sealed class TaskAggregationRoot:AggregationRoot<Guid, Task> 
    {
        private readonly Task _currentTask;

        /// <summary>
        /// load an aggregate from store
        /// </summary>
        /// <param name="currentTask"></param>
        private TaskAggregationRoot(Task currentTask)
        {
            Id = Guid.NewGuid();
            _currentTask = currentTask;
        }

        /// <summary>
        /// to register new aggregate as change
        /// </summary>
        /// <param name="descr"></param>
        /// <param name="projectId"></param>
        private TaskAggregationRoot(Description descr, EntityId entityId, EntityId projectId)
        :this(Task.From(descr, entityId, projectId))
        {
            Change(_currentTask);
        }

        /// <summary>
        /// update currentTask value allowed from patch interface
        /// </summary>
        /// <param name="patchTask"></param>
        public void UpdateTask(Task.Patch patchTask)
        {
            var change = Task.CombineWithPatch(_currentTask, patchTask);
                
            Change(change);
        }

        #region Aggregation contruction
        
        /// <summary>
        /// reconstructing aggregation from current state loaded from store
        /// </summary>
        /// <param name="currentState"></param>
        /// <returns></returns>
        public static TaskAggregationRoot ReconstructFrom(Task currentState)
        {
            return new TaskAggregationRoot(currentState);
        }

        /// <summary>
        /// creating new aggregation as design by business based on business concepts 
        /// </summary>
        /// <param name="descr"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static TaskAggregationRoot CreateFrom(Description descr, EntityId entityId, EntityId projectId)
        {
            return new TaskAggregationRoot(descr, entityId, projectId);
        }
        
        #endregion
    }
}