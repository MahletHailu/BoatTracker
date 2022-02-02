using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoatTrackerDomain.DataTransferObjects;
using BoatTrackerDomain.Models;
using BoatTrackerDomain.Repository;
using Microsoft.EntityFrameworkCore;

namespace BoatTrackerDomain.Services
{
    public class BoatDataManagers : IDataRepository<Boat, BoatDto>
    {
        private readonly BoatTrackerContext _boatTrackerContext;

        public BoatDataManagers(BoatTrackerContext boatTrackerContext)
        {
            _boatTrackerContext = boatTrackerContext
                ?? throw new ArgumentNullException(nameof(boatTrackerContext));
        }

        /// <summary>
        /// <c>AddAsync</c>
        /// This async method adds a new boat to the data store
        /// If there is already a boat instance with the same HIN(Hull Identification Number), it will throw an error
        /// </summary>
        /// <param name="boatDto"></param>
        /// <returns>Task</returns>
        public async Task AddAsync(BoatDto boatDto)
        {
            if (boatDto == null)
            {
                throw new ApplicationException("Data can't be null");
            }

            var existingBoat = await _boatTrackerContext.Boats
            .SingleOrDefaultAsync(b => b.HIN == boatDto.HIN);

            if (existingBoat != null)
            {
                throw new ApplicationException("Data already exists");
            }

            _boatTrackerContext.Boats.Add(MapToEntity(boatDto));
            _boatTrackerContext.SaveChanges();
        }

        /// <summary>
        /// <c>UpdateAsync</c>
        /// This async method updates an existing boat 
        /// If there is already a boat instance with the same HID(Hull Identification Number), it will throw an error
        /// </summary>
        /// <param name="boatDto"></param>
        /// <returns>Task<bool></returns>
        public async Task<bool> UpdateAsync(BoatDto boatDto)
        {
            if (boatDto == null)
            {
                throw new ApplicationException("data can not be null");
            }

            var existingBoat = await _boatTrackerContext.Boats
            .SingleOrDefaultAsync(b => b.HIN == boatDto.HIN);

            if (existingBoat == null)
            {
                throw new ApplicationException("Data doesn't exist");
            }

            if (existingBoat != null)
            {
                existingBoat.Name = boatDto.Name;
                existingBoat.State = Convert.ToByte(boatDto.BoatState.Id);
                _boatTrackerContext.SaveChanges();

                return true;
            }

            return false;
        }

        public void Delete(Boat entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <c>GetAsync</c>
        /// This async method finds boat by HIN
        /// </summary>
        /// <param name="HIN"></param>
        /// <returns>Task<BoatDto></returns>
        public async Task<BoatDto> GetAsync(string HIN)
        {
            var boat = await _boatTrackerContext.Boats
                .Include(boat => boat.BoatState)
                .SingleOrDefaultAsync(b => b.HIN == HIN);

            if (boat == null)
            {
                throw new ApplicationException("Unable to find a boat with that HIN");
            }

            return MapToDto(boat);
        }

        /// <summary>
        /// <c>GetAllAsync</c>
        /// This async method gets all boats
        /// </summary>
        /// <returns>Task<IEnumerable<BoatDto>></returns
        public async Task<IEnumerable<BoatDto>> GetAllAsync()
        {
            var boatDtos = await _boatTrackerContext.Boats
               .Include(boat => boat.BoatState)
               .OrderBy(b => b.Name)
               .ToListAsync();

            return boatDtos.Select(b => MapToDto(b)).ToList();
        }

        /// <summary>
        /// <c>MapToDto</c>
        /// A mapping utility function to map boat model to DTO
        /// </summary>
        /// <param name="boat"></param>
        /// <returns>BoatDto</returns
        private BoatDto MapToDto(Boat boat)
        {
            var boatDto = (boat != null) ? new BoatDto
            {
                HIN = boat?.HIN,
                Name = boat?.Name,
                BoatState = new()
                {
                    Id = boat?.BoatState?.Id ?? 0,
                    Description = boat?.BoatState?.Description ?? "",
                }
            }
            : null;

            return boatDto;
        }

        /// <summary>
        /// <c>MapToDto</c>
        /// A mapping utility function to map boat dto to model
        /// </summary>
        /// <param name="boatDto"></param>
        /// <returns>Boat</returns
        private Boat MapToEntity(BoatDto boatDto)
        {
            var boat = (boatDto != null) ? new Boat
            {
                HIN = boatDto?.HIN,
                Name = boatDto?.Name,
                State = Convert.ToByte(boatDto?.BoatState?.Id ?? 0),
            }
            : null;

            return boat;
        }
    }
}
