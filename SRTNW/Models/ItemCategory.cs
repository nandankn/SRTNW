using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRTNW.Models
{
    public class ItemCategory
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "Code")]
        public string ReferenceNo { get; set; }
        [Required]
        public string Description { get; set; }
    }
}