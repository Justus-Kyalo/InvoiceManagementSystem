using System.Net;

namespace InvoiceManagementSystemAPI.Models;

public class APIResponse
{
    public APIResponse()
    {
        Errors = new List<string>();
    }

    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess => ((int)StatusCode >= 200 && (int)StatusCode <= 300);
    public List<string> Errors { get; set; }
    public object Result { get; set; }
}