using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagementSystemAPI.Models;

public class CustomerItemPrice
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerItemPriceId { get; set; }
    
    [Required]
    [ForeignKey("Customer")]
    public int CustomerId { get; set; }
    
    [Required]
    [ForeignKey("Item")]
    public int ItemId { get; set; }
    
    [Required]
    [Precision(18, 2)]
    public decimal Price { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public DateTime UpdatedAt { get; set; }
    
    public Item? Item { get; set; }
    [Newtonsoft.Json.JsonIgnore]
    public Customer? Customer { get; set; }
    
    
}