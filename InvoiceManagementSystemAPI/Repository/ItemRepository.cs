using InvoiceManagementSystemAPI.Data;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Repository.IRepository;

namespace InvoiceManagementSystemAPI.Repository;

public class ItemRepository : Repository<Item>,IItemRepository
{
    private readonly ApplicationDbContext _db;
    public ItemRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<Item> UpdateAsync(Item entity)
    {
         _db.Items.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}