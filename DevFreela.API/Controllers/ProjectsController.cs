using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using DevFreela.Application.Services;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        public readonly IProjectService _service;
        //Padrão options, atráves da propriedade value consegue configurações
        private readonly FreelanceTotalCostConfig _config;
        public ProjectsController(IProjectService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Get(string search = "")
        {
            var result = _service.GetAll();

            return Ok(result);
        }

        //Get api/projects/id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);
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
        public IActionResult Post(CreateProjectInputModel model)
        {
            var result = _service.Insert(model);
            return CreatedAtAction(nameof(GetById), new {id = result.Data}, result);
        }

        //PUT api/projects/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {
            var result = _service.Update(model);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.Delete(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return NoContent();
        }

        //PUT api/projects/id/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var result = _service.Start(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return NoContent();
        }

        //PUT api/projects/id/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var result = _service.Complete(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return NoContent();
        }

        //DELETE api/projects/id
        

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateProjectCommentsInputModel model)
        {
            var result = _service.InsertComment(id, model);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
