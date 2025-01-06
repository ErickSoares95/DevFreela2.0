using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        public readonly DevFreelaDbContext _context;

        public SkillsController(DevFreelaDbContext context) 
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var skill = _context.Skills
                .SingleOrDefault(p => p.Id == id);
            var model = SkillItemViewModel.FromEntity(skill);

            return Ok(model);
        }

        //Get api/skills
        [HttpGet]
        public IActionResult GetAll() 
        {
            var skills = _context.Skills.ToList();
            var model = skills.Select(SkillItemViewModel.FromEntity).ToList();
            return Ok(skills);
        }

        //Post api/skills
        [HttpPost]
        public IActionResult Post(CreateSkillInputModel model) 
        {
            var skill = model.ToEntity();
            _context.Add(skill);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }
    }
}
