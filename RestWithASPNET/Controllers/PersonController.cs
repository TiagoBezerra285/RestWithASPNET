﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Filters;
using RestWithASPNETUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    //[Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;
        private IPersonBusiness _personBusiness;
        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get([FromQuery] string name, string sortDirection, int pageSize, int page)
        {
            return Ok(_personBusiness.FindWithPagedSearch(name, sortDirection, pageSize, page));
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetId(long id)
        {
            var person = _personBusiness.FindByID(id);
            if (person == null) return NotFound();

            return Ok(person);
        }

        [HttpGet("findPersonByName")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(4001)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var person = _personBusiness.FindByName(firstName, lastName);
            if (person == null) return NotFound();

            return Ok(person);
        }


        [HttpPost]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(4001)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO PersonVO)
        {
           
            if (PersonVO == null) return BadRequest();
            return Ok(_personBusiness.Create(PersonVO));
        }


        [HttpPut]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(4001)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonVO PersonVO)
        {

            if (PersonVO == null) return BadRequest();
            return Ok(_personBusiness.Update(PersonVO));
        }


        [HttpPatch("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(4001)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Patch(long id)
        {
            var person = _personBusiness.Disable(id);
            return Ok(person);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(4001)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }

    }
}
