using System.ComponentModel.DataAnnotations;
namespace InvoiceManagementSystemAPI.Models.Dto;

public class CustomerCreateDto
{
    [Required]
    public int AccountNumber { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public  bool Taxable { get; set; }
    [Required]
    public bool Active { get; set; }
    [Required]
    public ICollection<CustomerItemPriceCreateDto> CustomerItemPrices { get; set; }
}