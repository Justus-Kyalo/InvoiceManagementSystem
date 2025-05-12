using InvoiceManagementSystemAPI.Data;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Repository.IRepository;

namespace InvoiceManagementSystemAPI.Repository;

public class InvoiceRepository : Repository<Invoice>,IInvoiceRepository
{
    private readonly ApplicationDbContext _db;

    public InvoiceRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

}