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
using System.Collections.Immutable;
using FluentMediator;
using AppFabric.Business.CommandHandlers.Commands;
using AppFabric.Domain.AggregationUser;
using AppFabric.Domain.BusinessObjects;
using AppFabric.Persistence.Model.Repositories;
using Microsoft.Extensions.Logging;
using DFlow.Business.Cqrs;
using DFlow.Business.Cqrs.CommandHandlers;
using System.Threading;
using DFlow.Domain.Events;
using DFlow.Persistence;
using System.Threading.Tasks;

namespace AppFabric.Business.CommandHandlers
{
    public sealed class AddUserCommandHandler : CommandHandler<AddUserCommand, CommandResult<Guid>>
    {
        private readonly IDbSession<IUserRepository> _dbSession;
        private readonly ILogger<AddUserCommandHandler> _logger;
        
        public AddUserCommandHandler(ILogger<AddUserCommandHandler> logger,IDomainEventBus publisher
            , IDbSession<IUserRepository> dbSession)
            :base(publisher)
        {
            _logger = logger;
            _dbSession = dbSession;
        }
        
        protected override async Task<CommandResult<Guid>> ExecuteCommand(AddUserCommand command, CancellationToken cancellationToken)
        {
            var agg = UserAggregationRoot.CreateFrom(command.Name);
            
            var isSucceed = false;
            var okId = Guid.Empty;
      
            if (agg.IsValid)
            {
                _dbSession.Repository.Add(agg.GetChange());
                
                await _dbSession.SaveChangesAsync();
                
                agg.GetEvents().ToImmutableList()
                    .ForEach( ev => Publisher.Publish(ev));
                
                isSucceed = true;
                okId = agg.GetChange().Identity.Value;
            }
            
            return await Task.FromResult(new CommandResult<Guid>(isSucceed, okId,agg.Failures));
        }
    }
}