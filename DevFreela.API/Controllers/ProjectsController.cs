using DevFreela.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        //Padrão options, atráves da propriedade value consegue configurações
        private readonly FreelanceTotalCostConfig _config;
        public ProjectsController(IOptions<FreelanceTotalCostConfig> options)
        {
            _config = options.Value;
        }

        //Get api/projects?search=crm
        [HttpGet]
        public IActionResult Get(string search)
        {
            return Ok();
        }

        //Get api/projects/id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        //Post api/projects
        //CreatedAtAction quando vocçe futuramente vai poder consultar em outro endpoint
        //Primeiro é o metodo que poderar se consultado, segundo parametro que no caso é o id e terceiro o modelo
        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        {
            if (model.TotalCoast < _config.Minimum || model.TotalCoast > _config.Maximum)
            {
                return BadRequest("Numero fora dos limites");
            }

            return CreatedAtAction(nameof(GetById), new {id = 1}, model);
        }        

        //PUT api/projects/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {
            return NoContent();
        }

        //PUT api/projects/id/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            return NoContent();
        }

        //PUT api/projects/id/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            return NoContent();
        }

        //DELETE api/projects/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(CreateProjectCommentsInputModel model)
        {
            return Ok();
        }
    }
}
