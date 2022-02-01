using BoatTracker.Service.Model;
using BoatTrackerDomain.DataTransferObjects;
using BoatTrackerDomain.Models;
using BoatTrackerDomain.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BoatTracker.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoatsController : ControllerBase
    {
        private readonly IDataRepository<Boat, BoatDto> _dataRepository;

        public BoatsController(IDataRepository<Boat, BoatDto> dataRepository)
        {
            _dataRepository = dataRepository
                           ?? throw new System.ArgumentNullException(nameof(dataRepository));
        }

        // GET: api/boats
        //Gets all boats
        [HttpGet(Name = "GetAllBoats")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var boats = await _dataRepository.GetAllAsync();

                return Ok(boats);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        // GET api/api/boats/ABC67689B606
        [HttpGet("{HIN}", Name = "GetBoatByHIN")]
        public async Task<IActionResult> Get(string HIN)
        {
            try
            {
                var boat = await _dataRepository.GetAsync(HIN);

                return Ok(boat);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // POST api/boats/
        [HttpPost]
        public IActionResult Post([FromBody] BoatDto boatDto)
        {
            try
            {
                if (!string.IsNullOrEmpty(boatDto?.HIN))
                {
                    _dataRepository.Add(boatDto);

                    return CreatedAtRoute("GetBoatByHIN", new { HIN = boatDto.HIN }, null);
                }
                else
                {
                    return BadRequest("HIN can not be null.");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT api/boats/ABC67689B606
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] BoatDto boatDto)
        {
            try
            {
                if (!string.IsNullOrEmpty(boatDto?.HIN))
                {
                    return await _dataRepository.UpdateAsync(boatDto) ?
                         CreatedAtRoute("GetBoatByHIN", new { HIN = boatDto.HIN }, null)
                         : BadRequest("Data couldn't be updated. Please make sure you're updating an existing data and the format is correct.");
                }
                else
                {
                    return BadRequest("HIN can not be null.");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE api/boats/ABC12345D404
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
