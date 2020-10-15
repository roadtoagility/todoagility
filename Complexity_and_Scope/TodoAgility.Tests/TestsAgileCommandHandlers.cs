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
using TodoAgility.Agile.Persistence.Model;
using TodoAgility.Agile.Persistence.Repositories;
using Xunit;

namespace TodoAgility.Tests
{
    public class TestsAgileCommandHandlers
    {
       
        #region Task Command Handlers
        
        [Fact]
        public void Task_AddCommandHandler_Succeed()
        {
            var description = "Given Description";
            var projectId = 1u;
            var taskOptionsBuilder = new DbContextOptionsBuilder<TaskDbContext>();
            taskOptionsBuilder.UseSqlite("Data Source=todoagility_test.db;");
            using var repTask = new TaskRepository(taskOptionsBuilder.Options);
            
            var projectOptionsBuilder = new DbContextOptionsBuilder<ProjectDbContext>();
            projectOptionsBuilder.UseSqlite("Data Source=todoagilityProject_test.db;");            
            using var repProject = new ProjectRepository(projectOptionsBuilder.Options);
            
            var command = new AddTaskCommand(description, projectId);

            repProject.Save(Project.From(Description.From(description),EntityId.From(projectId)));
            repProject.Commit();
            var handler = new AddTaskCommandHandler(repTask, repProject);
            handler.Execute(command);
        }

        [Fact]
        public void Task_UpdateCommandHandler_Succeed()
        {
            var description = "Given Description";
            var id = 1u;
            var projectId = 1u;
            var taskOptionsBuilder = new DbContextOptionsBuilder<TaskDbContext>();
            taskOptionsBuilder.UseSqlite("Data Source=todoagility_test.db;");
            using var repTask = new TaskDbSession(taskOptionsBuilder.Options);
            
            var projectOptionsBuilder = new DbContextOptionsBuilder<ProjectDbContext>();
            projectOptionsBuilder.UseSqlite("Data Source=todoagilityProject_test.db;");            
            using var repProject = new ProjectRepository(projectOptionsBuilder.Options);
            var project = Project.From(Description.From(description), EntityId.From(projectId));
            var originalTask = Task.From(Description.From(description), EntityId.From(id), project);
            repProject.Save(project);
            repProject.Commit();
            
            repTask.Tasks.Add(originalTask);
            repTask.SaveChanges();
            
            var descriptionNew = "Given Description Changed";
            var command = new UpdateTaskCommand(id, descriptionNew);
            
            var handler = new UpdateTaskCommandHandler(repTask);
            handler.Execute(command);

            var task = repTask.Tasks.Get(EntityId.From(id));
            
            Assert.NotEqual(task,originalTask);
        }
        
        [Fact]
        public void Task_ChangeStatusCommandHandler_Succeed()
        {
            var description = "Given Description";
            var id = 1u;
            var status = 3;
            var projectId = 1u;
            var optionsBuilder = new DbContextOptionsBuilder<TaskDbContext>();
            optionsBuilder.UseSqlite("Data Source=todoagility_test.db;");
            using var rep = new TaskRepository(optionsBuilder.Options);
            
            var project = Project.From(Description.From(description), EntityId.From(projectId));
            var originalTask = Task.From(Description.From(description), EntityId.From(id), project);
            rep.Save(originalTask);
            rep.Commit();
            
            var command = new ChangeTaskStatusCommand(id, status);
            
            var handler = new ChangeTaskStatusCommandHandler(rep);
            handler.Execute(command);

            var task = rep.FindBy(EntityId.From(id));
            
            Assert.NotEqual(task,originalTask);
        }
        
        #endregion
    }
}