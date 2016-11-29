using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SRTNW.Models
{
    public class ItemGroup
    {
        [Key]
        public string Id { get; set; }
        [Display(Name = "Code")]
        public string ReferenceNo { get; set; }
        [Required]
        public string Description { get; set; }

        //[JsonIgnore]
        public List<Item> Items { get; set; }
    }
}