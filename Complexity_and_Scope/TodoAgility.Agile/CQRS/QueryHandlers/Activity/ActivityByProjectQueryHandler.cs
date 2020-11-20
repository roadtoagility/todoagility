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
using TodoAgility.Agile.Persistence.Framework;
using TodoAgility.Agile.Persistence.Projections.Activity;
using TodoAgility.Agile.Persistence.Repositories;

namespace TodoAgility.Agile.CQRS.QueryHandlers.Activity
{
    public sealed class ActivityByProjectQueryHandler : IRequestHandler<ActivityByProjectFilter, ActivityByProjectResponse>
    {
        public ActivityByProjectQueryHandler(IDbSession<IActivityRepository> taskSession)
        {

        }

        public Task<ActivityByProjectResponse> Handle(ActivityByProjectFilter request, CancellationToken cancellationToken)
        {
            var tasks = new List<ActivityByProjectProjection>()
            {
                new ActivityByProjectProjection(){ Id = 1, ProjectId = 1, Title = "Desenho da interface de inclusão de usuários"},
                new ActivityByProjectProjection(){ Id = 2, ProjectId = 1, Title = "Criação do PDM de modelo do banco"},
                new ActivityByProjectProjection(){ Id = 3, ProjectId = 1, Title = "Integração com Active Directory"},
                new ActivityByProjectProjection(){ Id = 4, ProjectId = 1, Title = "Criar o board para SPRINT 5 com os épicos e estórias envolvidas, alinhar com equipe"}
            };

            return Task.FromResult(ActivityByProjectResponse.From(true, tasks));
        }
    }
}
