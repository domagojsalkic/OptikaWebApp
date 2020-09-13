using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptikaWeb.Models
{
    public class Frame
    {
        public int FrameId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
