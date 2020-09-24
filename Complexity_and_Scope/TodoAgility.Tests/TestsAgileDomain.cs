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
using TodoAgility.Agile.Domain.Aggregations;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Persistence.Model;
using Xunit;

namespace TodoAgility.Tests
{
    public class TestsTodoDomain
    {
       
        #region Task like a DTO
        // 1 - mínimo controle escopo
        // 2 - uso de tipos primitivos, gera impacto em refatoramentos
        // 3 - proximidade com modelo de persistência pode causar confisão
        // 4 - não possui ferramenta de verificação de estado, muita dependência da
        //     camada de negócio ou de validação por decoração, ou via construtor, etc.
        
        [Fact]
        public void LIKE_DTO_Check_Todo_Instance()
        {
            var name = "givenName";
            var todo = new TodoDTO(name);
            
            Assert.NotNull(todo);
        }
        
        [Fact]
        public void LIKE_DTO_Check_Todo_Invalid()
        {
            var name = "";
            var todo = new TodoDTO(name);
            
            Assert.Empty(todo.Name);
        }
        
        [Fact]
        public void LIKE_DTO_Check_Todo_valid_Name()
        {
            var name = "givenName";
            var todo = new TodoDTO(name);
            
            Assert.Equal(todo.Name,name);
        }

        #endregion
        
        #region Description Business Object tests
        [Fact]
        public void Check_Description_Invalid_ValueNull()
        {
            Assert.Throws<ArgumentException>(() => Description.From(null));
        }

        [Fact]
        public void Check_Description_Invalid_ValueEmpty()
        {
            var description = "";
            Assert.Throws<ArgumentException>(() => Description.From(description));
        }

        [Fact]
        public void Check_Description_Invalid_ValueBlanks()
        {
            var description = "        ";
            Assert.Throws<ArgumentException>(() => Description.From(description));
        }

        [Fact]
        public void Check_Description_Invalid_SizeLimit()
        {
            var description = "Teste excendo o limite do nome para o todo Teste excendo o limite do nome para o todo Teste excendo o limite do nome para o todo";
            Assert.Throws<ArgumentException>(() => Description.From(description));
        }
       
        [Fact]
        public void Check_Description_Value_Exposing()
        {
            var givenName = "Given Description";
            var todoName = Description.From(givenName);
            IExposeValue<string> state = todoName;
            Assert.Equal(givenName, state.GetValue());
        }

        [Fact]
        public void Check_Descriptions_Are_Equal()
        {
            var givenName1 = "Given Description";
            var givenName2 = "Given Description";
            var name1 = Description.From(givenName1);
            var name2 = Description.From(givenName2);
            
            Assert.Equal(name1, name2);
        }
        #endregion
        
        #region Task Business Object tests
        [Fact]
        public void Check_Task_Invalid_Description()
        {
            Assert.Throws<ArgumentException>(() => Task.FromDescription(null));
        }
        
        [Fact]
        public void Check_Task_valid_instance()
        {
            var name = Description.From("givenName");
            var todo = Task.FromDescription(name);
            
            Assert.NotNull(todo);
        }
        
        [Fact]
        public void Check_Task_GetValue()
        {
            var givenName = "givenName";
            var name = Description.From(givenName);
            
            var todo = Task.FromDescription(name);
            IExposeValue<TaskState> state = todo;
            var todoState = state.GetValue();
                
            Assert.Equal(todoState.Description, givenName);
        }
        
        #endregion
        
        #region Task aggregate

        [Fact]
        public void Check_TodoAggregation_Create()
        {
            var agg = new TaskAggregationRoot();
            var descriptionText = "Given Description";
            
            agg.AddTask(descriptionText);
            IExposeValue<TaskState> changes = agg.Changes();
            var state = changes.GetValue();
            
            Assert.Equal(state.Description, descriptionText);
        }
   
        #endregion
        
    }
}