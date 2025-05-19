namespace InvoiceManagementSystemAPI.Models.Dto;

public class CustomerDto
{
    
    public int CustomerId { get; set; }
    
    public  string AccountNumber { get; set; }
    public string Name { get; set; }
    public  bool Taxable { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    
}