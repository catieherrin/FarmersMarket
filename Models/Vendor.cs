using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FM2.Models
{
    public class Vendor
    {
        [Key]

        public int VendorId { get; set; }
        [Required]

        public string VendorName { get; set; }
        [Required]

        public string VendorOwner { get; set; }
        [Required]

        public string VendorEmail { get; set; }
        [Required]

        public string VendorSite { get; set; }
        [Required]

        public int VendorPhone { get; set; }
        [Required]

        public string VendorLocation { get; set; }
        [Required]

        public string VendorDesc { get; set; }
        [Required]

        public int VendorJoinDate { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}