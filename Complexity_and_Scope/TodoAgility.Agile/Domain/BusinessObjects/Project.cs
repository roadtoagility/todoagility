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
    public sealed class Project : IEquatable<Project>, IExposeValue<ProjectState>
    {
        public EntityId Id { get; }
        public Description Description { get; }
        
        private readonly int  _rowVersion;

        private Project(Description description, EntityId id, int  rowVersion)
        {
            Description = description;
            Id = id;
            _rowVersion = rowVersion;
        }

        public static Project From(Description description, EntityId entityId)
        {
            if (description == null)
            {
                throw new ArgumentException("Informe uma descripção válida.", nameof(description));
            }


            if (entityId == null)
            {
                throw new ArgumentException("Informe um projeto válido.", nameof(entityId));
            }


            return new Project(description, entityId, 0);
        }

        //     
        /// <summary>
        /// used to restore the aggregation
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Project FromState(ProjectState state)
        {
            if (state == null)
            {
                throw new ArgumentException("Informe um projeto válido.", nameof(state));
            }

            return new Project(Description.From(state.Description), EntityId.From(state.Id),state.RowVersion);
        }

        ProjectState IExposeValue<ProjectState>.GetValue()
        {
            IExposeValue<string> stateDescr = Description;
            IExposeValue<uint> id = Id;
            return new ProjectState(stateDescr.GetValue(), 
                id.GetValue(), Guid.NewGuid(),_rowVersion);
        }

        #region IEquatable implementation

        public bool Equals(Project other)
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
                   && Id == other.Id;
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

            return Equals((Project) obj);
        }

        public static bool operator ==(Project left, Project right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Project left, Project right)
        {
            return !Equals(left, right);
        }

        #endregion

        public override string ToString()
        {
            return $"[PROJECT]:[Id:{Id.ToString()}, description: {Description.ToString()}]";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Description);
        }
    }
}