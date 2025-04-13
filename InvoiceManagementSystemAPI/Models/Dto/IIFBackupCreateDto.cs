using System.ComponentModel.DataAnnotations;

namespace InvoiceManagementSystemAPI.Models.Dto;

public class IIFBackupCreateDto
{
    
    [Required]
    public  DateTime StartDate { get; set; }
    [Required]
    public  DateTime EndDate { get; set; }
    
}