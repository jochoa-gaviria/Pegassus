using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pegassus.Web.Data;
using Pegassus.Web.Data.Entities;

namespace Pegassus.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EventTypesController : ControllerBase
    {
        private readonly DataContext _context;
        public EventTypesController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetEventTypes")]
        public IEnumerable<EventType> GetEventTypes()
        {
            return _context.EventTypes.OrderBy(pt => pt.Name);
        }
    }
}