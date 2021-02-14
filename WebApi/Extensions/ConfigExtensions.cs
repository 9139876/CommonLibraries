using Microsoft.AspNetCore.Builder;

namespace CommonLibraries.WebApiPack.Extensions
{
    public static class ConfigExtensions
    {
        public static void UserApiErrorHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
        }
    }
}
