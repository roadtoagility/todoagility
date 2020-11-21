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
using System.Collections.Generic;
using System.Text;
using TodoAgility.Agile.CQRS.CommandHandlers.Framework;
using TodoAgility.Agile.Persistence.Projections.Counter;

namespace TodoAgility.Agile.CQRS.QueryHandlers.Counter
{
    public class ProjectFinishedCounterResponse
    {
        public string[] Labels { get; private set; }
        public int[][] Series { get; private set; }

        private ProjectFinishedCounterResponse(string[] labels, int[][] series)
        {
            Labels = labels;
            Series = series;
        }

        public static ProjectFinishedCounterResponse From(string[] labels, int[][] series)
        {
            return new ProjectFinishedCounterResponse(labels, series);
        }
    }
}
