using DevFreela.Application.Commands.SkillsCommands.InsertSkill;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.SkillQueries.GetAllSkill;
using DevFreela.Application.Queries.SkillQueries.GetSkillById;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        

        private readonly IMediator _mediator;

        public SkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Get api/skills
        [HttpGet]
        public async Task<IActionResult> GetAll(string search = "")
        {
            var query = new GetAllSkillsQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await  _mediator.Send(new GetSkillByIdQuery(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        

        //Post api/skills
        [HttpPost]
        public async Task<IActionResult> Post(InsertSkillCommand command) 
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        }
    }
}
