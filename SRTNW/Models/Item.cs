using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRTNW.Models
{
    public class Item
    {
        [Key]
        public string Id { get; set; }
        [Display(Name = "Code")]
        public string ReferenceNo { get; set; }

        [Display(Name = "Item Name")]
        [Required]
        public string ItemName { get; set; }

        [Display(Name = "Item Class")]
        public ItemClass ItemClass { get; set; }

        [Display(Name = "Purchase UOM")]
        public UOM PurchaseUOM { get; set; }

        [Display(Name = "Suspended")]
        public bool IsSuspended { get; set; }
        
        [Display(Name = "Item Group")]
        [ForeignKey("ItemGroup")]
        public string ItemGroupId { get; set; }
        public virtual ItemGroup ItemGroup { get; set; }
    }
}