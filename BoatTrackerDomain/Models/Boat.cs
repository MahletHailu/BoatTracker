using System;
using System.Collections.Generic;

#nullable disable

namespace BoatTrackerDomain.Models
{
    public partial class Boat
    {
        public string HIN { get; set; }
        public string Name { get; set; }

        private byte _state;
        public byte State
        {
            get
            {
                return this._state;
            }

            set
            {
                if (value < 0 || value > 3)
                {
                    throw new ArgumentOutOfRangeException("State has to be between 0 and 3");
                }

                _state = value;
            }
        }
        public virtual State BoatState { get; set; }

        public Boat()
        {
            HIN = string.Empty;
            Name = string.Empty;
            State = 0;
        }
    }
}
