using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        public readonly DevFreelaDbContext _context;

        public UsersController(DevFreelaDbContext context)
        {
            _context = context;
        }

        //Get api/users?search=crm
        [HttpGet]
        public IActionResult Get(string search = "")
        {
            var users = _context.Users
                .Include(u => u.Skills)
                .ThenInclude(u => u.Skill)
                .Where(u => !u.IsDeleted).ToList();

            var model = users.Select(UserItemViewModel.FromEntity).ToList();

            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _context.Users
                .Include(p => p.Skills)
                .SingleOrDefault(p => p.Id == id);

            var model = UserItemViewModel.FromEntity(user);
            return Ok(model);
        }
        //POST api/users
        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            var user = model.ToEntity();

            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }

        //PUT
        [HttpPost("{id}/skills")]
        public IActionResult PostSkills(int id, UserSkillsInputModel model)
        {
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
