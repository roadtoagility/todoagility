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
using TodoAgility.Agile.Model;

namespace TodoAgility.Agile.BusinessObjects
{
    public class Todo : IEquatable<Todo>, IExposeValue<TodoState>
    {
        private readonly Name _name;

        private Todo(Name name)
        {
            _name = name;
        }

        public static Todo FromName(Name name)
        {
            if (name == null )
                throw new ArgumentException("Informe um nome válido.", nameof(name));

            return new Todo(name);
        }

        public bool Equals(Todo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _name == other._name;
        }

        TodoState IExposeValue<TodoState>.GetValue()
        {
            IExposeValue<string> nameState = _name;
            return new TodoState(nameState.GetValue());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Todo) obj);
        }

        public static bool operator ==(Todo left, Todo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Todo left, Todo right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return $"[TODO]:[{ _name.ToString()}]";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_name);
        }
    }
}