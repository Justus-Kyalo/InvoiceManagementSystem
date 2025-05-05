namespace InvoiceManagementSystemAPI.Models.Dto;

public class CustomerExtendCIPDto
{
    public int CustomerId { get; set; }
    
    public  int AccountNumber { get; set; }
    public string Name { get; set; }
    public  bool Taxable { get; set; }
    public bool Active { get; set; }
    public ICollection<CustomerItemPriceTrimDto> CustomerItemPrices { get; set; }

}