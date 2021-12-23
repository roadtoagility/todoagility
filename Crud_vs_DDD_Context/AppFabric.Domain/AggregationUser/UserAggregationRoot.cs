// Copyright (C) 2021  Road to Agility
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

using AppFabric.Domain.AggregationUser.Events;
using AppFabric.Domain.BusinessObjects;
using DFlow.Domain.Aggregates;
using DFlow.Domain.BusinessObjects;
using System;

namespace AppFabric.Domain.AggregationUser
{
    public sealed class UserAggregationRoot : ObjectBasedAggregationRoot<User, UserId>
    {

        private UserAggregationRoot(User user)
        {
            if (user.IsValid)
            {
                Apply(user);
                
                if (user.IsNew())
                {
                    Raise(UserAddedEvent.For(user));
                }
            }
            AppendValidationResult(user.Failures);
        }

        #region Aggregation contruction
        
        public static UserAggregationRoot ReconstructFrom(User currentState)
        {
            var nextVersion = currentState.IsValid?
                VersionId.Next(currentState.Version):currentState.Version;
            var user = User.From(currentState.Identity, currentState.Name, nextVersion);
            
            return new UserAggregationRoot(user);

        }
        
        public static UserAggregationRoot CreateFrom(Name name)
        {
            var user = User.From(UserId.From(Guid.NewGuid()), name, VersionId.New());
            return new UserAggregationRoot(user);
        }

        #endregion
    }
}