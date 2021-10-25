using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class ProductOption
    {
        [Key]
        public int Id  { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public virtual Product Product  { get; set; }
        public int productId { get; set; }
        
    }
}