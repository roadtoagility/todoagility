using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoAgility.Agile.Persistence.Model;
using TodoAgility.Agile.Persistence.Projections.Activity;

namespace TodoAgility.Agile.Persistence.Repositories.ActivityRepos
{
    public class ActivityProjectionRepository : IActivityProjectionRepository
    {
        private readonly ManagementDbContext _context;
        public ActivityProjectionRepository(ManagementDbContext context)
        {
            _context = context;
            //_context.Database.EnsureCreated();
            //_context.Database.Migrate();
        }

        public List<ActivityByProjectProjection> GetActivities(uint projectId)
        {
            var tasks = new List<ActivityByProjectProjection>()
            {
                new ActivityByProjectProjection(){ Id = 1, ProjectId = 1, Title = "Desenho da interface de inclusão de usuários"},
                new ActivityByProjectProjection(){ Id = 2, ProjectId = 1, Title = "Criação do PDM de modelo do banco"},
                new ActivityByProjectProjection(){ Id = 3, ProjectId = 1, Title = "Integração com Active Directory"},
                new ActivityByProjectProjection(){ Id = 4, ProjectId = 1, Title = "Criar o board para SPRINT 5 com os épicos e estórias envolvidas, alinhar com equipe"}
            };
            //var tasks = _context.ActivitiesByProject.ToList();
            return tasks;
        }
    }
}
