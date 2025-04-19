using InvoiceManagementSystemAPI.Models;

namespace InvoiceManagementSystemAPI.Services.IServices;

public interface IIIFGeneratorService
{
    string GenerateIIFContent(List<Slip> invoices);
}