using InvoiceManagementSystemAPI.Models;

namespace InvoiceManagementSystemAPI.Repository.IRepository;

public interface IVehicleRepository:IRepository<Vehicle>
{

    Task<Vehicle> Update(Vehicle entity);

}