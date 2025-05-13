namespace InvoiceManagementSystemAPI.Models;

public class SlipDetail
{
    public int CustomerId { get; set; }
    public int SlipId { get; set; }
    public DateTime SlipDate { get; set; }
    public int SlipNumber { get; set; }
    public string Status { get; set; }
    public int AccountNumber { get; set; }
    public bool Active { get; set; }
    public string Name { get; set; }
    public bool Taxable { get; set; }
    public string VehicleRegistration { get; set; }
    public string ItemName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

}