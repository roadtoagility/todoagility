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
using TodoAgility.Agile.Persistence.Repositories;
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

            var project = Project.From(Description.From(descriptionText), EntityId.From(projectId));
            var task = Task.From(Description.From(descriptionText),EntityId.From(id), project);

            //when
            var taskOptionsBuilder = new DbContextOptionsBuilder<TaskDbContext>();
            taskOptionsBuilder.UseSqlite("Data Source=todoagility_repo_test.db;");
            var taskDbContext = new TaskDbContext(taskOptionsBuilder.Options);
            var repTask = new TaskRepository(taskDbContext);
            
            using var taskDbSession = new DbSession<ITaskRepository>(taskDbContext,repTask);
            taskDbSession.Repository.Add(task);
            taskDbSession.SaveChanges();

            //then
            var taskSaved = taskDbSession.Repository.Get(EntityId.From(id));
            Assert.Equal(taskSaved, task);
        }
        
        [Fact]
        public void Check_TaskRespository_Update()
        {
            //given
            var descriptionText = "Given Description";
            var descriptionTextChanged = "Given Description Modificada";
            var projectId = 1u;
            var id = 1u;

            var project = Project.From(Description.From(descriptionText), EntityId.From(projectId));
            var task = Task.From(Description.From(descriptionText),EntityId.From(id), project);

            //when
            var taskOptionsBuilder = new DbContextOptionsBuilder<TaskDbContext>();
            taskOptionsBuilder.UseSqlite("Data Source=todoagility_repo_update_test.db;");
            var taskDbContext = new TaskDbContext(taskOptionsBuilder.Options);
            var repTask = new TaskRepository(taskDbContext);
            
            using var taskDbSession = new DbSession<ITaskRepository>(taskDbContext,repTask);
            taskDbSession.Repository.Add(task);
            taskDbSession.SaveChanges();

            //then
            var taskSaved = taskDbSession.Repository.Get(EntityId.From(id));
            var updatetask = Task.CombineWithPatch(taskSaved, 
                Task.Patch.FromDescription(Description.From(descriptionTextChanged)));
            using var taskDbSession2 = new DbSession<ITaskRepository>(taskDbContext,repTask);
            taskDbSession2.Repository.Add(updatetask);
            taskDbSession2.SaveChanges();
            
            var taskUpdated = taskDbSession2.Repository.Get(EntityId.From(id));
            Assert.NotEqual(taskUpdated, task);
        }
        
        [Fact]
        public void Check_TaskRespository_Concurrency()
        {
            //given
            var descriptionText = "Given Description";
            var projectId = 1u;
            var id = 1u;

            var project = Project.From(Description.From(descriptionText), EntityId.From(projectId));
            var task = Task.From(Description.From(descriptionText),EntityId.From(id), project);

            //when
            var taskOptionsBuilder = new DbContextOptionsBuilder<TaskDbContext>();
            taskOptionsBuilder.UseSqlite("Data Source=todoagility_repo_concurrency_test.db;");
            var taskDbContext = new TaskDbContext(taskOptionsBuilder.Options);
            var repTask = new TaskRepository(taskDbContext);
            
            using var taskDbSession = new DbSession<ITaskRepository>(taskDbContext,repTask);
            taskDbSession.Repository.Add(task);
            taskDbSession.SaveChanges();

            
            //then
            Assert.Throws<DbUpdateException>(() =>
            {
                using var taskDbSession1 = new DbSession<ITaskRepository>(taskDbContext,repTask);
                taskDbSession1.Repository.Add(task);
                taskDbSession1.SaveChanges();         
            });
        }

        #endregion
    }
}