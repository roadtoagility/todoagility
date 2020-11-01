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
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Domain.BusinessObjects
{
    public sealed class Activity : IEquatable<Activity>, IExposeValue<ActivityState>
    {
        private static readonly int InitialStatus = 1;

        private Activity(ActivityStatus status, Description description, EntityId id, EntityId projectId)
        {
            Status = status;
            Description = description;
            Id = id;
            ProjectId = projectId;
        }

        public EntityId ProjectId { get; }

        public ActivityStatus Status { get; }

        public EntityId Id { get; }

        public Description Description { get; }

        ActivityState IExposeValue<ActivityState>.GetValue()
        {
            IExposeValue<int> stateStatus = Status;
            IExposeValue<string> stateDescr = Description;
            IExposeValue<uint> id = Id;
            IExposeValue<uint> projectId = ProjectId;
            return new ActivityState(stateStatus.GetValue(), stateDescr.GetValue()
                , id.GetValue(), projectId.GetValue());
        }

        public static Activity From(Description description, EntityId entityId, EntityId projectId)
        {
            if (description == null) throw new ArgumentException("Informe uma descripção válida.", nameof(description));


            if (projectId == null) throw new ArgumentException("Informe um projeto válido.", nameof(projectId));


            if (entityId == null) throw new ArgumentException("Informe um projeto válido.", nameof(entityId));

            return new Activity(ActivityStatus.From(InitialStatus), description, entityId, projectId);
        }

        /// <summary>
        ///     used to restore the aggregation
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Activity FromState(ActivityState state)
        {
            if (state == null) throw new ArgumentException("Informe uma atividade válida.", nameof(state));


            return new Activity(ActivityStatus.From(state.Status),
                Description.From(state.Description), EntityId.From(state.ActivityId),
                EntityId.From(state.ProjectId));
        }

        /// <summary>
        ///     used to update the aggregation
        /// </summary>
        /// <param name="current"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Activity CombineWithPatch(Activity current, Patch patch)
        {
            var state = ((IExposeValue<ActivityState>) current).GetValue();

            var descr = Description.From(state.Description);
            var id = EntityId.From(state.ActivityId);
            var projectId = EntityId.From(state.ProjectId);
            var status = ActivityStatus.From(state.Status);

            if (patch == null) throw new ArgumentException("Informe os valores a serem atualizados.", nameof(patch));


            if (descr == patch.Description)
                throw new ArgumentException("Informe uma descrição diferente da atual.", nameof(patch));

            return new Activity(status, patch.Description, id, projectId);
        }

        public static Activity CombineWithStatus(Activity current, ActivityStatus status)
        {
            if (status == null) throw new ArgumentNullException(nameof(status));

            var state = ((IExposeValue<ActivityState>) current).GetValue();
            IExposeValue<int> st = status;
            state.Status = st.GetValue();
            return FromState(state);
        }

        public override string ToString()
        {
            return $"[TASK]:[Id:{Id}, description: {Description}: status: {Status}: Project: {ProjectId}]";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Description, Status, ProjectId);
        }

        public class Patch
        {
            private Patch(Description description)
            {
                Description = description;
            }

            public Description Description { get; }

            public static Patch FromDescription(Description descr)
            {
                return new Patch(descr);
            }
        }

        #region IEquatable implementation

        public bool Equals(Activity other)
        {
            if (ReferenceEquals(null, other)) return false;

            if (ReferenceEquals(this, other)) return true;

            return Description == other.Description
                   && Id == other.Id && Status == other.Status;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != GetType()) return false;

            return Equals((Activity) obj);
        }

        public static bool operator ==(Activity left, Activity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Activity left, Activity right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}