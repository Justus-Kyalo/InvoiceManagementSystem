using System.ComponentModel.DataAnnotations;

namespace InvoiceManagementSystemAPI.Models.Dto;

public class SlipUpdateDto

{
    [Required]
    public int SlipId { get; set; }
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public  DateTime  SlipDate { get; set; }
    public int? VehicleId { get; set; }
    [Required]
    public string SlipNumber { get; set; }
    [Required]
    public string Status { get; set; }
    [Required]
    public List<SlipItemUpdateDto> SlipItems {  get; set; }
             
}