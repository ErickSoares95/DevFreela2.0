﻿using DevFreela.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        //Get api/skills
        [HttpGet]
        public IActionResult GetAll() 
        {
            return Ok();
        }

        //Post api/skills
        [HttpPost]
        public IActionResult Post(CreateSkillInputModel model) 
        {
            return Ok();
        }
    }
}
