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
    public sealed class Description : IEquatable<Description>, IExposeValue<string>
    {
        private static readonly int DESCRIPTION_LENGTH_LIMIT = 100;

        private readonly string _description;

        private Description(string description)
        {
            _description = description;
        }

        public bool Equals(Description other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(_description, other._description);
        }

        string IExposeValue<string>.GetValue()
        {
            return _description;
        }

        public static Description From(string description)
        {
            if (string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("A descrição informada é nulo, vazio ou composto por espaços em branco.",
                    nameof(description));
            }



            if (description.Length > DESCRIPTION_LENGTH_LIMIT)
            {
                throw new ArgumentException(
                    $"A descripção excedeu o limite máximo de {DESCRIPTION_LENGTH_LIMIT} definido.",
                    nameof(description));
            }


            return new Description(description);
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

            return Equals((Description) obj);
        }

        public static bool operator ==(Description left, Description right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Description left, Description right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return $"{_description}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_description);
        }
    }
}