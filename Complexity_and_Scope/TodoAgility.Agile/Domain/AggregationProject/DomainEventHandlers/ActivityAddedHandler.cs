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
using TodoAgility.Agile.Domain.DomainEvents;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.DomainEvents;
using TodoAgility.Agile.Persistence.Framework;
using TodoAgility.Agile.Persistence.Repositories;

namespace TodoAgility.Agile.Domain.AggregationProject.DomainEventHandlers
{
    public class ActivityAddedHandler : DomainEventHandler
    {
        private readonly IDbSession<IProjectRepository> _projectSession;

        public ActivityAddedHandler(IDbSession<IProjectRepository> projectSession)
        {
            _projectSession = projectSession;
            HandlerId = nameof(ActivityAddedHandler);
        }

        protected override void ExecuteHandle(IDomainEvent @event)
        {
            var ev = @event as ActivityAddedEvent;
            var project = _projectSession.Repository.Get(ev?.Project.Id);

            var activity = new List<EntityId> {ev?.Id};
            var projectWithTasks = Project.CombineProjectAndActivities(project, activity);
            
            _projectSession.Repository.Add(projectWithTasks);
            _projectSession.SaveChanges();
        }
    }
}