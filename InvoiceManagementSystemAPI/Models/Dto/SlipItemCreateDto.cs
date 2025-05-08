using System.ComponentModel.DataAnnotations;

namespace InvoiceManagementSystemAPI.Models.Dto;

public class SlipItemCreateDto
{
    [Required]
    public int ItemId { get; set; }
    [Required]
    public int Quantity { get; set; }
    public string ? Description { get; set; }
}