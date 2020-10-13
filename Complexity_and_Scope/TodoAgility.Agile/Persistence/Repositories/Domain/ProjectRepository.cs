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
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Persistence.Repositories.Domain
{
    public class  ProjectRepository: IRepository<ProjectState, Project>, IDisposable
    {
        private readonly ProjectDbContext _context;
        
        public ProjectRepository(DbContextOptions<ProjectDbContext> contextOptions)
        {
            _context = new ProjectDbContext(contextOptions);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
        
        public void Save(IExposeValue<ProjectState> state)
        {
            ProjectState project = state.GetValue();
            _context.Projects.Add(project);
        }

        public Project FindBy(EntityId id)
        {
            IExposeValue<uint> entityId = id;
            
            var project = _context.Projects.AsQueryable().First(t => t.Id == entityId.GetValue());
            
            return Project.FromState(project);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
        
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}