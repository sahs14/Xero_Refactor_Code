using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class ProductOptionDto
    {
        public int Id  { get; set; }
         public int productId  { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        
    }
}