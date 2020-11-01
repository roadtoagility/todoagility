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

namespace TodoAgility.Agile.Domain.Framework.BusinessObjects
{
    public sealed class EntityId : IEquatable<EntityId>, IComparable<EntityId>, IExposeValue<uint>
    {
        private readonly uint _id;

        private EntityId(uint id)
        {
            _id = id;
        }

        uint IExposeValue<uint>.GetValue()
        {
            return _id;
        }

        public static EntityId From(uint id)
        {
            return new EntityId(id);
        }


        public override string ToString()
        {
            return $"{_id}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id);
        }

        #region IEquatable

        public bool Equals(EntityId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _id == other._id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((EntityId) obj);
        }

        public static bool operator ==(EntityId left, EntityId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EntityId left, EntityId right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region IComparable

        public int CompareTo(EntityId other)
        {
            if (ReferenceEquals(this, other)) return 0;

            if (ReferenceEquals(null, other)) return 1;

            return _id.CompareTo(other._id);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;

            if (ReferenceEquals(this, obj)) return 0;

            return obj is EntityId other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(EntityId)}");
        }

        public static bool operator <(EntityId left, EntityId right)
        {
            return Comparer<EntityId>.Default.Compare(left, right) < 0;
        }

        public static bool operator >(EntityId left, EntityId right)
        {
            return Comparer<EntityId>.Default.Compare(left, right) > 0;
        }

        public static bool operator <=(EntityId left, EntityId right)
        {
            return Comparer<EntityId>.Default.Compare(left, right) <= 0;
        }

        public static bool operator >=(EntityId left, EntityId right)
        {
            return Comparer<EntityId>.Default.Compare(left, right) >= 0;
        }

        #endregion
    }
}