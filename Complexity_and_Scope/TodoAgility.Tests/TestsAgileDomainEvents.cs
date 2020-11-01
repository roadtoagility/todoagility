﻿// Copyright (C) 2020  Road to Agility
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

using Microsoft.EntityFrameworkCore;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.DomainEvents;
using TodoAgility.Agile.Domain.DomainEvents.Framework;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Persistence.Framework;
using TodoAgility.Agile.Persistence.Model;
using TodoAgility.Agile.Persistence.Repositories;
using Xunit;

namespace TodoAgility.Tests
{
    public class TestsAgileDomainEvents
    {
        #region Activity DomainEvents

        [Fact]
        public void Check_DomainEvents_Raise()
        {
            //given
            var activity = Activity.From(Description.From("activity to do"), EntityId.From(1u), 
                EntityId.From(1u));
            var project = Project.From(EntityId.From(1u), Description.From("descriptionText"));
            
            var projectOptionsBuilder = new DbContextOptionsBuilder<ProjectDbContext>();
            projectOptionsBuilder.UseSqlite("Data Source=todoagility_proj_activity_reference.db;");
            var projectDbContext = new ProjectDbContext(projectOptionsBuilder.Options);
            var repProject = new ProjectRepository(projectDbContext);
            using var projectDbSession = new DbSession<IProjectRepository>(projectDbContext, repProject);
            repProject.Add(project);
            projectDbSession.SaveChanges();
            var handlerActivityAdded = new ProjectAggregateActivityAddedHandler(projectDbSession);
            var dispatcher = new DomainEventDispatcher();
            dispatcher.Subscribe(typeof(ActivityAddedEvent).FullName, handlerActivityAdded);

            //when
            dispatcher.Publish(ActivityAddedEvent.For(activity));

            //then
            var proj = repProject.Get(EntityId.From(1u));
            Assert.True(proj.Activities.Count > 0);
        }

        #endregion
    }
}