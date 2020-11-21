using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoAgility.Agile.CQRS.QueryHandlers.Counter;


namespace TodoAgility.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndicatorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IndicatorsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("ActivityDailyCounter")]
        public async Task<ActionResult<ActivityDailyCounterFilterResponse>> ActivityDailyCounter()
        {
            var query = ActivityDailyCounterFilter.For();
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet("ProjectFinishedCounter")]
        public async Task<ActionResult<ProjectFinishedCounterResponse>> ProjectFinishedCounter()
        {
            var query = ProjectFinishedCounterFilter.For();
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet("ActivityFinishedCounter")]
        public async Task<ActionResult<ActivityFinishedCounterResponse>> ActivityFinishedCounter()
        {
            var query = ActivityFinishedCounterFilter.For();
            var result = await _mediator.Send(query);
            return result;
        }
    }
}
