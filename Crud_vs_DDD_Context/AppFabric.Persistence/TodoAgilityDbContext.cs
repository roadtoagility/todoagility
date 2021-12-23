// Copyright (C) 2021  Road to Agility
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
using AppFabric.Persistence.Model;
using AppFabric.Persistence.ReadModel;
using DFlow.Persistence.EntityFramework.Model;

namespace AppFabric.Persistence
{
    public class TodoAgilityDbContext : AggregateDbContext
    {
        public TodoAgilityDbContext(DbContextOptions<TodoAgilityDbContext> options)
            : base(options)
        {

        }

        public DbSet<UserState> Users { get; set; }
        public DbSet<UserProjection> UsersProjection { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserState>(
                b =>
                {
                    b.Property(e => e.Id).ValueGeneratedNever().IsRequired();
                    b.Property(e => e.Name).IsRequired();
                    
                    b.Property(p => p.PersistenceId);
                    b.Property(e => e.IsDeleted);
                    b.HasQueryFilter(user => EF.Property<bool>(user, "IsDeleted") == false);
                    b.Property(e => e.CreateAt);
                    b.Property(e => e.RowVersion);
                });
            
            modelBuilder.Entity<ClientState>(
                b =>
                {
                    b.Property(e => e.ProjectId).ValueGeneratedNever();
                    b.Property(e => e.ClientId).IsRequired();
                    b.HasKey(e => e.ClientId);
                    b.Property(p => p.PersistenceId);
                    b.Property(q => q.IsDeleted);
                    b.HasQueryFilter(client => EF.Property<bool>(client, "IsDeleted") == false);
                    b.Property(e => e.CreateAt);
                    b.Property(e => e.RowVersion);
                });

            #region projection
                        
            modelBuilder.Entity<UserProjection>(u =>
            {
                u.Property(pr => pr.Id).ValueGeneratedNever();
                u.HasKey(pr => pr.Id);
                u.Property(pr => pr.Name);
                u.HasQueryFilter(user => EF.Property<bool>(user, "IsDeleted") == false);
            });
            
            #endregion
        }
    }
}