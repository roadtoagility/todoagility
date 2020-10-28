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



// Reference implementation for this version
// https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects
// https://leanpub.com/tdd-ebook/read#leanpub-auto-value-objects
// https://enterprisecraftsmanship.com/posts/value-object-better-implementation/


using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoAgility.Agile.Domain.Framework.BusinessObjects
{
    public abstract class ValueObject<TValueObject> where TValueObject: ValueObject<TValueObject>, IEquatable<TValueObject>
    {
        protected abstract IEnumerable<object> GetValueComponents();
        
        #region Equatable
        
        public bool Equals(TValueObject other)
        {
            if (ReferenceEquals(null, other)){ return false;}
            if (ReferenceEquals(this, other)){ return true;}
            return GetValueComponents().SequenceEqual(other.GetValueComponents());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)){ return false;}
            if (ReferenceEquals(this, obj)){ return true;}
            if (obj.GetType() != this.GetType()){ return false;}

            ValueObject<TValueObject> vo = (ValueObject<TValueObject>) obj; 
            return Equals(vo);
        }

        #endregion
       
        public override string ToString()
        {
            return GetValueComponents().Select(prop => ":[{prop}]")
                .Aggregate((current,next)=> $"{current}:{next}").ToString(); 
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GetValueComponents());
        }
   }
}