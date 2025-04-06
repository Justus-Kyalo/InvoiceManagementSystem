using System.ComponentModel.DataAnnotations;

namespace InvoiceManagementSystemAPI.Models.Dto;

public class InvoiceCreateDto
{[Required]
    public string CustomerAccountNumber { get; set; }
    [Required]
    public string Item {  get; set; }
    [Required]
    public  DateTime  InvoiceDate { get; set; }
    [Required]
    public string VehicleRegistration { get; set; }
    public  string Description { get; set; }
    [Required]
    public string CollectionSlipNumber { get; set; }
    [Required]
    public string Status { get; set; }
    [Required]
    public decimal VAT {  get; set; }
    [Required]
    public decimal Rate { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public decimal Total { get; set; }
}