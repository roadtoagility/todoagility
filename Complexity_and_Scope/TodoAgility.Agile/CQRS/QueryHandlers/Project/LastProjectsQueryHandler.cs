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
using TodoAgility.Agile.Persistence.Projections.Project;

namespace TodoAgility.Agile.CQRS.QueryHandlers.Project
{
    public sealed class LastProjectsQueryHandler : IRequestHandler<LastProjectsFilter, LastProjectsResponse>
    {
        public LastProjectsQueryHandler()
        {

        }

        public Task<LastProjectsResponse> Handle(LastProjectsFilter request, CancellationToken cancellationToken)
        {
            var projects = new List<LastProjectsProjection>()
            {
                new LastProjectsProjection(){ Id = 1, Name = "FaturamentoWeb", Budget = 36.738m, Client = "Potencial" },
                new LastProjectsProjection(){ Id = 2, Name = "SCA", Budget = 23.789m, Client = "Localiza" },
                new LastProjectsProjection(){ Id = 3, Name = "API Rodovias", Budget = 6.142m, Client = "Fiat" },
                new LastProjectsProjection(){ Id = 4, Name = "CargasWeb", Budget = 38.200m, Client = "ANTT" }
            };

            return Task.FromResult(LastProjectsResponse.From(true, projects));
        }
    }
}
