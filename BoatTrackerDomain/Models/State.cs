using System;
using System.Collections.Generic;

#nullable disable

namespace BoatTrackerDomain.Models
{
    public partial class State
    {
        public State()
        {
            Boats = new HashSet<Boat>();
        }

        public byte Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Boat> Boats { get; set; }
    }
}
