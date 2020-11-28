using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Persistence.Repositories.CounterRepos
{
    public class CounterProjectionRepository : ICounterProjectionRepository
    {
        private readonly ManagementDbContext _context;
        public CounterProjectionRepository(ManagementDbContext context)
        {
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            _context = context;
        }

        public Counter GetDailyConunter()
        {
            var labels = new string[] { "M", "T", "W", "T", "F", "S", "S" };
            var series = new int[] { 12, 17, 7, 17, 23, 18, 38 };

            //var counters = _context.Set<CounterRegistry>()
            //    .Where(x => x.Type.Equals("DailyConunter"))
            //    .OrderBy(x => x.Order);

            //var labels = counters.Select(x => x.Label).Distinct().ToArray();
            //var series = counters
            //    .GroupBy(x => new
            //    {
            //        Label = x.Label,
            //        Total = 1
            //    })
            //    .Select(x => new
            //    {
            //        Label = x.Key.Label,
            //        Quantity = x.Sum(x => 1)
            //    }).Select(x => x.Quantity).ToArray();

            return new Counter(labels, new int[][] { series });
        }

        public Counter GetFinishedActivitiesCounter()
        {
            var labels = new string[] { "J", "F", "M", "A", "M", "J", "J", "A", "S", "O", "N", "D" };
            var series = new int[] { 542, 443, 320, 780, 553, 453, 326, 434, 568, 610, 756, 895 };

            //var counters = _context.Set<CounterRegistry>()
            //    .Where(x => x.Type.Equals("FinishedActivities"))
            //    .OrderBy(x => x.Order);

            //var labels = counters.Select(x => x.Label).Distinct().ToArray();
            //var series = counters
            //    .GroupBy(x => new
            //    {
            //        Label = x.Label,
            //        Total = 1
            //    })
            //    .Select(x => new
            //    {
            //        Label = x.Key.Label,
            //        Quantity = x.Sum(x => 1)
            //    }).Select(x => x.Quantity).ToArray();

            return new Counter(labels, new int[][] { series });
        }

        public Counter GetFinishedProjectsCounter()
        {
            var labels = new string[] { "J", "F", "M", "A", "M", "J", "J", "A", "S", "O", "N", "D" };
            var series = new int[] {542, 443, 320, 780, 553, 453, 326, 434, 568, 610, 756, 895};
            //return new Counter(labels, series);

            //var counters = _context.Set<CounterRegistry>()
            //   .Where(x => x.Type.Equals("FinishedProjects"))
            //   .OrderBy(x => x.Order);

            //var labels = counters.Select(x => x.Label).Distinct().ToArray();
            //var series = counters
            //    .GroupBy(x => new
            //    {
            //        Label = x.Label,
            //        Total = 1
            //    })
            //    .Select(x => new
            //    {
            //        Label = x.Key.Label,
            //        Quantity = x.Sum(x => 1)
            //    }).Select(x => x.Quantity).ToArray();

            return new Counter(labels, new int[][] { series });
        }
    }
}
