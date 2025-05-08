using System.ComponentModel.DataAnnotations;

namespace InvoiceManagementSystemAPI.Models.Dto;

public class CustomerItemPriceUpdateDto
{
    [Required]
    public int CustomerItemPriceId { get; set; }
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public int ItemId { get; set; }
    [Required]
    public decimal Price { get; set; }
}