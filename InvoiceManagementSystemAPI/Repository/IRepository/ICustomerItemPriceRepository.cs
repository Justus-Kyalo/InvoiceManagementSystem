using InvoiceManagementSystemAPI.Models;

namespace InvoiceManagementSystemAPI.Repository.IRepository;

public interface ICustomerItemPriceRepository:IRepository<CustomerItemPrice>
{
    Task<CustomerItemPrice> UpdateAsync(CustomerItemPrice entity);

}