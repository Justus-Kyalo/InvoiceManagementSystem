using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagementSystemAPI.Models
{
    public class Slip
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SlipId { get; set; }
        [Required]
        public string CustomerAccountNumber { get; set; }
        [Required]
        public List<SlipItem> SlipItems {  get; set; }
        [Required]
        public  DateTime  SlipDate { get; set; }
        [Required]
        public string VehicleRegistration { get; set; }
        [Required]
        public string SlipNumber { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime createdDate { get; set; }
        [Required]
        public DateTime updatedDate { get; set; }


    }
}
