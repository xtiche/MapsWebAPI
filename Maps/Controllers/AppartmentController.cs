using DAL.Abstract.Repositories;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppartmentController : ControllerBase
    {
        private readonly IAppartmentRepository _repo;

        public AppartmentController(IAppartmentRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("{appartmentId}/AddPeopleToAppartment")]
        public ActionResult<bool> AddPeopleToAppartment(int appartmentId, [FromBody] List<Person> entities)
        {
            try
            {
                return Ok(_repo.AddPeopleToAppartment(appartmentId, entities.AsEnumerable()));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        [HttpPost("{appartmentId}/RemovePeopleFromAppartment")]
        public ActionResult<bool> RemovePeopleFromAppartment(int appartmentId, [FromBody] List<Person> entities)
        {
            try
            {
                return Ok(_repo.RemovePeopleFromAppartment(appartmentId, entities.AsEnumerable()));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        [HttpGet("{id}/GetPersonsAppartments")]
        public ActionResult<List<Person>> GetPeopleInAppartment(int id)
        {
            try
            {
                return Ok(_repo.GetPeopleInAppartment(id));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // GET: api/<AppartmentController>
        [HttpGet]
        public ActionResult<IEnumerable<Appartment>> Get()
        {
            try
            {
                return Ok(_repo.GetAll());
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // GET api/<AppartmentController>/5
        [HttpGet("{id}")]
        public ActionResult<Appartment> Get(int id)
        {
            try
            {
                return Ok(_repo.GetById(id));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // POST api/<AppartmentController>
        [HttpPost]
        public ActionResult<int> Post([FromBody] Appartment entity)
        {
            if (entity == null)
                return NotFound();

            try
            {
                return Ok(_repo.Insert(entity));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // PUT api/<AppartmentController>/5
        [HttpPut("{id}")]
        public ActionResult<bool> Put([FromBody] Appartment entity)
        {
            try
            {
                return Ok(_repo.Update(entity));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // DELETE api/<AppartmentController>/5
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
