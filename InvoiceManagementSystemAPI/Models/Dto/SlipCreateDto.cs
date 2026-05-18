using System.ComponentModel.DataAnnotations;

namespace InvoiceManagementSystemAPI.Models.Dto;

public class SlipCreateDto
{   
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public  DateTime  SlipDate { get; set; }
    public int? VehicleId { get; set; }
    [Required]
    public int SlipNumber { get; set; }
    [Required]
    public string Status { get; set; }
    [Required]
    public List<SlipItemCreateDto> SlipItems {  get; set; }
}