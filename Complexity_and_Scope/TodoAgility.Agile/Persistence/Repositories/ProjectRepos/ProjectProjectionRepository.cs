using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoAgility.Agile.Persistence.Model;
using TodoAgility.Agile.Persistence.Projections.Project;

namespace TodoAgility.Agile.Persistence.Repositories.ProjectRepos
{
    public class ProjectProjectionRepository : IProjectProjectionRepository
    {
        private readonly ManagementDbContext _context;
        public ProjectProjectionRepository(ManagementDbContext context)
        {
            _context = context;
            //_context.Database.EnsureDeleted();
            ////_context.Database.Migrate();
            //_context.Database.EnsureCreated();
        }

        public List<FeaturedProjectsProjection> GetFeaturedProjects()
        {
            var featuredProjects = new List<FeaturedProjectsProjection>()
            {
                new FeaturedProjectsProjection(){ Id = 1, Name = "SCA", Icon = "bug_report"},
                new FeaturedProjectsProjection(){ Id = 2, Name = "FaturamentoWeb", Icon = "code"},
                new FeaturedProjectsProjection(){ Id = 3, Name = "API Rodovias", Icon = "cloud"}
            };

            //var featuredProjects = _context.Set<FeaturedProjectsProjection>().ToList();
            return featuredProjects;
        }

        public List<LastProjectsProjection> GetLastProjects()
        {
            var projects = new List<LastProjectsProjection>()
            {
                new LastProjectsProjection(){ Id = 1, Name = "FaturamentoWeb", Budget = 36.738m, Client = "Potencial" },
                new LastProjectsProjection(){ Id = 2, Name = "SCA", Budget = 23.789m, Client = "Localiza" },
                new LastProjectsProjection(){ Id = 3, Name = "API Rodovias", Budget = 6.142m, Client = "Fiat" },
                new LastProjectsProjection(){ Id = 4, Name = "CargasWeb", Budget = 38.200m, Client = "ANTT" }
            };
            //var projects = _context.Set<LastProjectsProjection>().ToList();
            return projects;
        }
    }
}
