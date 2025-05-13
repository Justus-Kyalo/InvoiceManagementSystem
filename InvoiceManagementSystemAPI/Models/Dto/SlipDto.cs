using System.ComponentModel.DataAnnotations;

namespace InvoiceManagementSystemAPI.Models.Dto;

public class SlipDto
{  
    [Required]
    public int SlipId { get; set; }
    [Required]
    [MaxLength(30)]
    public int CustomerId { get; set; }
    [Required]
    public  DateTime  SlipDate { get; set; }
    [Required]
    public int VehicleId { get; set; }
    [Required]
    public int SlipNumber { get; set; }
    [Required]
    public string Status { get; set; }
    [Required]
    public List<SlipItemTrimDto> SlipItems {  get; set; }
}