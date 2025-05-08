using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagementSystemAPI.Models
{
    public class Slip
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SlipId { get; set; }
        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [Required]
        public  DateTime  SlipDate { get; set; }
        [Required]
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        [Required]
        public string SlipNumber { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime UpdatedDate { get; set; }
        [Required]
        public ICollection<SlipItem> SlipItems {  get; set; }
        
        public Customer?  Customer { get; set; }
        public Vehicle?  Vehicle { get; set; }


    }
}
