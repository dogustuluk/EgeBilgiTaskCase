using Microsoft.AspNetCore.Identity;

namespace EgeBilgiTaskCase.Domain.Entities.Identity
{
    public class AppRole : IdentityRole<int>
    {
        public bool isActive { get; set; }
    }
}
