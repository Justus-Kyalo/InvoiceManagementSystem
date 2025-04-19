using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagementSystemAPI.Models;

public class SlipItem
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SlipItemId { get; set; }
    // ForeignKey to Slip
    [Required]
    public int SlipId { get; set; }
    [ForeignKey("SlipId")]
    public Slip Slip { get; set; }
    
    // ForeignKey to Item
    [Required]
    public int ItemId { get; set; }
    [ForeignKey("ItemId")]
    public Item Item { get; set; }
    
    [Required]
    public int Quantity { get; set; }
    
    public string ?  Description  { get; set; }
    
    
}