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


using System.Collections.Generic;

using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Persistence.Repositories.Domain
{
    public class  ProjectRepository: IRepository<ProjectState, Project>
    {
        private readonly IDictionary<EntityId, ProjectState> _items = new Dictionary<EntityId, ProjectState>();
        
        public void Save(IExposeValue<ProjectState> state)
        {
            ProjectState task = state.GetValue();
            var id = EntityId.From(task.Id);
            
            if (_items.ContainsKey(id))
            {
                _items[id] = task;
            }
            else
            {
                _items.Add(id,task);
            }
        }

        public Project FindBy(EntityId id)
        {
            return Project.FromState(_items[id]);
        }

        public void Commit()
        {
            //do not persist anything yet
        }
    }
}