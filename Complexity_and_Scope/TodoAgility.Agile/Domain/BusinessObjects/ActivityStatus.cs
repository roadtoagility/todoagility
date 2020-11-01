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
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Domain.BusinessObjects
{
    public sealed class ActivityStatus : IEquatable<ActivityStatus>, IComparable<ActivityStatus>, IExposeValue<int>
    {
        public enum Status
        {
            Created,
            Started,
            Completed
        }

        private readonly Status _status;

        private ActivityStatus(Status status)
        {
            _status = status;
        }

        int IExposeValue<int>.GetValue()
        {
            return (int) _status;
        }

        public static ActivityStatus From(int status)
        {
            if (!Enum.IsDefined(typeof(Status), status))
            {
                throw new ArgumentException("O estado informado é inválido.", nameof(status));
            }

            return new ActivityStatus((Status) status);
        }

        public static ActivityStatus FromState(ActivityState state)
        {
            return From(state.Status);
        }


        public override string ToString()
        {
            return $"{_status}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_status);
        }

        #region IEquatable

        public bool Equals(ActivityStatus other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return _status == other._status;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((ActivityStatus) obj);
        }

        public static bool operator ==(ActivityStatus left, ActivityStatus right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ActivityStatus left, ActivityStatus right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region IComparable

        public int CompareTo(ActivityStatus other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            return _status.CompareTo(other._status);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return 1;
            }

            if (ReferenceEquals(this, obj))
            {
                return 0;
            }

            return obj is ActivityStatus other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(ActivityStatus)}");
        }

        public static bool operator <(ActivityStatus left, ActivityStatus right)
        {
            return Comparer<ActivityStatus>.Default.Compare(left, right) < 0;
        }

        public static bool operator >(ActivityStatus left, ActivityStatus right)
        {
            return Comparer<ActivityStatus>.Default.Compare(left, right) > 0;
        }

        public static bool operator <=(ActivityStatus left, ActivityStatus right)
        {
            return Comparer<ActivityStatus>.Default.Compare(left, right) <= 0;
        }

        public static bool operator >=(ActivityStatus left, ActivityStatus right)
        {
            return Comparer<ActivityStatus>.Default.Compare(left, right) >= 0;
        }

        #endregion
    }
}