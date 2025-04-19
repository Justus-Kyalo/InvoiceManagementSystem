using InvoiceManagementSystemAPI.Models;

namespace InvoiceManagementSystemAPI.Repository.IRepository;

public interface IItemRepository:IRepository<Item>
{
    Task<Item> UpdateAsync(Item entity);
}