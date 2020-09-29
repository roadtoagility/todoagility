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
    public class Task : IEquatable<Task>, IExposeValue<TaskState>
    {
        private readonly TaskStatus _status;
        private readonly TaskId _id;
        private readonly Description _description;
        private readonly int _version;

        private Task(TaskStatus status, Description description, TaskId id, int version)
        {
            _status = status;
            _description = description;
            _id = id;
            _version = version;
        }

        public static Task FromDescription(Description description)
        {
            if (description == null )
                throw new ArgumentException("Informe uma descripção válida.", nameof(description));

            return new Task( TaskStatus.From(1), description,TaskId.From(0),-1);
        }
        
        public static Task FromStateAndPatch(TaskState state, Patch patch)
        {
            if (patch == null )
                throw new ArgumentException("Informe os valores a serem atualizados.", nameof(patch));
            
            if (state == null )
                throw new ArgumentException("Informe uma atividade válida.", nameof(state));

            return new Task(TaskStatus.From(state.Status), 
                Description.From(patch.Description),
                TaskId.From(state.Id), state.Version);
        }
        
        public bool Equals(Task other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _description == other._description 
                   && _id == other._id 
                   && _version == other._version;
        }

        TaskState IExposeValue<TaskState>.GetValue()
        {
            IExposeValue<int> stateStatus = _status;
            IExposeValue<string> stateDescr = _description;
            IExposeValue<uint> stateId = _id;
            return new TaskState(stateStatus.GetValue(),stateDescr.GetValue(), stateId.GetValue(), _version);
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
            return $"[TODO]:[Id:{ _id.ToString()}, description: { _description.ToString()}]";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id,_description, _version);
        }

        public class Patch
        {
            public string Description { get;}

            public Patch(string description)
            {
                Description = description;
            }
        }
    }
}