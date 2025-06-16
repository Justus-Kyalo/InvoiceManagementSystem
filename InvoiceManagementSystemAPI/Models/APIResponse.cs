using System.Net;

namespace InvoiceManagementSystemAPI.Models;

public class APIResponse
{
    public APIResponse()
    {
        Errors = new List<string>();
    }

    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public List<string> Errors { get; set; }
    public object Result { get; set; }
}