using Pegasssus.Common.Models;
using Pegassus.Web.Data.Entities;
using Pegassus.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pegassus.Web.Helpers
{
    public interface IConverterHelper
    {
        EventResponse ToEventResponse(Event eventVar);
        Task<Room> ToRoomAsync(RoomViewModel model, string path, bool isNew);
        RoomViewModel ToRoomViewModel(Room room);
        Task<Event> ToEventAsync(EventViewModel model, bool isNew);
        EventViewModel ToEventViewModel(Event Event);
    }
}
