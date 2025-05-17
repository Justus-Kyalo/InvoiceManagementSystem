using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagementSystemAPI.Models;

public class Invoice
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InvoiceId { get; set; }
    [Required]
    [ForeignKey("Customer")]
    public int CustomerId { get; set; }
    [Required]
    public  DateTime TaxDate { get; set; }
    [Required]
    public  int JobNumber { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public DateTime CreatedDate { get; set; }
    [Required]
    public DateTime UpdatedDate { get; set; }
    public Customer?  Customer { get; set; }
}