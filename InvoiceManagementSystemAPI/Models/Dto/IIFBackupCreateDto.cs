using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace InvoiceManagementSystemAPI.Models.Dto;

public class IIFBackupCreateDto
{
    
    [Required]
    public  DateTime StartDate { get; set; }
    [Required]
    public  DateTime EndDate { get; set; }
    [Required]
    public int CustomerId { get; set; }
    
}