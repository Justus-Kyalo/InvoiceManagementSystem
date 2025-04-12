using InvoiceManagementSystemAPI.Data;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Repository.IRepository;

namespace InvoiceManagementSystemAPI.Repository;

public class IIFBackupRepository:Repository<IIFBackup>,IIIFBackupRepository
{
    private readonly ApplicationDbContext _db;
    public IIFBackupRepository(ApplicationDbContext db) : base(db)
    {

        _db = db;
    }
    public async Task CreateAsync(IIFBackup entity)
    {
        entity.GeneratedOn=DateTime.Now;
        await _db.IIFBackups.AddAsync(entity);
        await _db.SaveChangesAsync();
    }
}