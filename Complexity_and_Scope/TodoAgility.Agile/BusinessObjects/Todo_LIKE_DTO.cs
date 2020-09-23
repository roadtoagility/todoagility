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

namespace TodoAgility.Agile.BusinessObjects
{
    public class TodoDTO: IEquatable<TodoDTO>
    {
        private static readonly int NAME_LENGTH_LIMIT = 20;
        public string Name { get; }

        public TodoDTO(string name)
        {
            Name = name;
        }

        public bool Equals(TodoDTO other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TodoDTO) obj);
        }

        public static bool operator ==(TodoDTO left, TodoDTO right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TodoDTO left, TodoDTO right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return $"[TODO]:[{ Name.ToString()}]";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}