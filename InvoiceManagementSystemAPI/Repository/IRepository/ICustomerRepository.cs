using InvoiceManagementSystemAPI.Models;

namespace InvoiceManagementSystemAPI.Repository.IRepository;

public interface ICustomerRepository:IRepository<Customer>
{
    public Task<Customer> UpdateAsync(Customer entity);

}