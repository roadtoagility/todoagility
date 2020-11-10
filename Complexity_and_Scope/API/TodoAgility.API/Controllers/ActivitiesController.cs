using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoAgility.Agile.CQRS.QueryHandlers.Activity;
using TodoAgility.API.Models;

namespace TodoAgility.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActivitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("ActivitiesByProject")]
        public async Task<ActionResult<string>> ActivitiesByProject([FromQuery] ActivityByProjectDTO dto)
        {
            var query = ActivityByProjectFilter.For(dto.ProjectId);
            var result = await _mediator.Send(query);
            return await Task.FromResult(CreatedAtAction("Indicador", result));
        }
    }
}
