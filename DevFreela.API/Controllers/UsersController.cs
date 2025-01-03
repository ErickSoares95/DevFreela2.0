using DevFreela.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        //POST api/users
        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            return Ok();
        }
        //PUT
        [HttpPost("id}/skills")]
        public IActionResult PostSkills(UserSkillsInputModel model)
        {
            return NoContent();
        }
        //post picture
        [HttpPut]
        public IActionResult PostProfilePicture (IFormFile file)
        {
            var description = $"file {file.FileName}, Size: {file.Length} ";

            //processamento

            return Ok(description);
        }
    }
}
