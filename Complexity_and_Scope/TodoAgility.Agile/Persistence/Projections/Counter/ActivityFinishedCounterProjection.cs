using System;
using System.Collections.Generic;
using System.Text;

namespace TodoAgility.Agile.Persistence.Projections.Counter
{
    public class ActivityFinishedCounterProjection
    {
        public int ProjectId { get; set; }
        public int Qtd { get; set; }
        public DateTime Date { get; set; }
    }
}
