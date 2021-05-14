using Common.BL.Abstract;
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
    public class HouseController : ControllerBase
    {
        private readonly IHouseRepository _repo;
        private readonly IHouseBusinessLogic _bl;
        private readonly JsonSerializerOptions _jsonIgnoreNullOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            WriteIndented = true
        };

        public HouseController(IHouseRepository repo, IHouseBusinessLogic bl)
        {
            _repo = repo;
            _bl = bl;
        }

        [HttpPost("{houseId}/AddAppartmentsToHouse")]
        public ActionResult<bool> AddAppartmentsToHouse(int houseId, [FromBody] List<Appartment> entities)
        {
            try
            {
                return Ok(_repo.AddAppartmentsToHouse(houseId, entities.AsEnumerable()));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // GET: api/<HouseController>
        [HttpGet("GetPeopleFromHouse/{id}")]
        public ActionResult<IEnumerable<House>> GetPeopleFromHouse(int id)
        {
            try
            {
                return Ok(
                    JsonSerializer.Serialize(_bl.GetPeopleFromHouse(id), _jsonIgnoreNullOptions));

            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // GET: api/<HouseController>
        [HttpGet]
        public ActionResult<IEnumerable<House>> Get()
        {
            try
            {
                return Ok(
                    JsonSerializer.Serialize(_repo.GetAll(), _jsonIgnoreNullOptions));

            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // GET api/<HouseController>/5
        [HttpGet("{id}")]
        public ActionResult<House> Get(int id)
        {
            try
            {
                return Ok(
                    JsonSerializer.Serialize(_repo.GetById(id), _jsonIgnoreNullOptions));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // POST api/<HouseController>
        [HttpPost]
        public ActionResult<int> Post([FromBody] House entity)
        {
            if (entity == null)
                return NotFound();

            try
            {
                return Ok(_repo.Insert(entity));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // PUT api/<HouseController>/5
        [HttpPut("{id}")]
        public ActionResult<bool> Put([FromBody] House entity)
        {
            try
            {
                return Ok(_repo.Update(entity));
            }
            catch (Exception e) { return BadRequest(e.Message); }
        }

        // DELETE api/<HouseController>/5
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
