﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBuenasPracticas.DTOs
{
    public class PostDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
