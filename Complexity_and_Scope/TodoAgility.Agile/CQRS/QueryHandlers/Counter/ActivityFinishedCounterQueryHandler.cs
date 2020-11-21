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

namespace TodoAgility.Agile.CQRS.QueryHandlers.Counter
{
    public class ActivityFinishedCounterQueryHandler : IRequestHandler<ActivityFinishedCounterFilter, ActivityFinishedCounterResponse>
    {
        public ActivityFinishedCounterQueryHandler()
        {

        }


        public Task<ActivityFinishedCounterResponse> Handle(ActivityFinishedCounterFilter request, CancellationToken cancellationToken)
        {
            var labels = new string[] { "J", "F", "M", "A", "M", "J", "J", "A", "S", "O", "N", "D" };
            var series = new int[][] { new int[] { 542, 443, 320, 780, 553, 453, 326, 434, 568, 610, 756, 895 } };

            return Task.FromResult(ActivityFinishedCounterResponse.From(labels, series));
        }
    }
}
