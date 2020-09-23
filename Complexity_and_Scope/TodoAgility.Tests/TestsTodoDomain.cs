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
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Persistence.Model;
using Xunit;

namespace TodoAgility.Tests
{
    public class TestsTodoDomain
    {
       
        #region Todo like a DTO
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
        public void LIKE_DTO_Check_Todo_valid_Name()
        {
            var name = "givenName";
            var todo = new TodoDTO(name);
            
            Assert.Equal(todo.Name,name);
        }

        #endregion
        
        #region Name Business Object tests
        [Fact]
        public void Check_Name_Invalid_ValueNull()
        {
            Assert.Throws<ArgumentException>(() => Name.From(null));
        }

        [Fact]
        public void Check_Name_Invalid_ValueEmpty()
        {
            Assert.Throws<ArgumentException>(() => Name.From(null));
        }

        [Fact]
        public void Check_Name_Invalid_ValueBlanks()
        {
            var givenName = "        ";
            Assert.Throws<ArgumentException>(() => Name.From(givenName));
        }

        [Fact]
        public void Check_Name_Invalid_SizeLimit()
        {
            var givenName = "Teste excendo o limite do nome para o todo";
            Assert.Throws<ArgumentException>(() => Name.From(givenName));
        }

        [Fact]
        public void Check_Name_Valid_Instance()
        {
            var givenName = "Given Name";
            var todoName = Name.From(givenName);
            Assert.NotNull(todoName);
        }
        
        [Fact]
        public void Check_Name_Value_Exposing()
        {
            var givenName = "Given Name";
            var todoName = Name.From(givenName);
            IExposeValue<string> state = todoName;
            Assert.Equal(givenName, state.GetValue());
        }

        [Fact]
        public void Check_Names_Are_Equal()
        {
            var givenName1 = "Given Name";
            var givenName2 = "Given Name";
            var name1 = Name.From(givenName1);
            var name2 = Name.From(givenName2);
            
            Assert.Equal(name1, name2);
        }
        #endregion
        
        #region Todo Business Object tests
        [Fact]
        public void Check_Todo_Invalid_Name()
        {
            Assert.Throws<ArgumentException>(() => Todo.FromName(null));
        }
        
        [Fact]
        public void Check_Todo_valid_instance()
        {
            var name = Name.From("givenName");
            var todo = Todo.FromName(name);
            
            Assert.NotNull(todo);
        }
        
        [Fact]
        public void Check_Todo_GetValue()
        {
            var givenName = "givenName";
            var name = Name.From(givenName);
            
            var todo = Todo.FromName(name);
            IExposeValue<TodoState> state = todo;
            var todoState = state.GetValue();
                
            Assert.Equal(todoState.Name, givenName);
        }
        #endregion
        
    }
}