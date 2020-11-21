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
using Microsoft.EntityFrameworkCore;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Persistence.Framework
{
    public class DbSession<TRepository> : IDbSession<TRepository>, IDisposable
    {
        public DbSession(DbContext context, TRepository repository)
        {
            Context = context;
            Repository = repository;
        }

        private DbContext Context { get; }
        public TRepository Repository { get; }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }

    public class ActivityDbSession<TRepository> : IDbSession<TRepository>, IDisposable
    {
        public ActivityDbSession(ActivityDbContext context, TRepository repository)
        {
            Context = context;
            Repository = repository;
        }

        private DbContext Context { get; }
        public TRepository Repository { get; }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}