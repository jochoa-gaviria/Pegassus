using System;
using System.Collections.Generic;
using System.Text;

namespace Pegasssus.Common.Models
{
    public class EventResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int InvitesNumber { get; set; }

        public string EventType { get; set; }

        public string Remarks { get; set; }

        public string Organizer { get; set; }

        public string RoomAddress { get; set; }

        public int RoomCapacity { get; set; }
    }
}
