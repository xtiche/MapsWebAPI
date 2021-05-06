using DAL.Abstract.Repositories;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Maps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreetController : ControllerBase
    {
        private readonly IStreetRepository _repo;
        private readonly JsonSerializerOptions _jsonIgnoreNullOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            WriteIndented = true
        };

        public StreetController(IStreetRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<StreetController>
        [HttpGet]
        public ActionResult<String> Get()
        {
            try
            {
                return Ok(
                    JsonSerializer.Serialize(_repo.GetAll(), _jsonIgnoreNullOptions));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // GET api/<StreetController>/5
        [HttpGet("{id}")]
        public ActionResult<String> Get(int id)
        {
            try
            {
                return Ok(
                   JsonSerializer.Serialize(_repo.GetById(id), _jsonIgnoreNullOptions));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // POST api/<StreetController>
        [HttpPost]
        public ActionResult<int> Post([FromBody] Street entity)
        {
            if (entity == null)
                return NotFound();

            try
            {
                return Ok(_repo.Insert(entity));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // PUT api/<StreetController>/5
        [HttpPut("{id}")]
        public ActionResult<bool> Put([FromBody] Street street)
        {
            try
            {
                return Ok(_repo.Update(street));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // DELETE api/<StreetController>/5
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
