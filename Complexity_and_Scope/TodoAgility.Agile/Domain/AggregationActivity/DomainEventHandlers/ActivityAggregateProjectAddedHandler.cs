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


using TodoAgility.Agile.Domain.AggregationProject.Events;
using TodoAgility.Agile.Domain.Framework.DomainEvents;
using TodoAgility.Agile.Persistence.Framework;
using TodoAgility.Agile.Persistence.Repositories;

namespace TodoAgility.Agile.Domain.AggregationActivity.DomainEventHandlers
{
    public class ActivityAggregateProjectAddedHandler : DomainEventHandler
    {
        private readonly IDbSession<IActivityRepository> _activitySession;

        public ActivityAggregateProjectAddedHandler(IDbSession<IActivityRepository> activitySession)
        {
            _activitySession = activitySession;
            HandlerId = nameof(ActivityAggregateProjectAddedHandler);
        }

        protected override void ExecuteHandle(IDomainEvent @event)
        {
            var ev = @event as ProjectAddedEvent;
            // var activity = _activitySession.Repository.Add(Project.From(ev?.Id,ev?.Description));
            //
            // var activity = new List<EntityId> {ev?.Id};
            // var projectWithTasks = AggregationProject.Project.CombineProjectAndActivities(project, activity);
            //
            // _projectSession.Repository.Add(projectWithTasks);
            // _projectSession.SaveChanges();
        }
    }
}