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

        /// <summary>
        /// GET: api/boats
        //  Gets all boats 
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// GET api/api/boats/ABC67689B606
        /// Gets a boat by HIN(Hull Indentification Number)
        /// </summary>
        /// <param name="HIN"></param>
        /// <returns></returns>
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

      
        /// <summary>
        ///  POST api/boats/
        ///  Creates a new boat to the data store if it doesn't exist
        ///  HIN can't be empty or duplicate HIN isn't permitted
        /// </summary>
        /// <param name="boatDto"></param>
        /// <returns></returns>
        [HttpPost(Name = "createABoat")]
        public async Task<IActionResult> Post([FromBody] BoatDto boatDto)
        {
            try
            {
                if (!string.IsNullOrEmpty(boatDto?.HIN))
                {
                    await _dataRepository.AddAsync(boatDto);

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


        /// <summary>
        /// PUT api/boats/
        /// updated a boat
        /// HIN must be existing
        /// State can only be (0,1,2,3) 
        /// </summary>
        /// <param name="boatDto"></param>
        /// <returns></returns>
        [HttpPut(Name = "updateABoat")]
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

        
        /// <summary>
        /// DELETE api/boats/ABC12345D404
        /// Deletes a boat
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
