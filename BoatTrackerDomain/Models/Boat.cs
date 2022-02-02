using System;
using System.Collections.Generic;

#nullable disable

namespace BoatTrackerDomain.Models
{
    public partial class Boat
    {
        /// <summary>
        /// HIN: Hull Identification Number
        /// Identification of a boat
        /// </summary>
        public string HIN { get; set; }

        /// <summary>
        /// Human readable name of a boat
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// State of a boat
        /// 0: Docked
        /// 1:Outbound to Sea
        /// 2:Inbound to Harbor
        /// 3: Maintenance
        /// </summary>
        private byte _state;

        /// <summary>
        /// public setter and getter for state
        /// Validation: State can't be less than 0 or greater than 3
        /// </summary>
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
            State = Convert.ToByte(0);
        }
    }
}
