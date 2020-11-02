using System;
using TodoAgility.Agile.Domain.AggregationActivity;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.DomainEvents;

namespace TodoAgility.Agile.Domain.DomainEvents
{
    public class ActivityAddedEvent : DomainEvent
    {
        private ActivityAddedEvent(EntityId id, Description description, Project project, ActivityStatus status)
            : base(DateTime.Now)
        {
            Description = description;
            Id = id;
            Project = project;
            Status = status;
        }

        public Description Description { get; }
        public EntityId Id { get; }
        public Project Project { get; }
        public ActivityStatus Status { get; }

        public static ActivityAddedEvent For(Activity activity)
        {
            return new ActivityAddedEvent(activity.Id,activity.Description, activity.Project, activity.Status);
        }
    }
}