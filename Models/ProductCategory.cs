using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FM2.Models
{
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int VendorId { get; set; }
        public int ReviewId { get; set; }
    }
}