using System;
using System.Collections.Generic;
using System.Text;
using TodoAgility.Agile.Persistence.Projections.Project;

namespace TodoAgility.Agile.Persistence.Repositories.ProjectRepos
{
    public interface IProjectProjectionRepository
    {
        List<FeaturedProjectsProjection> GetFeaturedProjects();
        List<LastProjectsProjection> GetLastProjects();
    }
}
