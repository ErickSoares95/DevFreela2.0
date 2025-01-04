using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        public readonly DevFreelaDbContext _context;
        //Padrão options, atráves da propriedade value consegue configurações
        private readonly FreelanceTotalCostConfig _config;
        public ProjectsController(DevFreelaDbContext context)
        {
            _context = context;
        }

        //Get api/projects?search=crm
        [HttpGet]
        public IActionResult Get(string search = "")
        {
            var projects = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDeleted).ToList();

            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

            return Ok(model);
        }

        //Get api/projects/id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Include(p => p.Comments)
                .SingleOrDefault(p => p.Id == id);

            var model = ProjectItemViewModel.FromEntity(project);
            return Ok(model);
        }

        //Post api/projects
        //CreatedAtAction quando vocçe futuramente vai poder consultar em outro endpoint
        //Primeiro é o metodo que poderar se consultado, segundo parametro que no caso é o id e terceiro o modelo
        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        {   
            var project = model.ToEntity();

            _context.Projects.Add(project);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new {id = 1}, model);
        }        

        //PUT api/projects/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null)
            {
                return NotFound();
            }

            project.Update(model.Title, model.Description, model.TotalCost);
            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        //PUT api/projects/id/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null)
            {
                return NotFound();
            }
            project.Start();
            _context.Projects.Update(project);
            _context.SaveChanges();
            return NoContent();
        }

        //PUT api/projects/id/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null)
            {
                return NotFound();
            }

            project.Complete();
            _context.Projects.Update(project);
            _context.SaveChanges();

            return NoContent();
        }

        //DELETE api/projects/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null)
            {
                return NotFound();
            }
            project.SetAsDeleted();
            _context.Projects.Update(project);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateProjectCommentsInputModel model)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project is null)
            {
                return NotFound();
            }
            var comment = new ProjectComment(model.Content, model.IdProject, model.IdUser);
            _context.ProjectComments.Add(comment);
            _context.SaveChanges();
            return Ok(comment);
        }
    }
}
