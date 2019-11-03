using Pegasssus.Common.Models;
using Pegassus.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pegassus.Web.Helpers
{
    public interface IConverterHelper
    {
        EventResponse ToEventResponse(Event eventVar);
    }
}
