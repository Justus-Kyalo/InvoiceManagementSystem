namespace InvoiceManagementSystemAPI.Models.Dto;

public class CustomerDto
{
    
    public int CustomerId { get; set; }
    
    public  int AccountNumber { get; set; }
    public string Name { get; set; }
    public  bool Taxable { get; set; }
    
}