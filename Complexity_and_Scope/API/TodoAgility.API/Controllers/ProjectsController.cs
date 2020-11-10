using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoAgility.Agile.CQRS.QueryHandlers.Project;

namespace TodoAgility.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("FeaturedProjects")]
        public async Task<ActionResult<string>> FeaturedProjects()
        {
            var query = FeaturedProjectsFilter.For();
            var result = await _mediator.Send(query);
            return await Task.FromResult(CreatedAtAction("Projects", result));
        }

        [HttpGet("LastProjects")]
        public async Task<ActionResult<string>> LastProjects()
        {
            var query = LastProjectsFilter.For();
            var result = await _mediator.Send(query);
            return await Task.FromResult(CreatedAtAction("Projects", result));
        }
    }
}
