using System;
using System.Collections.Generic;

#nullable disable

namespace BoatTrackerDomain.Models
{
    public partial class Boat
    {
        public string HID { get; set; }
        public string Name { get; set; }
        public byte State { get; set; }

        public virtual State BoatState { get; set; }
    }
}
