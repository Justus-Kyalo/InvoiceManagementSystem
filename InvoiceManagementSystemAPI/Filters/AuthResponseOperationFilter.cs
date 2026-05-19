using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InvoiceManagementSystemAPI.Filters;

public class AuthResponseOperationFilter:IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasAllowAnonymous =
            context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                .OfType<AllowAnonymousAttribute>().Any()
            || context.MethodInfo.GetCustomAttributes(true)
                .OfType<AllowAnonymousAttribute>().Any();

        if (hasAllowAnonymous)
            return;

        operation.Responses.TryAdd("401", new OpenApiResponse
        {
            Description = "Unauthorized"
        });

        operation.Responses.TryAdd("403", new OpenApiResponse
        {
            Description = "Forbidden"
        });
    }
}