using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AzureServiceBus.Identity.API.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationUser : IdentityUser<string>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public string Role { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public string Token { get; set; }
    }
}
