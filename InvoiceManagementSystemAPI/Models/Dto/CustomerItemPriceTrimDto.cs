namespace InvoiceManagementSystemAPI.Models.Dto;

public class CustomerItemPriceTrimDto
{
    public int CustomerItemPriceId { get; set; }
    
    public int ItemId { get; set; }
    
    public decimal Price { get; set; }
}