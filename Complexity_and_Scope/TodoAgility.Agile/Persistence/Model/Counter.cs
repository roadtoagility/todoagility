using System;
using System.Collections.Generic;
using System.Text;

namespace TodoAgility.Agile.Persistence.Model
{
    public class Counter
    {
        public string[] Labels { get; private set; }
        public int[][] Series { get; private set; }

        public Counter(string[] labels, int[][] series)
        {
            Labels = labels;
            Series = series;
        }
    }
}
