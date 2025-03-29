using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagementSystemAPI.Models
{
    public class Invoice
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string Item {  get; set; }
        public  DateTime  InvoiceDate { get; set; }
        public string VehicleRegistration { get; set; }
        public  string Description { get; set; }
        public string CollectionSlipNumber { get; set; }
        public string Status { get; set; }
        public decimal VAT {  get; set; }
        public decimal Rate { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }


    }
}
