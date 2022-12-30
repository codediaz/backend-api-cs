using System;
using System.Collections.Generic;

namespace BackEndAPI.Models
{
    public partial class Product
    {
        public int IdItem { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
    }
}
