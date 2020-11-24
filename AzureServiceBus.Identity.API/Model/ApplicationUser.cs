using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AzureServiceBus.Identity.API.Model
{
    public class ApplicationUser : IdentityUser<string>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string Role { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
