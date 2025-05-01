using System.ComponentModel.DataAnnotations;

namespace InvoiceManagementSystemAPI.Models.Dto;

public class SlipCreateDto
{   
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public List<SlipItem> SlipItems {  get; set; }
    [Required]
    public  DateTime  SlipDate { get; set; }
    [Required]
    public int VehicleId { get; set; }
    [Required]
    public string SlipNumber { get; set; }
    [Required]
    public string Status { get; set; }
    [Required]
    public DateTime CreatedDate { get; set; }

}