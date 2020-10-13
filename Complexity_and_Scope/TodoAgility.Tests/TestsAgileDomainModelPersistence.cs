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
using TodoAgility.Agile.Domain.Aggregations;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Persistence.Model;
using TodoAgility.Agile.Persistence.Repositories.Domain;
using Xunit;

namespace TodoAgility.Tests
{
    public class TestsAgileDomainModelPersistence
    {
        #region Task Persistence

        [Fact]
        public void Check_TaskRespository_Create()
        {
            //given
            var descriptionText = "Given Description";
            var projectId = 1u;
            var id = 1u;
            var status = 1;

            var project = Project.From(Description.From(descriptionText), EntityId.From(projectId));
            var task = Task.From(Description.From(descriptionText),EntityId.From(id), project);
            
            var optionsBuilder = new DbContextOptionsBuilder<TaskDbContext>();
            optionsBuilder.UseSqlite("Data Source=todoagility_test.db;");
            
            //when
            using var rep = new TaskRepository(optionsBuilder.Options);
            rep.Save(task);
            rep.Commit();

            //then
            var taskSaved = rep.FindBy(EntityId.From(id));
            Assert.Equal(taskSaved, task);
        }

        #endregion
    }
}