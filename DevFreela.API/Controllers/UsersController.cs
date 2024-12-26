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
        public IActionResult Post()
        {
            return Ok();
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
