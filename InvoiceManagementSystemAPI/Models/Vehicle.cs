using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace InvoiceManagementSystemAPI.Models;

public class Vehicle
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int VehicleId { get; set; }
    [Required]
    public string VehicleRegistration { get; set; }
    public string? Description { get; set; }
}