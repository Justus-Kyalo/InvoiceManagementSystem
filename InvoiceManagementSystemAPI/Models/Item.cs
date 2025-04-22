using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagementSystemAPI.Models;

public class Item
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ItemId { get; set; }
    [Required]
    public string ItemName { get; set; }
    public ICollection<SlipItem> SlipItems { get; set; }
    
}