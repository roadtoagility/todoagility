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
        public async Task<ActionResult<string>> ActivityDailyCounter(DateTime filtro)
        {
            var query = ActivityDailyCounterFilter.For(filtro);
            var result = _mediator.Send(query);
            return await Task.FromResult(CreatedAtAction("Indicador", result));
        }

        [HttpGet("ProjectFinishedCounter")]
        public async Task<ActionResult<int>> ProjectFinishedCounter()
        {
            var query = ProjectFinishedCounterFilter.For();
            var result = _mediator.Send(query);
            return await Task.FromResult(CreatedAtAction("Indicador", result));
        }

        [HttpGet("ActivityFinishedCounter")]
        public async Task<ActionResult<string>> ActivityFinishedCounter()
        {
            var query = ActivityFinishedCounterFilter.For();
            var result = _mediator.Send(query);
            return await Task.FromResult(CreatedAtAction("Indicador", result));
        }
    }
}
