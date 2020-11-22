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

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TodoAgility.Agile.CQRS.CommandHandlers.Framework;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Persistence.Framework;
using TodoAgility.Agile.Persistence.Projections.Activity;
using TodoAgility.Agile.Persistence.Repositories;

namespace TodoAgility.Agile.CQRS.QueryHandlers.Activity
{
    public sealed class ActivityByProjectQueryHandler : IRequestHandler<ActivityByProjectFilter, ActivityByProjectResponse>
    {
        private readonly IDbSession<TodoAgility.Agile.Persistence.Repositories.ActivityRepos.IActivityProjectionRepository> _session;

        public ActivityByProjectQueryHandler(IDbSession<TodoAgility.Agile.Persistence.Repositories.ActivityRepos.IActivityProjectionRepository> session)
        {
            _session = session;
        }

        public Task<ActivityByProjectResponse> Handle(ActivityByProjectFilter request, CancellationToken cancellationToken)
        {
            var id = ((IExposeValue<uint>)request.ProjectId).GetValue();
            var tasks = _session.Repository.GetActivities(id);

            return Task.FromResult(ActivityByProjectResponse.From(true, tasks));
        }
    }
}
