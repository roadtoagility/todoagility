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
using System.Collections;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Domain.BusinessObjects
{
    public sealed class Project : IEquatable<Project>, IExposeValue<ProjectState>
    {
        private readonly EntityId _id;
        private readonly Description _description;

        private Project(Description description, EntityId id)
        {
            _description = description;
            _id = id;
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


            return new Project(description, entityId);
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

            return new Project(Description.From(state.Description), EntityId.From(state.Id));
        }

        ProjectState IExposeValue<ProjectState>.GetValue()
        {
            IExposeValue<string> stateDescr = _description;
            IExposeValue<uint> id = _id;
            return new ProjectState(stateDescr.GetValue(), id.GetValue());
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

            return _description == other._description
                   && _id == other._id;
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
            return $"[TODO]:[Id:{_id.ToString()}, description: {_description.ToString()}]";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id, _description);
        }
    }
}