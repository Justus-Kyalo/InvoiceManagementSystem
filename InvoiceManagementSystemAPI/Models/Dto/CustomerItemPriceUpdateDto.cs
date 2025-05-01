namespace InvoiceManagementSystemAPI.Models.Dto;

public class CustomerItemPriceUpdateDto
{
    public int CustomerItemPriceId { get; set; }
  
    public int CustomerId { get; set; }
    
    public int ItemId { get; set; }
    
    public decimal Price { get; set; }
}