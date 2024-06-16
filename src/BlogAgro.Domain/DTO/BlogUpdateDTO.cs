﻿using BlogAgro.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAgro.Domain.DTO
{
    public class BlogUpdateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Text { get; set; }
        public DateTime PostDate { get; set; }
    }
}
