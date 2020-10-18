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

using TodoAgility.Agile.Domain.Aggregations;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Persistence.Repositories;

namespace TodoAgility.Agile.CQRS.CommandHandlers
{
    public sealed class UpdateTaskCommandHandler : ICommandHandler<UpdateTaskCommand>
    {
        private readonly IDbSession<ITaskRepository> _session;

        public UpdateTaskCommandHandler(IDbSession<ITaskRepository> session)
        {
            _session = session;
        }
        public void Execute(UpdateTaskCommand command)
        {
            var entityId = command.Id;
            var currentState = _session.Repository.Get(entityId);
            var agg = TaskAggregationRoot.ReconstructFrom(currentState);
            var descr = command.Description;
            
            agg.UpdateTask(Task.Patch.FromDescription(descr));
            var task = agg.GetChange();
            
            _session.Repository.Add(task);
            _session.SaveChanges();
        }
    }
}