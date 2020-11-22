using System;
using System.Collections.Generic;
using System.Text;

namespace TodoAgility.Agile.Persistence.Projections.Project
{
    public class LastProjectsProjection
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public decimal Budget { get; set; }
        public string Client { get; set; }

    }
}
