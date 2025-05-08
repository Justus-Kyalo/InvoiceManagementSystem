using System.ComponentModel.DataAnnotations;

namespace InvoiceManagementSystemAPI.Models.Dto;

public class CustomerItemPriceCreateDto
{
    [Required]
    public int ItemId { get; set; }
    [Required]
    public decimal Price { get; set; }
}