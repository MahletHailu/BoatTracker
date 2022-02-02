using System;
using System.Collections.Generic;

#nullable disable

namespace BoatTrackerDomain.Models
{
    public partial class State
    {
        /// <summary>
        /// number identification of state 
        /// 0, 1, 2, 3
        /// </summary>
        private byte _id;
        public byte Id
        {
            get
            {
                return this._id;
            }

            set
            {
                if (value < 0 || value > 3)
                {
                    throw new ArgumentOutOfRangeException("State has to be between 0 and 3");
                }

                _id = value;
            }
        }

        /// <summary>
        /// String representation of state
        /// Docked, Outbound to Sea, Inbound to Harbor, Maintenance
        /// </summary>
        public string Description { get; set; }
        public virtual ICollection<Boat> Boats { get; set; }
        public State()
        {
            Id = Convert.ToByte(0);
            Description = string.Empty;
        }
    }
}
