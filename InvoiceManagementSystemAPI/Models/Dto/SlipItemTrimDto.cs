using Microsoft.Build.Framework;

namespace InvoiceManagementSystemAPI.Models.Dto;

public class SlipItemTrimDto
{
    public int SlipItemId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public string ?  Description  { get; set; }
}