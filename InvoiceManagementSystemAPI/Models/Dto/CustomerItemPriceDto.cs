namespace InvoiceManagementSystemAPI.Models.Dto;

public class CustomerItemPriceDto
{
    public int CustomerId { get; set; }
    
    public int ItemId { get; set; }
    
    public decimal Price { get; set; }
}