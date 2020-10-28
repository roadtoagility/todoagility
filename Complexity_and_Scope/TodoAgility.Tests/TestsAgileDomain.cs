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
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Persistence.Model;
using Xunit;

namespace TodoAgility.Tests
{
    public class TestsTodoDomain
    {
        
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
            string givenName = "Given Description";
            Description todoName = Description.From(givenName);
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
        
        #region Activity Business Object tests
        [Fact]
        public void Check_Task_Invalid_Description()
        {
            Assert.Throws<ArgumentException>(() => Activity.From(null, null,null));
        }
        
        [Fact]
        public void Check_Task_valid_instance()
        {
            var name = Description.From("givenName");
            var projectId = EntityId.From(1u);
            var entityId = EntityId.From(1u);
            var project = Project.From(projectId, name);

            var task = Activity.From(name, entityId, projectId);            
            Assert.NotNull(task);
        }
        
        [Fact]
        public void Check_Task_GetValue()
        {
            var givenName = "givenName";
            var name = Description.From(givenName);
            var projectId = EntityId.From(1u);
            var entityId = EntityId.From(1u);
            var project = Project.From(projectId, Description.From(givenName));

            var todo = Activity.From(name, entityId, projectId);
            IExposeValue<ActivityState> state = todo;
            var todoState = state.GetValue();
                
            Assert.Equal(todoState.Description, givenName);
        }
        
        [Fact]
        public void Check_TaskStatus_Invalid_Status()
        {
            Assert.Throws<ArgumentException>(() => ActivityStatus.From(-1));
        }
        
        [Fact]
        public void Check_TaskStatus_valid_Status()
        {
            var statusStarted = ActivityStatus.From(2);
            IExposeValue<int> state = statusStarted;

            Assert.Equal(2,state.GetValue());
        }
        #endregion
        
        #region Activity aggregate

        [Fact]
        public void Check_TaskAggregation_Create()
        {
            //given
            var descriptionText = "Given Description";
            var id = EntityId.From(1u);
            var projectId = EntityId.From(1u);
            
            var project = Project.From(projectId, Description.From(descriptionText));
            var task = Activity.From(Description.From(descriptionText), id, projectId);
            
            //when
            var agg = ActivityAggregationRoot.CreateFrom(Description.From(descriptionText), id, project);
            var changes = agg.GetChange();
            
            //then
            Assert.Equal(changes, task);
        }
        
        [Fact]
        public void Check_TaskAggregation_UpdateTask()
        {
            //given
            var descriptionText = "Given Description";
            var descriptionNewText = "Given Description New One";
            var id = EntityId.From(1u);
            var projectId = EntityId.From(1u);

            var oldState = Activity.From(Description.From(descriptionText), id, projectId);            
            //when
            var agg = ActivityAggregationRoot.ReconstructFrom(oldState);
            agg.UpdateTask(Activity.Patch.FromDescription(Description.From(descriptionNewText)));
            var changes = agg.GetChange();
            
            //then
            Assert.NotEqual(changes, oldState);
        }
   
        [Fact]
        public void Check_TaskAggregation_UpdateTaskStatus()
        {
            //given
            var descriptionText = "Given Description";
            var id = EntityId.From(1u);
            var newStatus = 2;
            var projectId = EntityId.From(1u);
            var oldState = Activity.From(Description.From(descriptionText),id, projectId);
            
            //when
            var agg = ActivityAggregationRoot.ReconstructFrom(oldState);
            agg.ChangeTaskStatus(ActivityStatus.From(newStatus));
            var changes = agg.GetChange();
            
            //then
            Assert.NotEqual(changes, oldState);
        }

           
        [Fact]
        public void Check_TaskAggregation_UpdateTaskStatus_invalid()
        {
            //given
            var descriptionText = "Given Description";
            var id = EntityId.From(1u);
            var projectId = EntityId.From(1u);
            var newStatus = 6;

            var oldState = Activity.From(Description.From(descriptionText),id, projectId);
            
            //when
            var agg = ActivityAggregationRoot.ReconstructFrom(oldState);
            
            //then
            Assert.Throws<ArgumentException>(() =>
            {
                agg.ChangeTaskStatus(ActivityStatus.From(newStatus));
            });
        }

        #endregion
        
    }
}