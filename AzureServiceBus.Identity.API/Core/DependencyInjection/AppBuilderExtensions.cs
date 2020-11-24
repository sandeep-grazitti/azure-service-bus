using Microsoft.AspNetCore.Builder;

namespace AzureServiceBus.Identity.API.Core.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerServices(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cars Island Catalog API v1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
