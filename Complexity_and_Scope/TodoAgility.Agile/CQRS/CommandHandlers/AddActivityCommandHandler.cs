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

using TodoAgility.Agile.CQRS.CommandHandlers.Framework;
using TodoAgility.Agile.Domain.Aggregations;
using TodoAgility.Agile.Domain.DomainEvents.Framework;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Persistence.Framework;
using TodoAgility.Agile.Persistence.Repositories;

namespace TodoAgility.Agile.CQRS.CommandHandlers
{
    public sealed class AddActivityCommandHandler : CommandHandler<AddActivityCommand, ExecutionResult>
    {
        private readonly IDbSession<IProjectRepository> _projectSession;
        private readonly IDbSession<IActivityRepository> _taskSession;

        public AddActivityCommandHandler(IEventDispatcher publisher, IDbSession<IActivityRepository> taskSession,
            IDbSession<IProjectRepository> projectSession):base(publisher)
        {
            _taskSession = taskSession;
            _projectSession = projectSession;
        }

        protected override ExecutionResult ExecuteCommand(AddActivityCommand command)
        {
            var descr = command.Description;
            var projectId = command.ProjectId;
            var entityId = EntityId.From(1u);
            var project = _projectSession.Repository.Get(projectId);

            var agg = ActivityAggregationRoot.CreateFrom(descr, entityId, project);
            var task = agg.GetChange();

            _taskSession.Repository.Add(task);
            _taskSession.SaveChanges();
            
            return new ExecutionResult(true);
        }
    }
}