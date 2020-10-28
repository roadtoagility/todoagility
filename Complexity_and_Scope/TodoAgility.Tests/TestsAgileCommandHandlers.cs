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
using TodoAgility.Agile.CQRS.CommandHandlers;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Persistence;
using TodoAgility.Agile.Persistence.Framework;
using TodoAgility.Agile.Persistence.Model;
using TodoAgility.Agile.Persistence.Repositories;
using Xunit;

namespace TodoAgility.Tests
{
    public class TestsAgileCommandHandlers
    {
       
        #region Activity Command Handlers
        
        [Fact]
        public void Task_AddCommandHandler_Succeed()
        {
            var description = "Given Description";
            var projectId = 1u;

            var taskOptionsBuilder = new DbContextOptionsBuilder<ActivityDbContext>();
            taskOptionsBuilder.UseSqlite("Data Source=todoagility_add_test.db;");
            var taskDbContext = new ActivityDbContext(taskOptionsBuilder.Options);
            var repTask = new ActivityRepository(taskDbContext);
            using var taskDbSession = new DbSession<IActivityRepository>(taskDbContext,repTask);
            
            var projectOptionsBuilder = new DbContextOptionsBuilder<ProjectDbContext>();
            projectOptionsBuilder.UseSqlite("Data Source=todoagilityProject_add_test.db;");
            var projectDbContext = new ProjectDbContext(projectOptionsBuilder.Options);
            var repProject = new ProjectRepository(projectDbContext);
            using var projectDbSession = new DbSession<IProjectRepository>(projectDbContext,repProject);
            
            var command = new AddTaskCommand(description, projectId);

            projectDbSession.Repository.Add(Project.From(EntityId.From(projectId), Description.From(description)));
            projectDbSession.SaveChanges();
            var handler = new AddTaskCommandHandler(taskDbSession, projectDbSession);
            handler.Execute(command);
            
            var task = taskDbSession.Repository.Find(a=> a.ProjectId == projectId);
            
            Assert.NotNull(task);
        }

        [Fact]
        public void Task_UpdateCommandHandler_Succeed()
        {
            var description = "Given Description";
            var id = 1u;
            var projectId = 1u;
            var taskOptionsBuilder = new DbContextOptionsBuilder<ActivityDbContext>();
            taskOptionsBuilder.UseSqlite("Data Source=todoagility_cqrs_test.db;");
            var taskDbContext = new ActivityDbContext(taskOptionsBuilder.Options);
            var repTask = new ActivityRepository(taskDbContext);
            using var taskDbSession = new DbSession<IActivityRepository>(taskDbContext,repTask);
            
            var projectOptionsBuilder = new DbContextOptionsBuilder<ProjectDbContext>();
            projectOptionsBuilder.UseSqlite("Data Source=todoagilityProject_cqrs_test.db;");
            var projectDbContext = new ProjectDbContext(projectOptionsBuilder.Options);
            var repProject = new ProjectRepository(projectDbContext);
            using var projectDbSession = new DbSession<IProjectRepository>(projectDbContext,repProject);
            
            var project = Project.From(EntityId.From(projectId), Description.From(description));
            var originalTask = Activity.From(Description.From(description), EntityId.From(id), project.Id);
            projectDbSession.Repository.Add(project);
            projectDbSession.SaveChanges();
            
            taskDbSession.Repository.Add(originalTask);
            taskDbSession.SaveChanges();
            
            var descriptionNew = "Given Description Changed";
            var command = new UpdateTaskCommand(id, descriptionNew);
            
            var handler = new UpdateTaskCommandHandler(taskDbSession);
            handler.Execute(command);

            var task = taskDbSession.Repository.Get(EntityId.From(id));
            
            Assert.NotEqual(task,originalTask);
        }
        
        [Fact]
        public void Task_ChangeStatusCommandHandler_Succeed()
        {
            var description = "Given Description";
            var id = 1u;
            var status = 2;
            var projectId = 1u;
            var optionsBuilder = new DbContextOptionsBuilder<ActivityDbContext>();
            optionsBuilder.UseSqlite("Data Source=todoagility_cqrs_changed_test.db;");
            var taskDbContext = new ActivityDbContext(optionsBuilder.Options);
            var repTask = new ActivityRepository(taskDbContext);
            using var taskDbSession = new DbSession<IActivityRepository>(taskDbContext,repTask);
            
            var project = Project.From(EntityId.From(projectId), Description.From(description));
            var originalTask = Activity.From(Description.From(description), EntityId.From(id), project.Id);
            taskDbSession.Repository.Add(originalTask);
            taskDbSession.SaveChanges();
            
            var command = new ChangeTaskStatusCommand(id, status);
            
            var handler = new ChangeTaskStatusCommandHandler(taskDbSession);
            handler.Execute(command);

            var task = taskDbSession.Repository.Get(EntityId.From(id));
            
            Assert.NotEqual(task,originalTask);
        }
        
        #endregion
    }
}