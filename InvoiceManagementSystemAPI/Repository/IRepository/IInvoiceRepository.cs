using InvoiceManagementSystemAPI.Models;

namespace InvoiceManagementSystemAPI.Repository.IRepository;

public interface IInvoiceRepository:IRepository<Invoice>
{
  public Task<Invoice> UpdateAsync(Invoice entity);
}