using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OptikaWeb.Models;

    public class AppContext : DbContext
    {
        public AppContext (DbContextOptions<AppContext> options)
            : base(options)
        {
        }

        public DbSet<OptikaWeb.Models.Frame> Frame { get; set; }

        public DbSet<OptikaWeb.Models.Category> Category { get; set; }
    }
