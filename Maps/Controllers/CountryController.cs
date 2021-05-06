﻿using DAL.Abstract.Repositories;
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
                return Ok(_repo.GetAll());
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // GET api/<CountryController>/5
        [HttpGet("{id}")]
        public ActionResult<Country> Get(int id)
        {
            try
            {
                return Ok(_repo.GetById(id));
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

        [HttpPost("AddCityToCountry/{countryId}&{cityId}")]
        public ActionResult<bool> AddCityToCountry(int countryId, int cityId)
        {
            try
            {
                return Ok(_repo.AddCityToCounty(countryId, cityId));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }
    }
}
