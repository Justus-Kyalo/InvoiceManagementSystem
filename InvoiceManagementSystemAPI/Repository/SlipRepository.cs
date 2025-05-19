using InvoiceManagementSystemAPI.Data;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Repository.IRepository;

namespace InvoiceManagementSystemAPI.Repository;

public class SlipRepository:Repository<Slip>,ISlipRepository
{
    private readonly ApplicationDbContext _db;
    public SlipRepository(ApplicationDbContext db):base(db)
    {
        _db = db;

    }

    public async Task<Slip> UpdateAsync(Slip entity)
    {
      entity.UpdatedAt=DateTime.Now;
      _db.Slips.Update(entity);
      await _db.SaveChangesAsync();
      return entity;
    }
}