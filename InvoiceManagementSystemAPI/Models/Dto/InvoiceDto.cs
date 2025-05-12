namespace InvoiceManagementSystemAPI.Models.Dto;

public class InvoiceDto
{
    public int InvoiceId { get; set; }
    public int CustomerId { get; set; }
    public  DateTime TaxDate { get; set; }
    public  int JobNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}