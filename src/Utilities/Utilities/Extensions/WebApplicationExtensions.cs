using Microsoft.AspNetCore.Builder;
using Utilities.Exceptions;

namespace Utilities.Extensions;
public static class WebApplicationExtensions
{
    public static IApplicationBuilder UseAppExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseExceptionHandler(new ExceptionHandlerOptions
        {
            ExceptionHandler = ExceptionHandler.Handle
        });
    }
}
