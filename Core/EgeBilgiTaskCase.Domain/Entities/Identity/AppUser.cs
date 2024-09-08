using Microsoft.AspNetCore.Identity;

namespace EgeBilgiTaskCase.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {
       // public int Id { get; set; }
        public Guid Guid { get; set; }
        public string NameSurname { get; set; }
        //public string Password { get; set; }
        //public string PasswordConfirm { get; set; }
        public string? Gender { get; set; }
        public string? IdentityNo { get; set; }
        public string? GSM { get; set; }
        public string? GSMPersonal { get; set; }
        public int StatusId { get; set; }
        public DateTime? BirthDate { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
    }
}
