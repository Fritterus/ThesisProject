using Microsoft.AspNetCore.Identity;

namespace CarSharing.Entities.DataBaseModels
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public decimal Money { get; set; }
    }
}
