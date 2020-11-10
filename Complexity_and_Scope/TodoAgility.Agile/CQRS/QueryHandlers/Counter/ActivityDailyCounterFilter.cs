﻿// Copyright (C) 2020  Road to Agility
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

namespace TodoAgility.Agile.CQRS.QueryHandlers.Counter
{
    public class ActivityDailyCounterFilter : IRequest<ActivityDailyCounterFilterResponse>
    {
        private ActivityDailyCounterFilter(DateTime dataFiltro)
        {
            DataFiltro = dataFiltro;
        }

        public DateTime DataFiltro { get; }

        public static ActivityDailyCounterFilter For(DateTime dataFiltro)
        {
            return new ActivityDailyCounterFilter(dataFiltro);
        }
    }
}
