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

using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.BusinessObjects;
using DFlow.Domain.DomainEvents;
using System;

namespace AppFabric.Domain.AggregationUser.Events
{
    public class UserAddedEvent : DomainEvent
    {
        private UserAddedEvent(UserId clientId, Name name, VersionId version)
            : base(DateTime.Now, version)
        {
            Id = clientId;
            Name = name;
        }
        public UserId Id { get; }
        
        public Name Name { get; }
          
        public static UserAddedEvent For(User user)
        {
            return new UserAddedEvent(user.Identity,user.Name, user.Version);
        }
    }
}