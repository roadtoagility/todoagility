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
using TodoAgility.Agile.Domain.Framework.BusinessObjects;

namespace TodoAgility.Agile.Domain.Framework.Validation
{
    public sealed class Scope :IExposeValue<string>
    {
        private string _name { get; }

        private Scope(string scopeName)
        {
            _name = scopeName;
        }

        public static Scope For(string scopeName)
        {
            
            return new Scope(scopeName);
        }

        string IExposeValue<string>.GetValue()
        {
            return _name;
        }

        public override string ToString()
        {
            return $"{_name}";
        }
    }
}