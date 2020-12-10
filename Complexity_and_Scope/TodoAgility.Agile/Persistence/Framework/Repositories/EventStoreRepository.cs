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
using System.Collections.Immutable;
using System.Data;
using System.Xml.Schema;
using TodoAgility.Agile.Domain.Framework.DomainEvents;
using TodoAgility.Agile.Persistence.Framework.Contexts;
using TodoAgility.Agile.Persistence.Framework.EventStore;

namespace TodoAgility.Agile.Persistence.Framework.Repositories
{
    public class EventStoreRepository : IEventStoreRepository<AggregateState>
    {
        private readonly EventStoreDbContext _context;
        
        public EventStoreRepository(EventStoreDbContext context)
        {
            _context = context;
        }
        public void Add(AggregateState aggregate)
        {
            var current = _context.Aggregate.FindOne(p => p.Id == aggregate.Id);
            
            if ((current == null) && (aggregate.Version == 0))
            {
                _context.Aggregate.Insert(aggregate);
            }
            else if (current.Version == aggregate.Version)
            {
                var events = new List<IDomainEvent>();
                events.AddRange(current.Events);
                events.AddRange(aggregate.Events);
                var agg = new AggregateState(aggregate.AggregateType, aggregate.Version, events.ToImmutableList());
                _context.Aggregate.Insert(agg);
            }
            else
            {
                throw new ConstraintException($"A sua versão não está correta {aggregate.Version}");
            }
        }

        public AggregateState Load(Guid id, uint version)
        {
            return _context.Aggregate.FindOne(p=> p.Id == id && p.Version == version);
        }
        
        // public  IReadOnlyList<AggregateState> Load(Guid id)
        // {
        //     return _context.Aggregate.Find(p=> p.Id == id).ToImmutableList();
        // }
    }
}