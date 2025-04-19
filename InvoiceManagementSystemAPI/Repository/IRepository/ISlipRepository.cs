using InvoiceManagementSystemAPI.Models;

namespace InvoiceManagementSystemAPI.Repository.IRepository;

public interface ISlipRepository:IRepository<Slip>
{
  public Task<Slip> UpdateAsync(Slip entity);
}