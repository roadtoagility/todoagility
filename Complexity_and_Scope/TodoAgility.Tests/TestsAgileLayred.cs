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
using TodoAgility.Agile.Layred;
using TodoAgility.Agile.Layred.Services;
using TodoAgility.Agile.Persistence.Model;
using TodoAgility.Agile.Persistence.Repositories;
using Xunit;

namespace TodoAgility.Tests
{
    public class TestsTaskLayred
    {
       
        #region Task like a DTO
        // 1 - mínimo controle escopo
        // 2 - uso de tipos primitivos, gera impacto em refatoramentos
        // 3 - proximidade com modelo de persistência pode causar confisão
        // 4 - não possui ferramenta de verificação de estado, muita dependência da
        //     camada de negócio ou de validação por decoração, ou via construtor, etc.
        
        [Fact]
        public void LIKE_DTO_Check_Task_Instance()
        {
            var descr = "givenName";
            var id = 1u;
            var task = new Task(descr,id);
            
            Assert.NotNull(task);
        }
        
        [Fact]
        public void LIKE_DTO_Check_Task_Invalid()
        {
            var descr = "";
            var id = 1u;
            var task = new Task(descr,id);
            
            Assert.Empty(task.Description);
        }
        
        [Fact]
        public void LIKE_DTO_Check_Task_valid()
        {
            var descr = "givenName";
            var id = 1u;
            var task = new Task(descr,id);
            
            Assert.Equal(task.Description,descr);
        }

        #endregion
        
        #region Task aggregate

        [Fact]
        public void Check_TaskService_Create()
        {
            //given
            var descriptionText = "Given Description";
            var id = 1u;
            //when
            var rep = new TaskRepository();
            var service = new TaskService(rep);
            var task = new Task(descriptionText, id);
            service.AddTask(task);

            var state = rep.FindBy(id);
            
            //then
            Assert.Equal(state.Description, descriptionText);
        }
        
        [Fact]
        public void Check_TaskService_UpdateTask()
        {
            //given
            var descriptionText = "Given Description";
            var descriptionNewText = "Given Description New One";
            var started = 2;
            var id = 1u;
            var oldState = new TaskState(started, descriptionText,id);
            
            //when
            var rep = new TaskRepository();
            rep.Save(oldState);
            var service = new TaskService(rep);
            var task = new Task(descriptionNewText, id);
            service.UpdateTask(id,task);

            var state = rep.FindBy(id);
            
            //then
            Assert.Equal(state.Description, descriptionNewText);
        }
   
        #endregion
    }
}