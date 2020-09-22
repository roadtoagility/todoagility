using System;

namespace TodoAgility.Domain.BusinessObjects
{
    public class TodoName : IEquatable<TodoName>, IExposeValue<string>
    {
        private static readonly int NAME_LENGTH_LIMIT = 20;
        private readonly string _nameValue;

        private TodoName(string nameValue)
        {
            _nameValue = nameValue;
        }

        public static TodoName From(string nameValue)
        {
            if (string.IsNullOrEmpty(nameValue) || string.IsNullOrWhiteSpace(nameValue))
                throw new ArgumentException("O nome informado é nulo, vazio ou composto por espaços em branco.",
                    nameof(nameValue));

            if (nameValue.Length > NAME_LENGTH_LIMIT)
                throw new ArgumentException($"O nome excedeu o limite máximo de {NAME_LENGTH_LIMIT} definido.",
                    nameof(nameValue));

            return new TodoName(nameValue);
        }

        public bool Equals(TodoName other)
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
            return Equals((TodoName) obj);
        }

        public static bool operator ==(TodoName left, TodoName right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TodoName left, TodoName right)
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