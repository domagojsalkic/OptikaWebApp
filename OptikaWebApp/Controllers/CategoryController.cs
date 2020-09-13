using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OptikaWeb.Models;

namespace OptikaWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppContext _context;

        public CategoryController(AppContext context)
        {
            _context = context;
        }

    }
}
