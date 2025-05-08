using System.ComponentModel.DataAnnotations;

namespace InvoiceManagementSystemAPI.Models.Dto;

public class SlipItemUpdateDto

{
    [Required]
    public int SlipItemId { get; set; }
    [Required]
    public int SlipId { get; set; }
    [Required]
    public int ItemId { get; set; }
    [Required]
    public int Quantity { get; set; }
    public string ?  Description  { get; set; }

}