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
using System.Collections.Generic;
using TodoAgility.Agile.Domain.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.BusinessObjects;
using TodoAgility.Agile.Domain.Framework.Validation;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Domain.AggregationActivity
{
    public sealed class Activity : ValueObject, IValidationResult, IExposeValue<ActivityState>
    {
        private static readonly int InitialStatus = 1;

        private Activity(ActivityStatus status, Description description, EntityId id,
            EntityId projectId, ValidationResult validationResult)
        {
            Status = status;
            Description = description;
            Id = id;
            ProjectId = projectId;
            ValidationResult = validationResult;
        }

        public EntityId ProjectId { get; }

        public ActivityStatus Status { get; }

        public EntityId Id { get; }

        public Description Description { get; }

        ActivityState IExposeValue<ActivityState>.GetValue()
        {
            IExposeValue<int> stateStatus = Status;
            IExposeValue<string> stateDescr = Description;
            IExposeValue<uint> id = Id;
            IExposeValue<uint> project = ProjectId;
            var stateProject = project.GetValue();
            return new ActivityState(stateStatus.GetValue(), stateDescr.GetValue()
                , id.GetValue(), stateProject);
        }

        public static Activity From(Description description, EntityId entityId, EntityId projectId)
        {
            var validation = new ValidateCondition();
            
            validation.CheckCondition(description == null,nameof(description),"Informe uma descripção válida.");

            if (!description.ValidationResult.IsValid)
            {
                validation.AddViolations(description.ValidationResult.Violations);                
            }

            validation.CheckCondition(projectId == null,nameof(projectId),"Informe um projeto válido.");
            
            validation.CheckCondition(entityId == null,nameof(entityId),"Informe um identificador válido.");
            
            return new Activity(ActivityStatus.From(InitialStatus), description,
                entityId, projectId, validation.GetValidationResult());
        }

        /// <summary>
        ///     used to restore the aggregation
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Activity FromState(ActivityState state)
        {
            var validation = new ValidateCondition();
            
            validation.CheckCondition(state == null,nameof(state),"Informe uma atividade válida.");

            return new Activity(ActivityStatus.From(state.Status),
                Description.From(state.Description), EntityId.From(state.ActivityId),
                EntityId.From(state.ProjectId),validation.GetValidationResult());
        }

        /// <summary>
        ///     used to update the aggregation
        /// </summary>
        /// <param name="current"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Activity CombineWithPatch(Activity current, Patch patch)
        {
            var validation = new ValidateCondition();
            
            validation.CheckCondition(patch == null,nameof(patch),"Informe os valores a serem atualizados.");

            return new Activity(current.Status, patch.Description, current.Id, current.ProjectId,validation.GetValidationResult());
        }

        public static Activity CombineWithStatus(Activity current, ActivityStatus status)
        {
            var validation = new ValidateCondition();
            
            validation.CheckCondition(status == null,nameof(status),"Informe um estado válido para a atividade.");

            var state = ((IExposeValue<ActivityState>) current).GetValue();
            IExposeValue<int> st = status;
            state.Status = st.GetValue();
            return FromState(state);
        }

        public override string ToString()
        {
            return $"[TASK]:[Id:{Id}, description: {Description}: status: {Status}: Project: {ProjectId}]";
        }
        
        public class Patch
        {
            private Patch(Description description)
            {
                Description = description;
            }

            public Description Description { get; }

            public static Patch FromDescription(Description descr)
            {
                return new Patch(descr);
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Description;
            yield return Status;
            yield return ProjectId;
        }

        public ValidationResult ValidationResult { get; }
    }
}