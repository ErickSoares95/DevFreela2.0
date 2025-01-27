using Microsoft.AspNetCore.Mvc;
using MediatR;
using DevFreela.Application.Queries.UserQueries.GetAllUsers;
using DevFreela.Application.Queries.UserQueries.GetUserById;
using DevFreela.Application.Commands.UserCommands.InsertUsers;
using DevFreela.Application.Commands.UserCommands.InsertSkills;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Get api/users?search=crm
        [HttpGet]
        public async Task<IActionResult> Get(string search = "")
        {
            var users = new GetAllUsersQuery();

            var result = await _mediator.Send(users);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = new GetUserByidQuery(id);

            var result = _mediator.Send(user);

            return Ok(result);
        }
        //POST api/users
        [HttpPost]
        public async Task<IActionResult> Post(InsertUserCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        }

        //PUT
        [HttpPost("{id}/skills")]
        public async Task<IActionResult> PostSkills(int id, InsertSkillsCommand command)
        {
            var result = _mediator.Send(command);

            if (!result.IsCompleted)
            {
                return BadRequest(result.Result);
            }

            return NoContent();
        }
            
        //post picture
        [HttpPut("{id}/profile-picture")]
        public IActionResult PostProfilePicture (IFormFile file)
        {
            var description = $"file {file.FileName}, Size: {file.Length} ";

            //processamento

            return Ok(description);
        }
    }
}
