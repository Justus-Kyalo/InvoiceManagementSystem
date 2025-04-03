using InvoiceManagementSystemAPI.Models;

namespace InvoiceManagementSystemAPI.Repository.IRepository;

public interface IInvoiceRepository
{
  public Task<Invoice> UpdateAsync(Invoice entity);
}