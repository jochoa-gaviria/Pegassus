using Microsoft.AspNetCore.Http;
using Pegassus.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pegassus.Web.Models
{
    public class RoomViewModel : Room
    {
        public int OwnerId { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
