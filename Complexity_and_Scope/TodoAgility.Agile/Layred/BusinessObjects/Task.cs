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

namespace TodoAgility.Agile.Layred
{
    public class Task: IEquatable<Task>
    {
        public string Description { get; }
        public uint Id { get; }
        
        public uint ProjectId { get; }

        public Task(string description, uint id, uint projectId)
        {
            Description = description;
            Id = id;
            ProjectId = projectId;
        }

        public bool Equals(Task other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Description == other.Description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Task) obj);
        }

        public static bool operator ==(Task left, Task right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Task left, Task right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return $"[TODO]:[{ Description}]";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Description);
        }
    }
}