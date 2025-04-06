using System.ComponentModel.DataAnnotations;

namespace InvoiceManagementSystemAPI.Models.Dto;

public class InvoiceDto
{   
    public  int InvoiceId { get; set; }
    [Required]
    [MaxLength(30)]
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
}