using CommonLibraries.Web;
using Microsoft.AspNetCore.Builder;

namespace CommonLibraries.Web.Extensions
{
    public static class ConfigExtensions
    {
        public static void UserApiErrorHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
        }
    }
}
