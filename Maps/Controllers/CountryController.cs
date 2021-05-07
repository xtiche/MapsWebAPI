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
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _repo;
        private readonly JsonSerializerOptions _jsonIgnoreNullOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            WriteIndented = true
        };

        public CountryController(ICountryRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("{countryId}/AddCitiesToCountry")]
        public ActionResult<bool> AddCitiesToCountry(int countryId, [FromBody] List<City> cities)
        {
            try
            { 
                return Ok(_repo.AddCitiesToCountry(countryId, cities.AsEnumerable()));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // GET: api/<CountryController>
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

        // GET api/<CountryController>/5
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

        // POST api/<CountryController>
        [HttpPost]
        public ActionResult<int> Post([FromBody] Country entity)
        {
            if (entity == null)
                return NotFound();

            try
            {
                return Ok(_repo.Insert(entity));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // PUT api/<CountryController>
        [HttpPut]
        public ActionResult<bool> Put([FromBody] Country entity)
        {
            try
            {
                return Ok(_repo.Update(entity));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // DELETE api/<CountryController>/5
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
