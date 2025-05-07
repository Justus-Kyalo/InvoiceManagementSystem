using InvoiceManagementSystemAPI.Data;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Repository.IRepository;

namespace InvoiceManagementSystemAPI.Repository;

public class VehicleRepository:Repository<Vehicle>,IVehicleRepository
{
    private readonly ApplicationDbContext _db;
    public VehicleRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<Vehicle> Update(Vehicle entity)
    {
        _db.Vehicles.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}