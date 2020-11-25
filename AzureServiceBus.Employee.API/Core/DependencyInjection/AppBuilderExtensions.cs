using Microsoft.AspNetCore.Builder;

namespace AzureServiceBus.Employee.API.Core.DependencyInjection
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API v1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
