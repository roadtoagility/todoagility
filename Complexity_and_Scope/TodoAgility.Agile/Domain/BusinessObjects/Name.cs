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

namespace TodoAgility.Agile.Domain.BusinessObjects
{
    public class Name : IEquatable<Name>, IExposeValue<string>
    {
        private static readonly int NAME_LENGTH_LIMIT = 20;
        private readonly string _nameValue;

        private Name(string nameValue)
        {
            _nameValue = nameValue;
        }

        public static Name From(string nameValue)
        {
            if (string.IsNullOrEmpty(nameValue) || string.IsNullOrWhiteSpace(nameValue))
                throw new ArgumentException("O nome informado é nulo, vazio ou composto por espaços em branco.",
                    nameof(nameValue));

            if (nameValue.Length > NAME_LENGTH_LIMIT)
                throw new ArgumentException($"O nome excedeu o limite máximo de {NAME_LENGTH_LIMIT} definido.",
                    nameof(nameValue));

            return new Name(nameValue);
        }

        public bool Equals(Name other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _nameValue == other._nameValue;
        }

        string IExposeValue<string>.GetValue()
        {
            return _nameValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Name) obj);
        }

        public static bool operator ==(Name left, Name right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Name left, Name right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return $"{_nameValue}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_nameValue);
        }
    }
}