using AzureServiceBus.Identity.API.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AzureServiceBus.Identity.API
{
    public class UserDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {

        }
    }
}
