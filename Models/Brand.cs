using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudOpeartionsInDotnetCore.Models
{
    public class Brand
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public int Active { get; set; }
    }
}