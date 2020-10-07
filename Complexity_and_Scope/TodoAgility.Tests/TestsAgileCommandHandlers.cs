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
using TodoAgility.Agile.CQRS.CommandHandlers;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Persistence.Model;
using TodoAgility.Agile.Persistence.Repositories.Domain;
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
            var command = new AddTaskCommand(description, projectId);
            var rep = new TaskRepository();
            var handler = new AddTaskCommandHandler(rep);
            handler.Execute(command);
        }

        [Fact]
        public void Task_UpdateCommandHandler_Succeed()
        {
            var description = "Given Description";
            var id = 1u;
            var projectId = 1u;
            var rep = new TaskRepository();
            var originalTask = Task.From(Description.From(description), EntityId.From(id), EntityId.From(projectId));
            rep.Save(originalTask);
            
            var descriptionNew = "Given Description Changed";
            var command = new UpdateTaskCommand(id, descriptionNew);
            
            var handler = new UpdateTaskCommandHandler(rep);
            handler.Execute(command);

            var task = rep.FindBy(EntityId.From(id));
            
            Assert.NotEqual(task,originalTask);
        }
        
        [Fact]
        public void Task_ChangeStatusCommandHandler_Succeed()
        {
            var description = "Given Description";
            var id = 1u;
            var status = 3;
            var projectId = 1u;
            var rep = new TaskRepository();
            var originalTask = Task.From(Description.From(description), EntityId.From(id), EntityId.From(projectId));
            rep.Save(originalTask);
            
            var command = new ChangeTaskStatusCommand(id, status);
            
            var handler = new ChangeTaskStatusCommandHandler(rep);
            handler.Execute(command);

            var task = rep.FindBy(EntityId.From(id));
            
            Assert.NotEqual(task,originalTask);
        }
        
        #endregion
    }
}