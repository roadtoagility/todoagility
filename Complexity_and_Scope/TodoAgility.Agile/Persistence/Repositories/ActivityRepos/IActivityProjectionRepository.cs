using System;
using System.Collections.Generic;
using System.Text;
using TodoAgility.Agile.Persistence.Projections.Activity;

namespace TodoAgility.Agile.Persistence.Repositories.ActivityRepos
{
    public interface IActivityProjectionRepository
    {
        List<ActivityByProjectProjection> GetActivities(uint projectId);
    }
}
