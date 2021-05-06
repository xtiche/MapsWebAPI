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
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _repo;

        public CityController(ICityRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<CityController>
        [HttpGet]
        public ActionResult<IEnumerable<City>> Get()
        {
            try
            {
                var list = _repo.GetAll();
                return Ok(list);
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // GET api/<CityController>/5
        [HttpGet("{id}")]
        public ActionResult<City> Get(int id)
        {
            try
            {
                return Ok(_repo.GetById(id));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // POST api/<CityController>
        [HttpPost]
        public ActionResult<int> Post([FromBody] City entity)
        {
            if (entity == null)
                return NotFound();

            try
            {
                return Ok(_repo.Insert(entity));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // PUT api/<CityController>/5
        [HttpPut("{id}")]
        public ActionResult<bool> Put([FromBody] City entity)
        {
            try
            {
                return Ok(_repo.Update(entity));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // DELETE api/<CityController>/5
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
