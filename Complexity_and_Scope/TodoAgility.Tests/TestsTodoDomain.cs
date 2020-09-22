using System;
using Xunit;

using TodoAgility.Domain.BusinessObjects;

namespace TodoAgility.Tests
{
    public class TestsTodoDomain
    {
        [Fact]
        public void Check_Invalid_ValueNull()
        {
            Assert.Throws<ArgumentException>(() => TodoName.From(null));
        }

        [Fact]
        public void Check_Invalid_ValueEmpty()
        {
            Assert.Throws<ArgumentException>(() => TodoName.From(null));
        }

        [Fact]
        public void Check_Invalid_ValueBlanks()
        {
            var givenName = "        ";
            Assert.Throws<ArgumentException>(() => TodoName.From(givenName));
        }

        [Fact]
        public void Check_Invalid_SizeLimit()
        {
            var givenName = "Teste excendo o limite do nome para o todo";
            Assert.Throws<ArgumentException>(() => TodoName.From(givenName));
        }

        [Fact]
        public void Check_Valid_Instance()
        {
            var givenName = "Given Name";
            var todoName = TodoName.From(givenName);
            Assert.NotNull(todoName);
        }
        
        [Fact]
        public void Check_Value_Exposing()
        {
            var givenName = "Given Name";
            var todoName = TodoName.From(givenName);
            IExposeValue<string> _value = todoName;
            Assert.Equal(givenName, _value.GetValue());
        }
    }
}