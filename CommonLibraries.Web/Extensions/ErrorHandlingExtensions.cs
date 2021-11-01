using CommonLibraries.Web;
using Microsoft.AspNetCore.Builder;

namespace CommonLibraries.Web.Extensions
{
    public static class ErrorHandlingExtensions
    {
        public static void UseErrorHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
        }
    }
}
