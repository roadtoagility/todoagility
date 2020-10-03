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
        private readonly ProjectId _projectId;
        private readonly TaskStatus _status;
        private readonly TaskId _id;
        private readonly Description _description;

        private Task(TaskStatus status, Description description, TaskId id, ProjectId projectId)
        {
            _status = status;
            _description = description;
            _id = id;
            _projectId = projectId;
        }

        public static Task From(Description description, ProjectId projectId)
        {
            if (description == null )
                throw new ArgumentException("Informe uma descripção válida.", nameof(description));
            
            if (projectId == null )
                throw new ArgumentException("Informe um projeto válido.", nameof(projectId));

            return new Task( TaskStatus.From(1), description,TaskId.From(0), projectId);
        }
        
        /// <summary>
        /// used to restore the aggregation
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Task FromState(TaskState state)
        {
            if (state == null )
                throw new ArgumentException("Informe uma atividade válida.", nameof(state));

            return new Task(TaskStatus.From(state.Status), 
                Description.From(state.Description), TaskId.From(state.Id), ProjectId.From(state.ProjectId));
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
            var id = TaskId.From(state.Id);
            var projectId = ProjectId.From(state.ProjectId);
            var status = TaskStatus.From(state.Status);

            if (patch == null )
                throw new ArgumentException("Informe os valores a serem atualizados.", nameof(patch));

            if(descr == patch.Description)
                throw new ArgumentException("Informe uma descrição diferente da atual.", nameof(patch));
            
            return new Task(status, patch.Description, id, projectId);
        }
        
        public bool Equals(Task other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _description == other._description 
                   && _id == other._id;
        }

        TaskState IExposeValue<TaskState>.GetValue()
        {
            IExposeValue<int> stateStatus = _status;
            IExposeValue<string> stateDescr = _description;
            IExposeValue<uint> id = _id;
            IExposeValue<uint> projectId = _projectId;
            return new TaskState(stateStatus.GetValue(),stateDescr.GetValue(), id.GetValue(),projectId.GetValue());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
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

        public override string ToString()
        {
            return $"[TODO]:[Id:{ _id.ToString()}, description: { _description.ToString()}: status: {_status}]";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id,_description,_status);
        }

        public class Patch
        {
            public Description Description { get;}

            private Patch(Description description)
            {
                Description = description;
            }

            public static Patch From(Description descr)
            {
                if (descr == null )
                    throw new ArgumentException("Informe os valores a serem atualizados.", nameof(descr));

                return new Patch(descr);

            }
        }
    }
}