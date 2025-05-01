using Microsoft.Build.Framework;

namespace InvoiceManagementSystemAPI.Models.Dto;

public class VehicleUpdateDto
{
    [Required]
    public int VehicleId { get; set; }
    [Required]
    public string VehicleRegistration { get; set; }
    public string? Description { get; set; }
}