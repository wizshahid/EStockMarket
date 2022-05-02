using Microsoft.AspNetCore.Http;

namespace Utilities.Extensions;
public static class HttpContextExtensions
{
    public static string? GetUserId(this HttpContext httpContext)
    {
        return httpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "UserId")?.Value;
    }
}
