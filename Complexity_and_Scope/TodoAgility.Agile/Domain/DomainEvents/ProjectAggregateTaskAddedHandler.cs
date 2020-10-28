using System;
using System.Collections.Generic;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.DomainEvents.Framework;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Persistence;
using TodoAgility.Agile.Persistence.Framework;
using TodoAgility.Agile.Persistence.Repositories;

namespace TodoAgility.Agile.Domain.DomainEvents
{
    public class ProjectAggregateTaskAddedHandler : DomainEventHandler
    {
        private IDbSession<IProjectRepository> _projectSession;
        public ProjectAggregateTaskAddedHandler(IDbSession<IProjectRepository> projectSession)
        {
            _projectSession = projectSession;
        }
        
        protected override void ExecuteHandle(IDomainEvent @event)
        {
            var ev = @event as TaskAddedEvent;
            var project = _projectSession.Repository.Get(ev.ProjectId);
            var tasks = new List<EntityId>(){ev.Id};
            var updateProject = Project.CombineProjectAndActivities(project, tasks);
            _projectSession.Repository.Add(updateProject);
        }
    }
}