using System;
using System.Collections.Generic;
using System.Text;
using TodoAgility.Agile.Persistence.Framework.Model;

namespace TodoAgility.Agile.Persistence.Model
{
    public class CounterRegistry : PersistentState
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        public int Order { get; set; }

        public CounterRegistry()
            : base(DateTime.Now)
        {
            
        }
    }
}
