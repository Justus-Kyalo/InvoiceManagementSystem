using System.ComponentModel.DataAnnotations;

namespace InvoiceManagementSystemAPI.Models.Dto;

public class SlipUpdateDto

{
    [Required]
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
             
}