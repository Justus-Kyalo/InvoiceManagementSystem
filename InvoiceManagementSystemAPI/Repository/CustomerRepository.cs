using InvoiceManagementSystemAPI.Data;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Repository.IRepository;

namespace InvoiceManagementSystemAPI.Repository;

public class CustomerRepository:Repository<Customer>,ICustomerRepository
{
    private readonly ApplicationDbContext _db;
    public CustomerRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<Customer> UpdateCustomer(Customer entity)
    {
       _db.Customers.Update(entity);
       await _db.SaveChangesAsync();
       return entity;

    }
}