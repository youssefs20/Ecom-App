﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Entities.Product
{
    public class Product: BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public virtual List<Photo> Photos { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]     
        public virtual Category Category { get; set; }
    }
   
}
