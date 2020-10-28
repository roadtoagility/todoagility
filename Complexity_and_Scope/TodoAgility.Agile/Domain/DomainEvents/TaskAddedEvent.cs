using System;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.DomainEvents.Framework;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;

namespace TodoAgility.Agile.Domain.DomainEvents
{
    public class TaskAddedEvent : DomainEvent
    {
        public Description Description { get; }
        public EntityId Id { get; }
        public EntityId ProjectId { get; }
        public ActivityStatus Status { get; }

        public TaskAddedEvent(EntityId id, Description description, EntityId projectId, ActivityStatus status)
            : base(DateTime.Now)
        {
            Description = description;
            Id = id;
            ProjectId = projectId;
            Status = status;
        }
    }
}