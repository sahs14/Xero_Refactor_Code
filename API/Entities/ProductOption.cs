using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class ProductOption
    {
        public int Id  { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public Product Product  { get; set; }
        public int productId { get; set; }
        
    }
}