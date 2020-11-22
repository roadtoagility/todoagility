using System;
using System.Collections.Generic;
using System.Text;
using TodoAgility.Agile.Persistence.Model;

namespace TodoAgility.Agile.Persistence.Repositories.CounterRepos
{
    public interface ICounterProjectionRepository
    {
        Counter GetDailyConunter();
        Counter GetFinishedActivitiesCounter();

        Counter GetFinishedProjectsCounter();
    }
}
