using InvoiceManagementSystemAPI.Models;

namespace InvoiceManagementSystemAPI.Repository.IRepository;

public interface ICustomerRepository:IRepository<Customer>
{
     Task<Customer> UpdateAsync(Customer entity);

}