using InvoiceManagementSystemAPI.Data;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagementSystemAPI.Repository;

public class SlipDetailRepository:Repository<SlipDetail>,ISlipDetailRepository
{
    private readonly ApplicationDbContext _db;
    public SlipDetailRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;

    }
}