using DAL.Abstract.Repositories;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Maps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _repo;

        public CountryController(ICountryRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<CountryController>
        [HttpGet]
        public ActionResult<IEnumerable<Country>> Get()
        {
            try
            {
                var list = _repo.GetAll();
                return Ok(list);
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // GET api/<CountryController>/5
        [HttpGet("{id}")]
        public ActionResult<Country> Get(int id)
        {
            try
            {
                var country = _repo.GetById(id);
                return Ok(country);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<CountryController>
        [HttpPost]
        public ActionResult<int> Post([FromBody] Country item)
        {
            if (item == null)
            {
                return NotFound();
            }
            try
            {
                var newItemId = _repo.Insert(item);
                return Ok(newItemId);
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        [HttpPost("AddCityToCountry/{countryId}&{cityId}")]
        public ActionResult<bool> AddCityToCountry(int countryId, int cityId)
        {
            try
            {
                return Ok(_repo.AddCityToCounty(countryId, cityId));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // PUT api/<CountryController>
        [HttpPut]
        public ActionResult<bool> Put([FromBody] Country item)
        {
            try
            {
                return Ok(_repo.Update(item));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<CountryController>/5
        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            try
            {
                return Ok(_repo.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
