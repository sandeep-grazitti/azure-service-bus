using AzureServiceBus.Identity.API.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AzureServiceBus.Identity.API
{
    /// <summary>
    /// 
    /// </summary>
    public class UserDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {

        }
    }
}
