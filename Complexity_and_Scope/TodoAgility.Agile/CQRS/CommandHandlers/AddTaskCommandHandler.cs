﻿// Copyright (C) 2020  Road to Agility
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
using TodoAgility.Agile.Domain.Aggregations;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Persistence.Model;
using TodoAgility.Agile.Persistence.Repositories;

namespace TodoAgility.Agile.CQRS.CommandHandlers
{
    public sealed class AddTaskCommandHandler : ICommandHandler<AddTaskCommand>
    {
        private readonly IDbSession<ITaskRepository> _taskSession;
        private readonly IDbSession<IProjectRepository> _projectSession;
        
        public AddTaskCommandHandler(IDbSession<ITaskRepository> taskSession, IDbSession<IProjectRepository> projectSession)
        {
            _taskSession = taskSession;
            _projectSession = projectSession;
        }
        public void Execute(AddTaskCommand command)
        {
            var descr = command.Description;
            var projectId = command.ProjectId;
            var entityId = EntityId.From(1u);
            var project = _projectSession.Repository.Get(projectId);
            
            var agg = TaskAggregationRoot.CreateFrom(descr, entityId, project);
            var task = agg.GetChange();
            
            _taskSession.Repository.Add(task);
            _taskSession.SaveChanges();
        }
    }
}