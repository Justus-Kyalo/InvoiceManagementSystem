using InvoiceManagementSystemAPI.Services.IServices;

namespace InvoiceManagementSystemAPI.Services;

public class QBAuth:IQBAuth
{
    public Task<string> TokenGenerator()
    {
        return null;
    }
}