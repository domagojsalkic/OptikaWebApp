using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OptikaWeb.Models;

namespace OptikaWeb.ViewModels
{
    public class FrameViewModel
    {
        public string Name { get; set; }
        public IFormFile ImageUrl { get; set; }
        public Category Category { get; set; }
    }
}
