using Microsoft.Build.Framework;

namespace InvoiceManagementSystemAPI.Models.Dto;

public class ItemCreateDto
{
    [Required]
    public string ItemName { get; set; }
}