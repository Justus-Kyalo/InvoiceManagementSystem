using Microsoft.Build.Framework;

namespace InvoiceManagementSystemAPI.Models.Dto;

public class InvoiceCreateDto
{    
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public  DateTime TaxDate { get; set; }
    [Required]
    public  int JobNumber { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
}