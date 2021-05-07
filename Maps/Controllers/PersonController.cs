using DAL.Abstract.Repositories;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Maps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _repo;

        public PersonController(IPersonRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("{personId}/AddAppartmentsToPerson")]
        public ActionResult<bool> AddAppartmentsToPerson(int personId, [FromBody] List<Appartment> entities)
        {
            try
            {
                return Ok(_repo.AddAppartmentsToPerson(personId, entities.AsEnumerable()));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        [HttpPost("{personId}/RemoveAppartmentsFromPerson")]
        public ActionResult<bool> RemoveAppartmentsFromPerson(int personId, [FromBody] List<Appartment> entities)
        {
            try
            {
                return Ok(_repo.RemoveAppartmentsFromPerson(personId, entities.AsEnumerable()));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        [HttpGet("{id}/GetPersonsAppartments")]
        public ActionResult<List<Appartment>> GetPersonsAppartments(int id)
        {
            try
            {
                return Ok(_repo.GetPersonsAppartments(id));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }


        // GET: api/<PersonController>
        [HttpGet]
        public ActionResult<String> Get()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    IgnoreNullValues = true,
                    WriteIndented = true
                };
                return Ok(JsonSerializer.Serialize(_repo.GetAll(), options));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public ActionResult<Person> Get(int id)
        {
            try
            {
                return Ok(_repo.GetById(id));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // POST api/<PersonController>
        [HttpPost]
        public ActionResult<int> Post([FromBody] Person entity)
        {
            if (entity == null)
                return NotFound();

            try
            {
                return Ok(_repo.Insert(entity));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public ActionResult<bool> Put([FromBody] Person entity)
        {
            try
            {
                return Ok(_repo.Update(entity));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            try
            {
                return Ok(_repo.Delete(id));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }
    }
}
