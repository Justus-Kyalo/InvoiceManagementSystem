using System.ComponentModel.DataAnnotations;

namespace InvoiceManagementSystemAPI.Models.Dto;

public class CustomerUpdateDto
{  [Required]
    public int CustomerId { get; set; }
    [Required]
    public  string AccountNumber { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public  bool Taxable { get; set; }
    [Required]
    public bool Active { get; set; }
    [Required]
    public ICollection<CustomerItemPriceUpdateDto> CustomerItemPrices { get; set; }
}