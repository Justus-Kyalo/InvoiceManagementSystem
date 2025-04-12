using InvoiceManagementSystemAPI.Models;

namespace InvoiceManagementSystemAPI.Repository.IRepository;

public interface IIIFBackupRepository:IRepository<IIFBackup>
{
    new Task CreateAsync(IIFBackup entity);
}
