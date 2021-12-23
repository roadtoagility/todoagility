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

using DFlow.Domain.BusinessObjects;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AppFabric.Domain.BusinessObjects
{
    public sealed class User : BaseEntity<UserId>
    {
        public User(UserId clientId, Name name, VersionId version)
            :base(clientId, version)
        {
            Name = name;

            AppendValidationResult(name.ValidationStatus.Errors.ToImmutableList());
        }
        
        public Name Name { get; }
                
        public static User From(UserId clientId, Name name, VersionId version)
        {
            return new User(clientId,name,version);
        }

        public static User Empty()
        {
            return From(UserId.Empty(), Name.Empty(), VersionId.Empty());
        }
        
        public override string ToString()
        {
            return $"[USER]:[ID: {Identity} Name: {Name}]";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Identity;
            yield return Name;
        }
    }
}