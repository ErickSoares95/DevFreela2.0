using DevFreela.Application.Models;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DevFreela.Application.Queries.ProjectQueries.GetAllProjects;
using DevFreela.Application.Queries.ProjectQueries.GetProjectDetailsById;
using DevFreela.Application.Commands.ProjectCommands.InsertProject;
using DevFreela.Application.Commands.ProjectCommands.UpdateProject;
using DevFreela.Application.Commands.ProjectCommands.DeleteProject;
using DevFreela.Application.Commands.ProjectCommands.StartProject;
using DevFreela.Application.Commands.ProjectCommands.CompleteProject;
using DevFreela.Application.Commands.ProjectCommands.InsertComment;
using DevFreela.Application.Queries.ProjectQueries.GetProjectDetailsById;
using Microsoft.AspNetCore.Authorization;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Authorize(Roles = "freelancer, client")]
        public async Task<IActionResult> Get(string search = "")
        {
            var query = new GetAllProjectsQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        //Get api/projects/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProjectDetailsByIdQuery(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        //Post api/projects
        //CreatedAtAction quando vocçe futuramente vai poder consultar em outro endpoint
        //Primeiro é o metodo que poderar se consultado, segundo parametro que no caso é o id e terceiro o modelo
        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Post(InsertProjectCommand command)
        { 
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            
            return CreatedAtAction(nameof(GetById), new {id = result.Data}, command);
        }

        //PUT api/projects/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProjectCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProjectCommand(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return NoContent();
        }

        //PUT api/projects/id/start
        [HttpPut("{id}/start")]
        public async Task<IActionResult>  Start(int id)
        {
            var result = await _mediator.Send(new StartProjectCommand(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return NoContent();
        }

        //PUT api/projects/id/complete
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            var result = await _mediator.Send(new CompleteProjectCommand(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return NoContent();
        }

        //DELETE api/projects/id
        

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment(int id, InsertCommentCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
