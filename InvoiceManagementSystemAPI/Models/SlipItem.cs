using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagementSystemAPI.Models;

public class SlipItem
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SlipItemId { get; set; }
    
    // ForeignKeys
    [Required]
    [ForeignKey("Slip")]
    public int SlipId { get; set; }
    
    [Required]
    [ForeignKey("Item")]
    public int ItemId { get; set; }
    [Required]
    public int Quantity { get; set; }
    public string ?  Description  { get; set; }
    [Required]
    // Navigation Properties
    [Newtonsoft.Json.JsonIgnore]
    public Slip? Slip { get; set; }
    public Item? Item { get; set; }
    
    
    
}