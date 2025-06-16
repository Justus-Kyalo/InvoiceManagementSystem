using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagementSystemAPI.Services.IServices;

public interface IQBAuth
{
    Task<string> TokenGenerator();
}