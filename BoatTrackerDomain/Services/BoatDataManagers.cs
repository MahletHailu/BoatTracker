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

        public void Add(BoatDto boatDto)
        {
            if (boatDto != null)
            {
                _boatTrackerContext.Boats.Add(MapToEntity(boatDto));
                _boatTrackerContext.SaveChanges();
            }
        }

        public async Task<bool> UpdateAsync(BoatDto boatDto)
        {
            if (boatDto != null)
            {
                var existingBoat = await _boatTrackerContext.Boats
                .Include(boat => boat.BoatState)
                .SingleOrDefaultAsync(b => b.HID == boatDto.HID);

                if (existingBoat != null)
                {
                    existingBoat.Name = boatDto.Name;
                    existingBoat.State = Convert.ToByte(boatDto.BoatState.Id);
                    _boatTrackerContext.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        public void Delete(Boat entity)
        {
            throw new NotImplementedException();
        }

        public async Task<BoatDto> GetAsync(string HID)
        {
            var boat = await _boatTrackerContext.Boats
                .Include(boat => boat.BoatState)
                .SingleOrDefaultAsync(b => b.HID == HID);

            return MapToDto(boat);
        }

        public async Task<IEnumerable<BoatDto>> GetAllAsync()
        {
            var boatDtos = await _boatTrackerContext.Boats
               .Include(boat => boat.BoatState)
               .OrderBy(b => b.Name)
               .ToListAsync();

            return boatDtos.Select(b => MapToDto(b)).ToList();
        }

        private BoatDto MapToDto(Boat boat)
        {
            var boatDto = (boat != null) ? new BoatDto
            {
                HID = boat?.HID,
                Name = boat?.Name,
                BoatState = new() {
                    Id = boat?.BoatState?.Id ?? 0,
                    Description = boat?.BoatState?.Description ?? "",
                }
            }
            : null;

            return boatDto;
        }

        private Boat MapToEntity(BoatDto boatDto)
        {
            var boat = (boatDto != null) ? new Boat
            {
                HID = boatDto?.HID,
                Name = boatDto?.Name,
                State = Convert.ToByte(boatDto?.BoatState?.Id?? 0),
            }
            : null;

            return boat;
        }
    }
}
