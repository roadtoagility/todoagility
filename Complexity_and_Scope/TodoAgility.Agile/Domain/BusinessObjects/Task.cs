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
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Domain.BusinessObjects
{
    public sealed class Task : IEquatable<Task>, IExposeValue<TaskState>
    {
        private static readonly int INITiAL_VERSION = 0;
        public EntityId ProjectId { get; }

        public TaskStatus Status { get; }

        public EntityId Id { get; }

        public Description Description { get; }

        private readonly int _rowVersion;
        
        private Task(TaskStatus status, Description description, EntityId id, 
            EntityId projectId)
        :this(status,description,id,projectId,INITiAL_VERSION)
        {
        }
        
        private Task(TaskStatus status, Description description, EntityId id, 
            EntityId projectId, int rowVersion)
        {
            Status = status;
            Description = description;
            Id = id;
            ProjectId = projectId;
            _rowVersion = rowVersion;
        }

        public static Task From(Description description, EntityId entityId, Project project)
        {
            if (description == null)
            {
                throw new ArgumentException("Informe uma descripção válida.", nameof(description));
            }


            if (project == null)
            {
                throw new ArgumentException("Informe um projeto válido.", nameof(project));
            }


            if (entityId == null)
            {
                throw new ArgumentException("Informe um projeto válido.", nameof(entityId));
            }

            return new Task( TaskStatus.From(1), description,entityId, project.Id);
        }
        
        /// <summary>
        /// used to restore the aggregation
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Task FromState(TaskState state)
        {
            if (state == null)
            {
                throw new ArgumentException("Informe uma atividade válida.", nameof(state));
            }
                

            return new Task(TaskStatus.From(state.Status), 
                Description.From(state.Description), EntityId.From(state.Id), 
                EntityId.From(state.ProjectId), state.RowVersion);
        }
        
        /// <summary>
        /// used to update the aggregation
        /// </summary>
        /// <param name="current"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Task CombineWithPatch(Task current, Patch patch)
        {
            var state = ((IExposeValue<TaskState>)current).GetValue();
            
            var descr = Description.From(state.Description);
            var id = EntityId.From(state.Id);
            var projectId = EntityId.From(state.ProjectId);
            var status = TaskStatus.From(state.Status);

            if (patch == null)
            {
                throw new ArgumentException("Informe os valores a serem atualizados.", nameof(patch));
            }

            if (descr == patch.Description)
            {
                throw new ArgumentException("Informe uma descrição diferente da atual.", nameof(patch));
            }
            
            return new Task(status, patch.Description, id, projectId, state.RowVersion);
        }

        public static Task CombineWithStatus(Task current, TaskStatus newStatus)
        {
            var state = ((IExposeValue<TaskState>)current).GetValue();
            
            var descr = Description.From(state.Description);
            var id = EntityId.From(state.Id);
            var projectId = EntityId.From(state.ProjectId);
            var status = TaskStatus.From(state.Status);

            if (newStatus == null)
            {
                throw new ArgumentException("Informe o novo estado da atividade.", nameof(newStatus));
            }

            if (status == newStatus)
            {
                throw new ArgumentException("Informe um estado diferente da atual.", nameof(newStatus));
            }
            
            return new Task(newStatus, descr, id, projectId, state.RowVersion);
        }
        
        TaskState IExposeValue<TaskState>.GetValue()
        {
            IExposeValue<int> stateStatus = Status;
            IExposeValue<string> stateDescr = Description;
            IExposeValue<uint> id = Id;
            IExposeValue<uint> projectId = ProjectId;
            return new TaskState(stateStatus.GetValue(),stateDescr.GetValue(), id.GetValue()
                ,projectId.GetValue(), Guid.NewGuid(),  _rowVersion);
        }
        
        #region IEquatable implementation
        
        public bool Equals(Task other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Description == other.Description 
                   && Id == other.Id && Status == other.Status;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((Task) obj);
        }

        public static bool operator ==(Task left, Task right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Task left, Task right)
        {
            return !Equals(left, right);
        }
        #endregion
        
        public override string ToString()
        {
            return $"[TASK]:[Id:{ Id.ToString()}, description: { Description.ToString()}: status: {Status}: Project: {ProjectId}]";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id,Description,Status, ProjectId);
        }

        public class Patch
        {
            public Description Description { get;}

            private Patch(Description description)
            {
                Description = description;
            }

            public static Patch FromDescription(Description descr)
            {
                return new Patch(descr);
            }
        }
    }
}