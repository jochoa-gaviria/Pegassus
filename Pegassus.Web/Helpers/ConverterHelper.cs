using Pegasssus.Common.Models;
using Pegassus.Web.Data;
using Pegassus.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pegassus.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext dataContext, ICombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
        }

        public EventResponse ToEventResponse(Event eventVar)
        {
            if (eventVar == null)
            {
                return null;
            }

            return new EventResponse
            {
                Id = eventVar.Id,
                Name = eventVar.Name,
                EventType = eventVar.EventType.Name,
                InvitedsNumber = eventVar.InvitesNumber,
                Remarks = eventVar.Remarks
            };
        }
    }
}
