﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Pegasssus.Common.Models
{
    public class RoomResponse
    {
        public int Id { get; set; }

        public string Owner { get; set; }

        public string ImageUrl { get; set; }

        public int Capacity { get; set; }

        public string Address { get; set; }

        public string Remarks { get; set; }
    }
}
