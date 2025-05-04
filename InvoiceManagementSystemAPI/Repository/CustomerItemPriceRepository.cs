using InvoiceManagementSystemAPI.Data;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Repository.IRepository;

namespace InvoiceManagementSystemAPI.Repository;

public class CustomerItemPriceRepository:Repository<CustomerItemPrice>,ICustomerItemPriceRepository
{
    private ApplicationDbContext _db;
    public CustomerItemPriceRepository(ApplicationDbContext db):base(db)
    {
        _db = db;

    }

    public async Task<CustomerItemPrice> UpdateAsync(CustomerItemPrice entity)
    {
        _db.CustomerItemPrices.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}