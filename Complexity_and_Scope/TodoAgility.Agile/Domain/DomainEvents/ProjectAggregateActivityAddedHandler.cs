using System.Collections.Generic;
using LiteDB;
using Microsoft.EntityFrameworkCore;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.DomainEvents.Framework;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Persistence.Framework;
using TodoAgility.Agile.Persistence.Model;
using TodoAgility.Agile.Persistence.Repositories;

namespace TodoAgility.Agile.Domain.DomainEvents
{
    public class ProjectAggregateActivityAddedHandler : DomainEventHandler
    {
        private readonly IDbSession<IProjectRepository> _projectSession;

        public ProjectAggregateActivityAddedHandler(IDbSession<IProjectRepository> projectSession)
        {
            _projectSession = projectSession;
            HandlerId = nameof(ProjectAggregateActivityAddedHandler);
        }

        protected override void ExecuteHandle(IDomainEvent @event)
        {
            var ev = @event as ActivityAddedEvent;
            var project = _projectSession.Repository.Get(ev?.ProjectId);

            var activity = new List<EntityId> {ev.Id};
            var projectWithTasks = Project.CombineProjectAndActivities(project, activity);
            
            _projectSession.Repository.Add(projectWithTasks);
            _projectSession.SaveChanges();
        }
    }
}